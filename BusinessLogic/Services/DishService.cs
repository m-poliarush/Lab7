using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DomainData.UoW;
using MenuManager.DB.Models;

namespace BusinessLogic.Services
{
    public class DishService : IDishService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DishService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<BaseMenuItemBusinessModel> GetAllDishes()
        {
            List<BaseMenuItemBusinessModel> result = new List<BaseMenuItemBusinessModel>();

            var dishes = _unitOfWork.DishRepository.GetAll();

            foreach (var dish in dishes)
            {
                result.Add(_mapper.Map<DishBusinessModel>(dish));
            }

            return result;
        }
        public List<BaseMenuItemBusinessModel> GetAllComplexDishes()
        {
            List<BaseMenuItemBusinessModel> result = new List<BaseMenuItemBusinessModel>();

            var complexDishes = _unitOfWork.ComplexDishRepository.GetAll(d => d.DishList);
            foreach (var complexDish in complexDishes)
            {
                result.Add(_mapper.Map<ComplexDishBusinessModel>(complexDish));
            }
            return result;
        }
        public List<BaseMenuItemBusinessModel> GetAllDishesAndComplexDishes()
        {
            List<BaseMenuItemBusinessModel> result = new List<BaseMenuItemBusinessModel>
                (GetAllDishes().Concat(GetAllComplexDishes()));
            return result;
        }
        public int CreateDish(BaseMenuItemBusinessModel dish)
        {
            int id = -1;

            if (dish is DishBusinessModel dModel)
            {
                var dishEntity = _mapper.Map<Dish>(dModel);
                _unitOfWork.DishRepository.Create(dishEntity);
                _unitOfWork.Save();
                id = dishEntity.ID;
            }
            else if (dish is ComplexDishBusinessModel cdModel)
            {
                var complexDishEntity = _mapper.Map<ComplexDish>(cdModel);
                var originalDishList = cdModel.DishList;

                complexDishEntity.DishList = new List<Dish>();

                _unitOfWork.ComplexDishRepository.Create(complexDishEntity);
                _unitOfWork.Save();

                ComplexDishMap(complexDishEntity, originalDishList);

                _unitOfWork.Save();

                id = complexDishEntity.ID;
            }

            return id;
        }
        public void ComplexDishMap(ComplexDish complexDishEntity, List<DishBusinessModel> dishList)
        {
            complexDishEntity.DishList.Clear();
            foreach (var dishModel in dishList)
            {
                var trackedDish = _unitOfWork.DishRepository.GetTrackedOrAttach(dishModel.ID);
                if (trackedDish != null)
                {
                    complexDishEntity.DishList.Add(trackedDish);
                }
            }
        }
        public void DeleteDish(int id)
        {
            BaseMenuItem dish = _unitOfWork.DishRepository.GetById(id);

            if (dish == null)
            {
                dish = _unitOfWork.ComplexDishRepository.GetById(id);
            }
            if(dish == null)
            {
                throw new IndexOutOfRangeException("Wrong id");
            }

            if (dish is ComplexDish)
            {
                _unitOfWork.ComplexDishRepository.Delete(id);
            }

            else if (dish is Dish)
            {
                _unitOfWork.DishRepository.Delete(id);
            }
            _unitOfWork.Save();

        }
        public void UpdateDish(BaseMenuItemBusinessModel dish)
        {
            BaseMenuItem entityToUpdate;
            try
            {
                entityToUpdate = _unitOfWork.DishRepository.GetTrackedOrAttach(dish.ID);
            }
            catch (Exception ex) {
                throw new IndexOutOfRangeException("Wrong id");
            }
            if (entityToUpdate != null && entityToUpdate is Dish dishEntity)
            { 
                dishEntity = _mapper.Map<Dish>(dish);
                _unitOfWork.DishRepository.Update(dishEntity);
            }
            else if (entityToUpdate == null && dish is ComplexDishBusinessModel cd)
            {
                var dishList = cd.DishList;
                entityToUpdate = _unitOfWork.ComplexDishRepository.GetTrackedOrAttach(cd.ID, cd => cd.DishList);
                entityToUpdate = _mapper.Map<ComplexDish>(dish as ComplexDishBusinessModel);
                var complexDish = entityToUpdate as ComplexDish;
                if (complexDish != null)
                {
                    complexDish.DishList = new();
                    ComplexDishMap(complexDish, dishList);
                    _unitOfWork.ComplexDishRepository.Update(complexDish);
                }
            }
            _unitOfWork.Save();
        }
    }
}
