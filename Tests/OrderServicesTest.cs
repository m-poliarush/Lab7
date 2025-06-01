using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Profiles;
using BusinessLogic.Services;
using DomainData.Repository;
using DomainData.UoW;
using MenuManager.DB.Models;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Tests;
public class OrderServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IGenericRepository<Order>> _mockOrderRepo;
    private readonly Mock<IGenericRepository<Dish>> _mockDishRepo;
    private readonly Mock<IGenericRepository<ComplexDish>> _mockComplexDishRepo;
    private readonly IMapper _mapper;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockOrderRepo = new Mock<IGenericRepository<Order>>();
        _mockDishRepo = new Mock<IGenericRepository<Dish>>();
        _mockComplexDishRepo = new Mock<IGenericRepository<ComplexDish>>();

        _mockUnitOfWork.Setup(u => u.OrdersRepository).Returns(_mockOrderRepo.Object);
        _mockUnitOfWork.Setup(u => u.DishRepository).Returns(_mockDishRepo.Object);
        _mockUnitOfWork.Setup(u => u.ComplexDishRepository).Returns(_mockComplexDishRepo.Object);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(DishProfile).Assembly);
        });
        _mapper = config.CreateMapper();

        _orderService = new OrderService(_mockUnitOfWork.Object, _mapper);
    }

    [Fact]
    public void GetAllOrdersTest()
    {
        var dish = new Dish
        {
            ID = 1,
            Name = "Dish1",
            Description = "Desc",
            Price = 100,
            Category = DishCategory.Main
        };

        var order = new Order
        {
            OrderID = 1
        };
        order.dishes.Add(dish);

        var orders = new List<Order> { order };

        _mockOrderRepo.Setup(r => r.GetAll(It.IsAny<System.Linq.Expressions.Expression<System.Func<Order, object>>>()))
            .Returns(orders);


        var result = _orderService.GetAllOrders();

        Assert.Single(result);
        Assert.Equal(1, result[0].OrderID);
        Assert.Single(result[0].dishes);
        Assert.Equal(100, result[0].dishes[0].Price);
    }

    [Fact]
    public void CreateOrderTest()
    {

        var orderModel = new OrderBusinessModel();
        orderModel.AddDish(new DishBusinessModel
        {
            ID = 1,
            Name = "Dish1",
            Description = "Desc",
            Price = 100,
            Category = DishCategory.Main
        });
        orderModel.AddDish(new ComplexDishBusinessModel
        {
            ID = 2,
            Name = "Complex1",
            Description = "Complex",
            Price = 500,
            Category = DishCategory.Complex
        });

        _mockDishRepo.Setup(r => r.GetTrackedOrAttach(1)).Returns(new Dish { ID = 1, Name = "Dish1", Description="Desc1", Price = 100, Category = DishCategory.Main });
        _mockComplexDishRepo.Setup(r => r.GetTrackedOrAttach(2)).Returns(new ComplexDish { ID = 2, Name = "Complex1", Description = "Desc2", Price = 500, Category = DishCategory.Complex });

        _orderService.CreateOrder(orderModel);

        _mockOrderRepo.Verify(r => r.Create(It.Is<Order>(o =>
            o.dishes.Count == 2 &&
            o.dishes[0].ID == 1 &&
            o.dishes[1].ID == 2
        )), Times.Once);

        _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
    }
}
