using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;

namespace MenuManager.Repository.DishesRepository
{
    internal interface IDishesRepository
    {
        IEnumerable<BaseMenuItem> GetDishes();
        IEnumerable<BaseMenuItem> GetComplexDishes();
        BaseMenuItem GetDishByID(int dishId);
        void InsertDish(BaseMenuItem dish);
        void DeleteDish(int dishID);
        void UpdateDish(BaseMenuItem dish);
        void Save();
    }
}
