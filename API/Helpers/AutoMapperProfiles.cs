using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<AppUser, MemberDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<RoleDto, Role>();
            CreateMap<BillDto, Bill>();
            CreateMap<OrderDto, Order>();
            CreateMap<ItemDto, Item>();
            CreateMap<MenuDto, Menu>();
        }
    }
}
