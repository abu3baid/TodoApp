using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Common.Extceptions;
using TodoApp.Common.Helper;
using TodoApp.Core.Managers.Interfaces;
using TodoApp.DbModel;
using TodoApp.DbModel.Models;
using TodoApp.ModelView.ModelView;

namespace TodoApp.Core.Managers
{
    public class UserManager : IUserManager
    {
        private tododbContext _tododbContext;
        private IMapper _mapper;
        public UserManager(tododbContext tododbContext, IMapper mapper)
        {
            _tododbContext = tododbContext;
            _mapper = mapper;
        }

        #region public
        public LoginUserResponseView Login(UserLoginView userReg)
        {
            var user = _tododbContext.Users.FirstOrDefault(a =>
                a.Email.Equals(userReg.Email, StringComparison.InvariantCultureIgnoreCase));

            if (user == null || !VerifyHashPassword(userReg.Password, user.Password))
            {
                throw new ServiceValidationException(300, "Invalid email or password");
            }

            var res = _mapper.Map<LoginUserResponseView>(user);
            res.Token = $"Bearer {GenerateJWTToken(user)}";
            return res;
        }

        public LoginUserResponseView SignUp(UserRegisterView userReg)
        {
            if (_tododbContext.Users.Any(a => a.Email.Equals(userReg.Email,
                    StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ServiceValidationException("User Already Exist");
            }

            var hashedPassword = HashPassword(userReg.Password);

            var user = _tododbContext.Users.Add(new User
            {
                FirstName = userReg.FirstName,
                LastName = userReg.LastName,
                Email = userReg.Email,
                Password = hashedPassword,
                Image = string.Empty
            }).Entity;

            _tododbContext.SaveChanges();

            var res = _mapper.Map<LoginUserResponseView>(user);
            res.Token = $"Bearer {GenerateJWTToken(user)}";

            return res;
        }

        public UserModelView UpdateProfile(UserModelView currentUser, UserModelView request)
        {
            var user = _tododbContext.Users.FirstOrDefault(a => a.Id == currentUser.Id) ??
                       throw new ServiceValidationException("User not found");

            var url = "";

            if (!string.IsNullOrWhiteSpace(request.ImageString))
            {
                url = Helper.SaveImage(request.ImageString, "profileimages");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(url))
            {
                var baseUrl = "https://localhost:44380";
                user.Image = $@"{baseUrl}/api/v1/user/fileretrive/profilepic?filename={url}";
            }

            _tododbContext.SaveChanges();
            return _mapper.Map<UserModelView>(user);
        }

        public void DeleteUser(UserModelView currentUser, int id)
        {
            if (currentUser.Id == id)
            {
                throw new ServiceValidationException("You have no access to delete yourself");
            }

            var user = _tododbContext.Users.FirstOrDefault(a => a.Id == id)
                       ?? throw new ServiceValidationException("User not found");

            user.IsArchived = true;
            _tododbContext.SaveChanges();
        }

        #endregion public

        #region private

        private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        private static bool VerifyHashPassword(string password, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPassword);
        }

        private string GenerateJWTToken(User user)
        {
            var jwtKey = "#test.key*&^vanthis%$^&*()$%^@#$@!@#%$#^%&*%^*";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("DateOfJoining", user.CreatedDateUtcTime.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var issuer = "test.com";

            var token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddDays(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion private 
    }
}
