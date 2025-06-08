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
    [Route("Dishes")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;
        private readonly IMapper _mapper;
        public DishController(IDishService service, IMapper mapper) {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("GetDishes")]
        public IActionResult GetDishes() {
            var dishModels = _service.GetAllDishes();
            var result = new List<DishDTO>();
            foreach (DishBusinessModel dish in dishModels)
            {
                result.Add(_mapper.Map<DishDTO>(dish));
            }

            return Ok(result);
        }
     
        [HttpPost("Create")]
        public IActionResult Create(DishDTO dish)
        {
            if(String.IsNullOrEmpty(dish.name) || String.IsNullOrEmpty(dish.description))
            {
                return BadRequest("Name or descriptions can not be empty");
            }
            var dishBusinessModel = _mapper.Map<DishBusinessModel>(dish);
            
            var id = _service.CreateDish(dishBusinessModel);
            return Ok(id);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.DeleteDish(id);
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);

            }
            return Ok();

        }
        [HttpPut("Update")]
        public IActionResult Update(BaseMenuItemDTO item)
        {
            if (String.IsNullOrEmpty(item.name) || String.IsNullOrEmpty(item.description))
            {
                return BadRequest("Name or descriptions can not be empty");
            }
            var modelToUpdate = _mapper.Map<BaseMenuItemBusinessModel>(item);
            _service.UpdateDish(modelToUpdate);
            return Ok();
        }
    }
}
