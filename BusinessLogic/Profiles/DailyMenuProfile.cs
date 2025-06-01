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
    public class DailyMenuProfile : Profile
    {
        public DailyMenuProfile()
        {
            CreateMap<DailyMenuBusinessModel, DailyMenu>();
            CreateMap<DailyMenu, DailyMenuBusinessModel>();
        }
    }
}
