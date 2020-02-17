using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{
    /// <summary>
    /// Апишка
    /// </summary>
    [Route("api/[controller]")]
    public class SettlerController : Controller
    {
        private IHotelRepository repository;

        public SettlerController(IHotelRepository repo) => repository = repo;

        [HttpGet]
        public IEnumerable<Settler> Get() => repository.Settlers;

        [HttpGet("{id}")]
        public Settler Get(int id) => repository[id];

        [HttpPost]
        public Settler Post([FromBody] Settler res) =>
            repository.AddSettler(new Settler
            {
                ID = res.ID,
                FirstName = res.FirstName,
                LastName = res.LastName,
                ArrivalDate = res.ArrivalDate,
                DepartureDate = res.DepartureDate,
                Room = res.Room
            });

        [HttpPut]
        public Settler Put([FromBody] Settler res) =>
            repository.UpdateSettler(res);

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DepartureSettler(id);
    }
}