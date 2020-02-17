using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    public class HotelDdContext : DbContext
    {
        public HotelDdContext(DbContextOptions<HotelDdContext> options)
            : base(options) { }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Settler> Settlers { get; set; }
    }
}
