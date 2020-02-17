using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Hotel.Models.Room;

namespace Hotel.Models
{
    public class SeedData
    {
        /// <summary>
        /// Метод для заполнения приложения начальными данными (Комнаты и посетители)
        /// </summary>
        /// <param name="app"></param>
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();

            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange(
                    new Room
                    {
                        ID = 1,
                        roomType = RoomType.Standart,
                        Capacity = 1,
                        Cost = 10,
                        Settlers = new List<Settler>()
                    },
                    new Room
                    {
                        ID = 2,
                        roomType = RoomType.Comfort,
                        Capacity = 2,
                        Cost = 20,
                        Settlers = new List<Settler>()
                    },
                    new Room
                    {
                        ID = 3,
                        roomType = RoomType.Luxe,
                        Capacity = 2,
                        Cost = 40,
                        Settlers = new List<Settler>()
                    },
                    new Room
                    {
                        ID = 4,
                        roomType = RoomType.Standart,
                        Capacity = 2,
                        Cost = 15,
                        Settlers = new List<Settler>()
                    },
                    new Room
                    {
                        ID = 5,
                        roomType = RoomType.Comfort,
                        Capacity = 3,
                        Cost = 35,
                        Settlers = new List<Settler>()
                    }
                );
                context.SaveChanges();
            }
            if (!context.Settlers.Any())
            {
                context.Settlers.AddRange(
                    new Settler
                    {
                        ID = 1,
                        FirstName = "Elisey",
                        LastName = "Laptenok",
                        ArrivalDate = DateTime.Now,
                        DepartureDate = DateTime.Now.AddMinutes(10),
                        Room = context.Rooms.Where(r => r.ID == 2).First()
                    },
                    new Settler
                    {
                        ID = 2,
                        FirstName = "Stas",
                        LastName = "Dayanov",
                        ArrivalDate = DateTime.Now,
                        DepartureDate = DateTime.Now.AddMinutes(10),
                        Room = context.Rooms.Where(r => r.ID == 1).First()
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
