using AutoMapper;
using TodoApp.Common.Extensions;
using TodoApp.DbModel.Models;
using TodoApp.ModelView.ModelView;

namespace TodoApp.Core.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserModelView>().ReverseMap();
            CreateMap<User, LoginUserResponseView>().ReverseMap();
            CreateMap<User, UserResultView>().ReverseMap();
            CreateMap<Todo, TodoModelView>().ReverseMap();
            CreateMap<PagedResult<TodoModelView>, PagedResult<Todo>>().ReverseMap();
            CreateMap<TodoModelView, UserModelView>().ReverseMap();
        }
    }
}
