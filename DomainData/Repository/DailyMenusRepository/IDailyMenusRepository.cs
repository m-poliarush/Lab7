using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;

namespace MenuManager.Repository.DailyMenusRepository
{
    public interface IDailyMenusRepository
    {
        IEnumerable<DailyMenu> GetMenu();
        DailyMenu GetMenuByID(int MenuId);
        void InsertMenu(DailyMenu menu);
        void DeleteMenu(int menuID);
        void UpdateMenu(DailyMenu menu);
        void Save();
    }
}
