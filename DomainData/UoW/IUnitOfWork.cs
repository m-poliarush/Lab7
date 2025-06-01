using DomainData.Repository;
using MenuManager.DB.Models;

namespace DomainData.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<DailyMenu> MenusRepository { get; }
        public IGenericRepository<Dish> DishRepository { get; }
        public IGenericRepository<ComplexDish> ComplexDishRepository { get; }
        public IGenericRepository<Order> OrdersRepository { get; }

        public void Save();
    }
}
