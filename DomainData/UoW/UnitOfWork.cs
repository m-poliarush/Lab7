using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainData.Repository;
using MenuManager.DB;
using MenuManager.DB.Models;

namespace DomainData.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;

        private readonly MenuContext menuContext;
        private IGenericRepository<DailyMenu> _menusRepository;
        private IGenericRepository<Dish> _dishRepository;
        private IGenericRepository<ComplexDish> _complexDishRepository;
        private IGenericRepository<Order> _ordersRepository;

        public IGenericRepository<DailyMenu> MenusRepository => _menusRepository ??= new GenericRepository<DailyMenu>(menuContext);
        public IGenericRepository<Dish> DishRepository => _dishRepository ??= new GenericRepository<Dish>(menuContext);
        public IGenericRepository<ComplexDish> ComplexDishRepository => _complexDishRepository ??= new GenericRepository<ComplexDish>(menuContext);
        public IGenericRepository<Order> OrdersRepository => _ordersRepository ??= new GenericRepository<Order>(menuContext);


        public UnitOfWork(MenuContext context)
        {
            this.menuContext = context;
        }

        public void Save()
        {
            menuContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    menuContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
