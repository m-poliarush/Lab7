using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;
using MenuManager.DB;
using Microsoft.EntityFrameworkCore;

namespace MenuManager.Repository.DishesRepository
{
    public class DishesRepository : IDishesRepository, IDisposable
    {
        private MenuContext context;

        public DishesRepository(MenuContext context)
        {
            this.context = context;
        }

        public IEnumerable<BaseMenuItem> GetDishes()
        {
            return context.Dishes.ToList();
        }
        public IEnumerable<BaseMenuItem> GetComplexDishes()
        {
            return context.ComplexDishes.ToList();
        }

        public BaseMenuItem GetDishByID(int id)
        {
            return context.Dishes.Find(id);
        }

        public void InsertDish(BaseMenuItem dish)
        {
            if (dish is Dish d)
            {
                context.Dishes.Add(d);
            }
            else if(dish is ComplexDish c)
            {
                context.ComplexDishes.Add(c);
            }
            context.SaveChanges();
        }

        public void DeleteDish(int dishID)
        {
            BaseMenuItem dish = context.Dishes.Find(dishID);
            if(dish == null)
            {
                dish = context.ComplexDishes.Find(dishID);
            }
            if (dish is Dish d)
            {
                context.Dishes.Remove(d);
            }
            else if(dish is ComplexDish c)
            {
                context.ComplexDishes.Remove(c);
            }
            context.SaveChanges();
        }

        public void UpdateDish(BaseMenuItem dish)
        {
            context.Entry(dish).State = EntityState.Modified;
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
