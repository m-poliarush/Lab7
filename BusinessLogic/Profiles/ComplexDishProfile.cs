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
    public class ComplexDishProfile : Profile
    {
        public ComplexDishProfile()
        {
            CreateMap<ComplexDishBusinessModel, ComplexDish>()
            .ForMember(dest => dest.DishList, opt => opt.Ignore());

            CreateMap<ComplexDish, ComplexDishBusinessModel>()
                .ForMember(dest => dest.DishList, opt => opt.MapFrom(src => src.DishList));
        }
    }
}
