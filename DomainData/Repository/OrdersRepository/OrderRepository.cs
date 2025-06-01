using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB;
using MenuManager.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuManager.Repository.OrdersRepository
{
    public class OrdersRepository : IOrdersRepository
    {
        private MenuContext context;

        public OrdersRepository(MenuContext context)
        {
            this.context = context;
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders.Include(x => x.dishes).ToList();
        }

        public Order GetOrderByID(int id)
        {
            return context.Orders.Include(x => x.dishes).FirstOrDefault(x => x.OrderID == id);
        }

        public void InsertOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void DeleteOrder(int orderID)
        {
            Order order = context.Orders.Find(orderID);
            context.Orders.Remove(order);
            context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
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
