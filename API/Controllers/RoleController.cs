using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class RoleController : BaseApiController
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoleController(IRoleRepository roleRepository, IMapper mapper, DataContext context)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<RoleDto>> Create(RoleDto roleDto)
        {
            if (await _context.Roles.AnyAsync(x => x.Name == roleDto.Name)) return BadRequest("Role is taken");

            var role = _mapper.Map<Role>(roleDto);

            await _roleRepository.CreateRoleAsync(role);
            await _roleRepository.SaveAllAsync();

            return roleDto;
        }

        [HttpGet]
        public async Task<ActionResult<RoleDto>> GetRoles()
        {
            return Ok(await _roleRepository.GetRolesAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDto>> GetRole(int id)
        {
            return Ok(await _roleRepository.GetRoleByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleDto>> Update(int id, RoleDto roleDto)
        {


            var role = await _roleRepository.GetRoleByIdAsync(id);
            if(role == null) return NotFound();

            _mapper.Map(roleDto, role);

            if(await _roleRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update role");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRole(int id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id);
            if (role == null) return NotFound();

            _roleRepository.DeleteRole(role);
            if (await _roleRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to delete role");
        }

    }
}
