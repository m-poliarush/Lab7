using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using DomainData.UoW;

namespace BusinessLogic.Services.Interfaces
{
    public interface IOrderService
    {
        public List<OrderBusinessModel> GetAllOrders();
        public void CreateOrder(OrderBusinessModel order);
    }
}
