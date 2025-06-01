using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;
using BusinessLogic.Models;
using BusinessLogic.Profiles;
using BusinessLogic.Services;
using DomainData.Repository;
using DomainData.UoW;
using MenuManager.DB.Models;
using System.Net.Security;
using System.Linq.Expressions;

namespace Tests
{
    public class DishServicesTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DishService _dishService;
        private readonly Mock<IGenericRepository<Dish>> _mockDishRepository;
        private readonly Mock<IGenericRepository<ComplexDish>> _mockComplexDishRepository;

        public DishServicesTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockDishRepository = new Mock<IGenericRepository<Dish>>();
            _mockComplexDishRepository = new Mock<IGenericRepository<ComplexDish>>();

            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DishProfile).Assembly));
            var mapper = config.CreateMapper();

            _mockUnitOfWork.Setup(x => x.DishRepository).Returns(_mockDishRepository.Object);
            _mockUnitOfWork.Setup(x => x.ComplexDishRepository).Returns(_mockComplexDishRepository.Object);

            _dishService = new DishService(_mockUnitOfWork.Object, mapper);
        }

        [Fact]
        public void GetAllDishesTest()
        {
            var dishes = new List<Dish> {
            new Dish { ID = 1, Name = "Dish1", Description = "Desc1", Price = 100, Category = DishCategory.Main },
            new Dish { ID = 2, Name = "Dish2", Description = "Desc2", Price = 200, Category = DishCategory.First }
        };

            _mockDishRepository.Setup(r => r.GetAll()).Returns(dishes);

            var result = _dishService.GetAllDishes();

            Assert.Equal(2, result.Count);
            Assert.Equal(dishes[0].ID, result[0].ID);
            Assert.Equal(dishes[0].Name, result[0].Name);
            Assert.Equal(dishes[0].Description, result[0].Description);
            Assert.Equal(dishes[0].Price, result[0].Price);
            Assert.Equal(dishes[0].Category, result[0].Category);
            Assert.Equal(dishes[1].ID, result[1].ID);
            Assert.Equal(dishes[1].Name, result[1].Name);
            Assert.Equal(dishes[1].Description, result[1].Description);
            Assert.Equal(dishes[1].Price, result[1].Price);
            Assert.Equal(dishes[1].Category, result[1].Category);
        }

        [Fact]
        public void GetAllComplexDishesTest()
        {
            var complexDishes = new List<ComplexDish> {
            new ComplexDish { ID = 10, Name = "Complex1", Description = "CDesc1", Price = 500, Category = DishCategory.Complex, DishList = new List<Dish>() },
            new ComplexDish { ID = 20, Name = "Complex2", Description = "CDesc2", Price = 700, Category = DishCategory.Complex, DishList = new List<Dish>() }
        };

            _mockComplexDishRepository.Setup(r => r.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<ComplexDish, object>>>()))
                .Returns(complexDishes);

            var result = _dishService.GetAllComplexDishes();

            Assert.Equal(2, result.Count);
            Assert.Equal(complexDishes[0].ID, result[0].ID);
            Assert.Equal(complexDishes[1].ID, result[1].ID);
        }

        [Fact]
        public void GetAllDishesAndComplexDishesTest()
        {
            var dishes = new List<Dish> {
            new Dish { ID = 1, Name = "Dish1", Description = "Desc1", Price = 100, Category = DishCategory.Main }
            };

            var complexDishes = new List<ComplexDish> {
            new ComplexDish { ID = 10, Name = "Complex1", Description = "CDesc1", Price = 500, Category = DishCategory.Complex, DishList = new List<Dish>() }
            };

            _mockDishRepository.Setup(r => r.GetAll()).Returns(dishes);
            _mockComplexDishRepository.Setup(r => r.GetAll(It.IsAny<System.Linq.Expressions.Expression<Func<ComplexDish, object>>>()))
                .Returns(complexDishes);

            var result = _dishService.GetAllDishesAndComplexDishes();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, r => r.ID == 1 && r.Name == "Dish1");
            Assert.Contains(result, r => r.ID == 10 && r.Name == "Complex1");
        }

        [Fact]
        public void CreateDishTest()
        {
            var dishModel = new DishBusinessModel
            {
                ID = 0,
                Name = "NewDish",
                Description = "NewDesc",
                Price = 123,
                Category = DishCategory.Drink
            };

            _mockDishRepository.Setup(r => r.Create(It.IsAny<Dish>()))
                .Callback<Dish>(d => d.ID = 7);
            _mockUnitOfWork.Setup(u => u.Save());

            var id = _dishService.CreateDish(dishModel);

            _mockDishRepository.Verify(r =>
                r.Create(It.Is<Dish>(x =>
                    x.Name == dishModel.Name &&
                    x.Description == dishModel.Description &&
                    x.Price == dishModel.Price &&
                    x.Category == dishModel.Category
                )), Times.Once);

            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);

            Assert.Equal(7, id);
        }

        [Fact]
        public void CreateComplexDishTest()
        {
            var dishList = new List<DishBusinessModel> {
            new DishBusinessModel { ID = 1, Name = "Dish1", Description = "D1", Price = 100, Category = DishCategory.Main },
            new DishBusinessModel { ID = 2, Name = "Dish2", Description = "D2", Price = 200, Category = DishCategory.First }
        };

            var complexDishModel = new ComplexDishBusinessModel
            {
                ID = 0,
                Name = "ComplexDish",
                Description = "ComplexDesc",
                Price = 600,
                Category = DishCategory.Complex,
                DishList = dishList
            };

            _mockComplexDishRepository.Setup(r => r.Create(It.IsAny<ComplexDish>()))
                .Callback<ComplexDish>(cd => cd.ID = 15);

            _mockUnitOfWork.Setup(u => u.Save());

            _mockDishRepository.Setup(r => r.GetTrackedOrAttach(
             It.IsAny<int>(),
             It.IsAny<Expression<Func<Dish, object>>[]>()))
                 .Returns<int, Expression<Func<Dish, object>>[]>((id, _) => new Dish
                 {
                     ID = id,
                     Name = $"Dish{id}",
                     Description = $"Desc{id}",
                     Price = id * 100,
                     Category = DishCategory.Side
                 });

            var id = _dishService.CreateDish(complexDishModel);

            _mockComplexDishRepository.Verify(r =>
                r.Create(It.Is<ComplexDish>(x =>
                    x.Name == complexDishModel.Name &&
                    x.Description == complexDishModel.Description &&
                    x.Price == complexDishModel.Price &&
                    x.Category == complexDishModel.Category
                )), Times.Once);

            _mockDishRepository.Verify(r =>
                r.GetTrackedOrAttach(It.IsIn(dishList.Select(d => d.ID).ToArray())), Times.Exactly(dishList.Count));

            _mockUnitOfWork.Verify(u => u.Save(), Times.Exactly(2));

            Assert.Equal(15, id);
        }

        [Fact]
        public void ComplexDishMapTest()
        {
            var dishList = new List<DishBusinessModel> {
            new DishBusinessModel { ID = 1, Name = "D1", Description = "Desc1", Price = 100, Category = DishCategory.Side },
            new DishBusinessModel { ID = 2, Name = "D2", Description = "Desc2", Price = 200, Category = DishCategory.Side }
        };

            var complexDishEntity = new ComplexDish
            {
                DishList = new List<Dish>(),
                Name = "SomeComplex",
                Description = "SomeDesc",
                Price = 300,
                Category = DishCategory.Complex
            };

            _mockDishRepository.Setup(r => r.GetTrackedOrAttach(
            It.IsAny<int>(),
            It.IsAny<Expression<Func<Dish, object>>[]>()))
                    .Returns<int, Expression<Func<Dish, object>>[]>((id, _) => new Dish
                    {
                            ID = id,
                            Name = $"Dish{id}",
                            Description = $"Desc{id}",
                            Price = id * 100,
                            Category = DishCategory.Side
                    });

            _dishService.ComplexDishMap(complexDishEntity, dishList);

            Assert.Equal(2, complexDishEntity.DishList.Count);
            Assert.Contains(complexDishEntity.DishList, d => d.ID == 1);
            Assert.Contains(complexDishEntity.DishList, d => d.ID == 2);

            _mockDishRepository.Verify(r => r.GetTrackedOrAttach(It.IsAny<int>()), Times.Exactly(dishList.Count));
        }

        [Fact]
        public void DeleteDishTest()
        {
            var dish = new Dish { ID = 1, Name = "Dish1", Description = "Desc1", Price = 100, Category = DishCategory.Main };

            _mockDishRepository.Setup(r => r.GetById(dish.ID)).Returns(dish);
            _mockUnitOfWork.Setup(u => u.Save());

            _dishService.DeleteDish(dish.ID);

            _mockDishRepository.Verify(r => r.Delete(dish.ID), Times.Once);
            _mockComplexDishRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        [Fact]
        public void UpdateDishTest()
        {
            var dishModel = new DishBusinessModel
            {
                ID = 1,
                Name = "UpdatedDish",
                Description = "UpdatedDesc",
                Price = 150,
                Category = DishCategory.Side
            };

            var trackedDish = new Dish
            {
                ID = dishModel.ID,
                Name = "OldName",
                Description = "OldDesc",
                Price = 100,
                Category = DishCategory.Main
            };

            _mockDishRepository.Setup(r => r.GetTrackedOrAttach(dishModel.ID)).Returns(trackedDish);
            _mockUnitOfWork.Setup(u => u.Save());

            _dishService.UpdateDish(dishModel);

            _mockDishRepository.Verify(r => r.Update(It.Is<Dish>(x =>
                x.ID == dishModel.ID &&
                x.Name == dishModel.Name &&
                x.Description == dishModel.Description &&
                x.Price == dishModel.Price &&
                x.Category == dishModel.Category
            )), Times.Once);

            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
        }

        
    }
}