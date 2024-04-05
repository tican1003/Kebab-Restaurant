using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BillController : BaseApiController
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public BillController(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BillDto>> Create(BillDto billDto)
        {
            var bill = _mapper.Map<Bill>(billDto);

            await _billRepository.CreateBillAsync(bill);
            await _billRepository.SaveAllAsync();

            return billDto;
        }

        [HttpGet]
        public async Task<ActionResult<BillDto>> GetBills()
        {
            var bills = await _billRepository.GetBillsAsync();
            return Ok(bills);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillDto>> GetBill(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BillDto>(bill));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BillDto billDto)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _mapper.Map(billDto, bill);

            if(await _billRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update bill");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _billRepository.DeleteBill(bill);
            if(await _billRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to delete bill");
        }
    }
}
