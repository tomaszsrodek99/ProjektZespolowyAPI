using AutoMapper;

namespace ProjektAPI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Models.Category, Dtos.CategoryDto>().ReverseMap();
            CreateMap<Models.Expense, Dtos.ExpenseDto>().ReverseMap();
            CreateMap<Models.Role, Dtos.RoleDto>().ReverseMap();
            CreateMap<Models.User, Dtos.UserDto>().ReverseMap();
        }
    }
}
