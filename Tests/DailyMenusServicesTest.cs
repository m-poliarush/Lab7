using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services;
using DomainData.Repository;
using DomainData.UoW;
using MenuManager.DB.Models;
using Moq;
using System.Collections.ObjectModel;
using Xunit;

namespace Tests;
public class DailyMenuServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IGenericRepository<DailyMenu>> _mockMenuRepo;
    private readonly Mock<IGenericRepository<Dish>> _mockDishRepo;
    private readonly Mock<IGenericRepository<ComplexDish>> _mockComplexDishRepo;
    private readonly IMapper _mapper;
    private readonly DailyMenuService _menuService;

    public DailyMenuServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMenuRepo = new Mock<IGenericRepository<DailyMenu>>();
        _mockDishRepo = new Mock<IGenericRepository<Dish>>();
        _mockComplexDishRepo = new Mock<IGenericRepository<ComplexDish>>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DailyMenu, DailyMenuBusinessModel>().ReverseMap();
            cfg.CreateMap<BaseMenuItem, BaseMenuItemBusinessModel>().Include<Dish, DishBusinessModel>().Include<ComplexDish, ComplexDishBusinessModel>().ReverseMap();
            cfg.CreateMap<Dish, DishBusinessModel>().ReverseMap();
            cfg.CreateMap<ComplexDish, ComplexDishBusinessModel>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _mockUnitOfWork.Setup(u => u.MenusRepository).Returns(_mockMenuRepo.Object);
        _mockUnitOfWork.Setup(u => u.DishRepository).Returns(_mockDishRepo.Object);
        _mockUnitOfWork.Setup(u => u.ComplexDishRepository).Returns(_mockComplexDishRepo.Object);

        _menuService = new DailyMenuService(_mockUnitOfWork.Object, _mapper);
    }

    [Fact]
    public void GetAllMenusTest()
    {
        var menus = new List<DailyMenu>
        {
            new DailyMenu
            {
                DayID = 1,
                DayOfWeek = "Monday",
                Dishes = new ObservableCollection<BaseMenuItem>
                {
                    new Dish { ID = 1, Name = "Dish1", Description = "Desc", Price = 100, Category = DishCategory.Main }
                }
            }
        };
        var complexDishes = new List<ComplexDish>();

        _mockMenuRepo.Setup(r => r.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<DailyMenu, object>>>()))
            .Returns(menus);

        _mockComplexDishRepo.Setup(r => r.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<ComplexDish, object>>>()))
            .Returns(complexDishes);

        var result = _menuService.GetAllMenus();

        Assert.Single(result);
        Assert.Equal("Monday", result[0].DayOfWeek);
        Assert.Single(result[0].Dishes);
        Assert.IsType<DishBusinessModel>(result[0].Dishes.First());
    }

    [Fact]
    public void UpdateMenuTest()
    {
        var dishModel = new DishBusinessModel
        {
            ID = 1,
            Name = "Dish1",
            Description = "Desc",
            Price = 100,
            Category = DishCategory.Main
        };

        var menuModel = new DailyMenuBusinessModel
        {
            DayID = 2,
            DayOfWeek = "Tuesday",
            Dishes = new ObservableCollection<BaseMenuItemBusinessModel> { dishModel }
        };

        var menuEntity = new DailyMenu
        {
            DayID = 2,
            DayOfWeek = "Tuesday",
            Dishes = new ObservableCollection<BaseMenuItem>()
        };

        var dishEntity = new Dish
        {
            ID = 1,
            Name = "Dish1",
            Description = "Desc",
            Price = 100,
            Category = DishCategory.Main
        };

        _mockMenuRepo.Setup(r => r.GetTrackedOrAttach(2, It.IsAny<System.Linq.Expressions.Expression<Func<DailyMenu, object>>>()))
            .Returns(menuEntity);

        _mockDishRepo.Setup(r => r.GetTrackedOrAttach(1)).Returns(dishEntity);

        _menuService.UpdateMenu(menuModel);

        _mockMenuRepo.Verify(r => r.Update(menuEntity), Times.Exactly(2));
        _mockUnitOfWork.Verify(u => u.Save(), Times.Exactly(2));
        Assert.Single(menuEntity.Dishes);
        Assert.Equal(1, menuEntity.Dishes.First().ID);
    }
}
