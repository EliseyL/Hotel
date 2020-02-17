using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    /// <summary>
    /// Класс, реализующий интерфейс IHotelRepository
    /// </summary>
    public class EFHotelRepository : IHotelRepository
    {
        private AppIdentityDbContext context;
        public EFHotelRepository(AppIdentityDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Room> Rooms => context.Rooms;

        public IQueryable<Settler> Settlers => context.Settlers;

        public Settler this[int id] => context.Settlers.Where(s => s.ID == id).FirstOrDefault();

        public IQueryable<Room> GetRoomsWithSettlers()
        {
            return context.Rooms.Include(u => u.Settlers);  // добавляем данные по посетителям
        }

        public Settler AddSettler(Settler settler)
        {
            //Через context.Settlers.Add -- не получалось, получал ошибку говорящую о дублировании ПК
            var result = context.Database.ExecuteSqlRaw("Insert into Settlers Values({0},{1},{2},{3},{4},{5})", settler.ID, settler.FirstName, settler.LastName, settler.ArrivalDate, settler.DepartureDate, settler.Room.ID);
            context.SaveChanges();
            return settler;
        }

        public Settler UpdateSettler(Settler settler)
        {
            context.Settlers.Update(settler);
            context.SaveChanges();
            return settler;
        }

        public void DepartureSettler(int settlerId)
        {
            var stlr = context.Settlers.Where(s => s.ID == settlerId).First();
            context.Settlers.Remove(stlr);
            context.SaveChanges();
        }
    }
}
