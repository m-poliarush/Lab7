using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DomainData.UoW;
using MenuManager.DB.Models;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<OrderBusinessModel> GetAllOrders()
        {
            List<OrderBusinessModel> result = new List<OrderBusinessModel>();

            var orders = _unitOfWork.OrdersRepository.GetAll(o => o.dishes);

            foreach (var order in orders)
            {
                result.Add(_mapper.Map<OrderBusinessModel>(order));
            }
            return result;
        }
        public void CreateOrder(OrderBusinessModel order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            

            foreach (var dishModel in order.dishes)
            {
                BaseMenuItem dishEntity;
                dishEntity = _unitOfWork.DishRepository.GetTrackedOrAttach(dishModel.ID);

                if (dishEntity == null)
                {
                    dishEntity = _unitOfWork.ComplexDishRepository.GetTrackedOrAttach(dishModel.ID);
                }
                else if(dishEntity == null)
                {
                    continue;
                }
                
                orderEntity.dishes.Add(dishEntity);
            }

            _unitOfWork.OrdersRepository.Create(orderEntity);
            _unitOfWork.Save();
        }
    }
}
