using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Lab7.Controllers
{
    [ApiController]
    [Route("Menus")]
    public class DailyMenuController : ControllerBase
    {
        private readonly IDailyMenuService _dailyMenuService;
        private readonly IMapper _mapper;
        public DailyMenuController(IDailyMenuService service, IMapper mapper)
        {
            _dailyMenuService = service;
            _mapper = mapper;
        }

        [HttpGet("GetMenus")]
        public IActionResult GetAllMeuns() {

            var menuList = _dailyMenuService.GetAllMenus();
            var result = new List<DailyMenuDTO>();
            foreach (var menu in menuList) {
                result.Add(_mapper.Map<DailyMenuDTO>(menu));
            }

            return Ok(result);

        }
        [HttpPut("Update")]
        public IActionResult UpdateMenu(DailyMenuDTO menu)
        {
            try
            {
                var newMenuModel = _mapper.Map<DailyMenuBusinessModel>(menu);
                _dailyMenuService.UpdateMenu(newMenuModel);
                return Ok(newMenuModel);
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
