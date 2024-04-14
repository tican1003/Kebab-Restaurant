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
                MenuId = addItemDto.MenuId,
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
        [HttpGet("exist/{id}")]
        public async Task<ActionResult<bool>> IsExist(int id)
        {
                return await _itemRepository.IsExist(id);
        }
        [HttpGet("exist/order/{id}")]
        public async Task<ActionResult<bool>> IsExistOrder(int id)
        {
            return await _itemRepository.IsExistOrder(id);
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
        [HttpPut("menu/{id}")]
        public async Task<ActionResult<Item>> UpdateByMenuId(int id, ItemDto itemDto)
        {
            var item = await _itemRepository.GetItemByMenuIdAsync(id);
            if (item == null)
            {
                return NotFound("Item not found");
            }

            _mapper.Map(itemDto, item);

            if (await _itemRepository.SaveAllAsync()) return item;

            return BadRequest("Failed to update item");
        }
        [HttpPost("plus")]

        public async Task<ActionResult<bool>> PlusItemByMenuId(OrderMenuIdDto om)
        {
            var item = await _itemRepository.PlusItemAsync(om);
            if (item == false)
            {
                return BadRequest("Failed to plus item");
            }

            return Ok(true);
        }
        [HttpPost("minus")]
        public async Task<ActionResult<bool>> MinusItemByMenuId(OrderMenuIdDto om)
        {
            var item = await _itemRepository.MinusItemAsync(om);
            if (item == false)
            {
                return BadRequest("Failed to plus item");
            }

            return Ok(true);
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

        [HttpGet("order/{id}")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemByOrderId(int id)
        {
            var items = await _itemRepository.GetItemsByOrderIdAsync(id);
            return Ok(items);
        }
    }
}
