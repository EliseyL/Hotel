using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.Models
{
    /// <summary>
    /// Комната отеля
    /// </summary>
    public class Room
    {
        public enum SortState
        {
            IdAsc,     // по ID по возрастанию
            IdDesc,    // по ID по убыванию
            TypeAsc,   // по типу по возрастанию
            TypeDesc,  // по типу по убыванию
            CapacityAsc, // по вместимости по возрастанию
            CapacityDesc, // по вместимости по убыванию
            CostAsc, // по стоимости по возрастанию
            CostDesc // по стоимости по убыванию
        }
        public enum RoomType : int
        {
            Standart, Comfort, Luxe
        }
        public enum State : int
        {
            Empty, NotFull, Full
        }

        public int ID { get; set; }
        public RoomType roomType { get; set; }
        public State roomState 
        {
            get
            {
                State state = State.Empty;
                if (Capacity - Settlers.Count() == 0)
                {
                    state = State.Full;
                }
                if (Capacity - Settlers.Count() != 0 && Settlers.Count > 0)
                {
                    state = State.NotFull;
                }
                if (Capacity - Settlers.Count() == Capacity)
                {
                    state = State.Empty;
                }
                return state;
            }
        }
        public int Capacity { get; set; }
        public int Cost { get; set; }
        public List<Settler> Settlers { get; set; }
    }
}
