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
        public OkObjectResult GetDishes() {
            var dishModels = _service.GetAllDishes();
            var result = new List<DishDTO>();
            foreach(DishBusinessModel dish in dishModels)
            {
                result.Add(_mapper.Map<DishDTO>(dish));
            }
            
            return new OkObjectResult(result);
        }
        [HttpPost("Create")]
        public IActionResult Create(DishDTO dish)
        {
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
            var modelToUpdate = _mapper.Map<BaseMenuItemBusinessModel>(item);
            _service.UpdateDish(modelToUpdate);
            return Ok();
        }
    }
}
