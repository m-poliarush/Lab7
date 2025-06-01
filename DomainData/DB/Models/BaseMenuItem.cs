using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuManager.DB.Models
{
    public abstract class BaseMenuItem
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public required DishCategory Category { get; set; }
        public List<Order> orders { get; set; }
        public List<DailyMenu> menus { get; set; }
    }
}
