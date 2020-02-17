using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Hotel.Models;
using static Hotel.Models.Room;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Controllers
{
    [Authorize(Roles ="Admin, Employee, User")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Хранилище данных
        /// </summary>
        private IHotelRepository repository;
        public HomeController(IHotelRepository repo)
        {
            repository = repo;
        }

        // Костыль для IActionResult Room(int ID)
        public static Room selectedRoom = new Room();

        /// <summary>
        /// Создание посетителя в бд с привязкой к комнате.
        /// </summary>
        /// <param name="settler"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult AddStlr(Settler settler)
        {
            var nextId = (from Settlers in repository.Settlers orderby Settlers.ID descending select Settlers.ID).FirstOrDefault() + 1;
            settler.ID = nextId;
            settler.Room = selectedRoom;
            if (ModelState.IsValid)
            {
                if (selectedRoom.Capacity - selectedRoom.Settlers.Count() != 0)
                    repository.AddSettler(settler);
                else
                {
                    ModelState.AddModelError("", "Нет места в номере!");
                    return View("Room");
                }
            }
            else return View("Room");

            return View("Index", repository.GetRoomsWithSettlers());
        }
        /// <summary>
        /// Удаление/Выселение/Депортация посетителя по ID (Удаление из БД)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult DeparStlr(int ID)
        {
            repository.DepartureSettler(ID);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<Room> rooms = repository.GetRoomsWithSettlers();

            // Сортировка.
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["TypeSort"] = sortOrder == SortState.TypeAsc ? SortState.TypeDesc : SortState.TypeAsc;
            ViewData["CapacitySort"] = sortOrder == SortState.CapacityAsc ? SortState.CapacityDesc : SortState.CapacityAsc;
            ViewData["CostSort"] = sortOrder == SortState.CostAsc ? SortState.CostDesc : SortState.CostAsc;

            // Тип сортировки, далее см Index.cshtml
            rooms = sortOrder switch
            {
                SortState.IdDesc => rooms.OrderByDescending(s => s.ID),
                SortState.TypeAsc => rooms.OrderBy(s => s.roomType),
                SortState.TypeDesc => rooms.OrderByDescending(s => s.roomType),
                SortState.CapacityAsc => rooms.OrderBy(s => s.Capacity),
                SortState.CapacityDesc => rooms.OrderByDescending(s => s.Capacity),
                SortState.CostAsc => rooms.OrderBy(s => s.Cost),
                SortState.CostDesc => rooms.OrderByDescending(s => s.Cost),
                _ => rooms.OrderBy(s => s.ID),
            };
            return View(await rooms.AsNoTracking().ToListAsync());
        }

        [Authorize(Roles = "Admin, Employee")]
        public IActionResult Room(int ID)
        {
            //Не удалось прокинуть через форму объект, пошел обходным путем, ищу комнату по ID.
            ViewBag.RoomID = ID;
            var room = repository.GetRoomsWithSettlers().Where(r => r.ID == ID).FirstOrDefault();
            selectedRoom = room;
            return View(room.Settlers);
        }
    }
}
