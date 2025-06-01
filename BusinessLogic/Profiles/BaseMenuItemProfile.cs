using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using MenuManager.DB.Models;

namespace BusinessLogic.Profiles
{
    public class BaseMenuItemProfile : Profile
    {
        public BaseMenuItemProfile()
        {
            CreateMap<BaseMenuItemBusinessModel, Dish>().ReverseMap();
            CreateMap<BaseMenuItem, BaseMenuItemBusinessModel>()
            .Include<Dish, DishBusinessModel>()
            .Include<ComplexDish, ComplexDishBusinessModel>();
            CreateMap<BaseMenuItemBusinessModel, BaseMenuItem>()
                .Include<DishBusinessModel, Dish>()
                .ForMember(dest => dest.ID, opt => opt.Condition(src => src.ID != 0))
                .Include<ComplexDishBusinessModel, ComplexDish>()
                .ForMember(dest => dest.ID, opt => opt.Condition(src => src.ID != 0));
        }
    }
}
