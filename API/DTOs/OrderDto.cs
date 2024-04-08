﻿using API.Entities;
using System;

namespace API.DTOs
{
    public class OrderDto
    {
        public AppUser User { get; set; }
        public bool IsActive { get; set; } = true;
        public int TableNumber { get; set; }
        public DateTime TimeIn { get; set; } = DateTime.UtcNow;
    }
}
