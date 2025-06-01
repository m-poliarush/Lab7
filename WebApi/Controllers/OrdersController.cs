using System.Numerics;
using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DTOs;
using MenuManager.DB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
        [ApiController]
        [Route("Orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public OrdersController(IMapper mapper ,IOrderService service)
        {
            _mapper = mapper;
            _orderService = service;
        }
        

        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody] OrderDTO order)
        {
            var orderModel = _mapper.Map<OrderBusinessModel>(order);

            _orderService.CreateOrder(orderModel);
            return Ok();
        }

    }
}
