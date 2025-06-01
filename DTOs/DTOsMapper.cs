using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;

namespace DTOs
{
    class DTOsMapper : Profile
    {
        public DTOsMapper()
        {
            CreateMap<OrderDTO, OrderBusinessModel>()
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.totalCost))
                .ForMember(dest => dest.dishes, opt => opt.MapFrom(src => src.dishDTOs));
            CreateMap<DailyMenuDTO, DailyMenuBusinessModel>()
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.dayOfWeek))
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.baseMenuItems))
                .ForMember(dest => dest.DayID, opt => opt.MapFrom(src => src.id)).ReverseMap();
            CreateMap<DishDTO, BaseMenuItemBusinessModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ReverseMap();
            CreateMap<DishDTO, DishBusinessModel>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.id))
                .ReverseMap();
                
            CreateMap<ComplexDishDTO, BaseMenuItemBusinessModel>().ReverseMap();
            CreateMap<BaseMenuItemDTO, BaseMenuItemBusinessModel>().ReverseMap();

        }
    }
}
