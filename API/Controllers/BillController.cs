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

            return CreatedAtAction(nameof(GetBill), new { id = bill.Id }, _mapper.Map<BillDto>(bill));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillDto>>> GetBills()
        {
            var bills = await _billRepository.GetBillsAsync();
            var billDtos = _mapper.Map<IEnumerable<BillDto>>(bills);
            return Ok(billDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillDto>> GetBill(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound("Bill not found");
            }
            return Ok(_mapper.Map<BillDto>(bill));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BillDto billDto)
        {
            if (id != billDto.Id)
            {
                return BadRequest("Invalid bill ID");
            }

            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound("Bill not found");
            }

            _mapper.Map(billDto, bill);

            try
            {
                await _billRepository.SaveAllAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
                {
                    return NotFound("Bill not found");
                }
                else
                {
                    throw;
                }
            }

            return Ok("Bill updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bill = await _billRepository.GetBillByIdAsync(id);
            if (bill == null)
            {
                return NotFound("Bill not found");
            }

            _billRepository.DeleteBill(bill);
            await _billRepository.SaveAllAsync();

            return Ok("Bill deleted successfully");
        }

        private bool BillExists(int id)
        {
            return _billRepository.GetBillByIdAsync(id) != null;
        }
    }
}
