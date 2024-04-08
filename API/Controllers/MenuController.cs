using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class MenuController : BaseApiController
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuController(IMenuRepository menuRepository, IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MenuDto>> Create(MenuDto menuDto)
        {
            var menu = _mapper.Map<Menu>(menuDto);

            await _menuRepository.CreateMenuItemAsync(menu);
            await _menuRepository.SaveAllAsync();

            return menuDto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDto>>> GetMenu()
        {
            var menuItems = await _menuRepository.GetMenuAsync();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetMenuItem(int id)
        {
            var menuItem = await _menuRepository.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound("Menu item not found");
            }
            return Ok(_mapper.Map<MenuDto>(menuItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuDto menuDto)
        {
            var menuItem = await _menuRepository.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound("Menu item not found");
            }
            _mapper.Map(menuDto, menuItem);

            if (await _menuRepository.SaveAllAsync()) 
                return NoContent();

            return BadRequest("Failed to update menu item");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var menuItem = await _menuRepository.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound("Menu item not found");
            }

            _menuRepository.DeleteMenuItem(menuItem);
            if (await _menuRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to delete menu item");
        }
    }
}
