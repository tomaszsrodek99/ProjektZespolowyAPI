using AutoMapper;

namespace ProjektAPI.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Models.Budget, Dtos.BudgetDto>().ReverseMap();
            CreateMap<Models.Category, Dtos.CategoryDto>().ReverseMap();
            CreateMap<Models.PrivateCategory, Dtos.PrivateCategoryDto>().ReverseMap();
            CreateMap<Models.Expense, Dtos.ExpenseDto>().ReverseMap();
            CreateMap<Models.User, Dtos.UserDto>().ReverseMap();
        }
    }
}
