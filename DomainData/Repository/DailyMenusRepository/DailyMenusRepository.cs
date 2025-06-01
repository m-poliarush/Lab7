using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB;
using MenuManager.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuManager.Repository.DailyMenusRepository
{
    public class DailyMenusRepository : IDailyMenusRepository, IDisposable
    {
        private MenuContext context;

        public DailyMenusRepository(MenuContext context)
        {
            this.context = context;
        }

        public IEnumerable<DailyMenu> GetMenu()
        {
            return context.DailyMenus.Include(x => x.Dishes).ToList();
        }

        public DailyMenu GetMenuByID(int id)
        {
            return context.DailyMenus.Include(x => x.Dishes).FirstOrDefault(x => x.DayID == id);
        }

        public void InsertMenu(DailyMenu menu)
        {
            context.DailyMenus.Add(menu);
            context.SaveChanges();
        }

        public void DeleteMenu(int menuID)
        {
            DailyMenu menu = context.DailyMenus.Find(menuID);
            context.DailyMenus.Remove(menu);
            context.SaveChanges();
        }

        public void UpdateMenu(DailyMenu menu)
        {
            context.Entry(menu).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
