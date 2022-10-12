using Autofac.Core;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using System;
using System.Linq;
using TodoApp.Common.Extceptions;
using TodoApp.Core.Managers.Interfaces;
using TodoApp.ModelView.ModelView;

namespace TodoApp.API.Attributes
{
    public class TodoAppAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                
                var roleManager = context.HttpContext.RequestServices.GetService(typeof(IRoleManager)) as IRoleManager;

                var stringId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id").Value;

                int.TryParse(stringId, out int id);

                var user = new UserModelView { Id = id };

                if (roleManager.CheckAccess(user))
                {
                    return;
                }

                throw new Exception("Unauthorized");
            }
            catch (RetryLimitExceededException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException("An Error occurred please contact system administrator");
            }
            catch (InvalidOperationException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException("An Error occurred please contact system administrator");
            }
            catch (DependencyResolutionException e)
            {
                Log.Logger.Information(e.Message);
                throw new ServiceValidationException("An Error occurred please contact system administrator");
            }
            catch (NullReferenceException ex)
            {
                Log.Logger.Information(ex.Message);
                throw new ServiceValidationException("An Error occurred please contact system administrator");
            }
            catch (Exception ex)
            {
                Log.Logger.Information(ex.Message);

                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
