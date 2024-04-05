using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ItemController : BaseApiController
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemController(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Create(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);

            await _itemRepository.CreateItemAsync(item);
            await _itemRepository.SaveAllAsync();

            return itemDto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _itemRepository.GetItemsAsync();
            return Ok(_mapper.Map<IEnumerable<ItemDto>>(items));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound("Item not found");
            }
            return Ok(_mapper.Map<ItemDto>(item));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ItemDto itemDto)
        {
            var item = await _itemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound("Item not found");
            }

            _mapper.Map(itemDto, item);

            if (await _itemRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update item");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound("Item not found");
            }

            _itemRepository.DeleteItem(item);
            if (await _itemRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to delete item");
        }

    }
}
