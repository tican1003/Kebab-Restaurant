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
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemController(IItemRepository itemRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Create(AddItemDto addItemDto)
        {

            var order = await _orderRepository.GetOrderByIdAsync(addItemDto.OrderId);
            var itemDto = new ItemDto
            {
                Name = addItemDto.Name,
                Price = addItemDto.Price,
                Quantity = addItemDto.Quantity,
                CaculationUnit = addItemDto.CaculationUnit,
                IsSuccess = addItemDto.IsSuccess,
                Order = order,
            };
            var item = _mapper.Map<Item>(itemDto);

            await _itemRepository.CreateItemAsync(item);
            if (await _itemRepository.SaveAllAsync()) return itemDto;
            return BadRequest("Failed to create Item");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _itemRepository.GetItemsAsync();
            return Ok(items);
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
