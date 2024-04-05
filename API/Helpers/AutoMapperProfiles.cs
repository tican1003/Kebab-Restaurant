﻿using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<RegisterDto, AppUser>();
            CreateMap<RoleDto, Role>();
            CreateMap<BillDto, Bill>();
            CreateMap<Bill, BillDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderDto>();
            CreateMap<ItemDto, Item>();
            CreateMap<Item, ItemDto>();
        }
    }
}
