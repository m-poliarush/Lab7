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
            if(order.dishDTOs.Count < 1)
            {
                return BadRequest("Order list can not be empty");
            }
            var orderModel = _mapper.Map<OrderBusinessModel>(order);
            try
            {
                _orderService.CreateOrder(orderModel);
            }
            catch (Exception ex) {
                return BadRequest("Wrong id");
            }
            return Ok();
        }

    }
}
