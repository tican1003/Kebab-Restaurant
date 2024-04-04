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
        public async Task<ActionResult<RoleDto>> CreateRole(RoleDto roleDto)
        {
            if (await _context.Roles.AnyAsync(x => x.Name == roleDto.Name)) return BadRequest("Role is taken");

            var role = _mapper.Map<Role>(roleDto);

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return roleDto;
        }

        [HttpGet]
        public async Task<ActionResult<RoleDto>> GetRoles()
        {
            return Ok(await _roleRepository.GetRolesAsync());
        }

    }
}
