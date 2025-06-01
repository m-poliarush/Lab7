using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface IDishService
    {
        public void UpdateDish(BaseMenuItemBusinessModel dish);
        public void DeleteDish(int id);
        public int CreateDish(BaseMenuItemBusinessModel dish);
        public List<BaseMenuItemBusinessModel> GetAllDishes();
        public List<BaseMenuItemBusinessModel> GetAllComplexDishes();
        public List<BaseMenuItemBusinessModel> GetAllDishesAndComplexDishes();
    }
}
