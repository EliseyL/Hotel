using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    /// <summary>
    /// Интерфейс хранилища для реализации
    /// </summary>
    public interface IHotelRepository
    {
        /// <summary>
        /// Комнаты в отеле
        /// </summary>
        public IQueryable<Room> Rooms { get; }
        /// <summary>
        /// Посетители
        /// </summary>
        public IQueryable<Settler> Settlers { get; }
        /// <summary>
        /// Метод возвращает коллецию комнат с информацией о проживающих в них жильцах
        /// </summary>
        /// <returns></returns>
        public IQueryable<Room> GetRoomsWithSettlers();
        Settler this[int id] { get; }

        public Settler AddSettler(Settler settler);
        Settler UpdateSettler(Settler settler);
        public void DepartureSettler(int settlerId);
    }
}
