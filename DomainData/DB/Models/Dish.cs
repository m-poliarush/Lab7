using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuManager.DB.Models
{
    public class Dish : BaseMenuItem
    {
        public List<ComplexDish> complexDishes { get; set; }
    }
}
