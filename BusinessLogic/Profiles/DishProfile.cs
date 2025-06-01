using AutoMapper;

using BusinessLogic.Models;
using MenuManager.DB.Models;
namespace BusinessLogic.Profiles
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            
            CreateMap<Dish, DishBusinessModel>();

            CreateMap<DishBusinessModel, Dish>();
        }
    }
}
