using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;

namespace BusinessLogic.Models
{
    public class BaseMenuItemBusinessModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public DishCategory Category { get; set; }
    }
}
