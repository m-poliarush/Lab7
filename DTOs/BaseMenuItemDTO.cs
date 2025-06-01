using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class BaseMenuItemDTO
    {

        public int id { get; set; }
        public required string name { get; set; }
        public required string description { get; set; }
        public required int price { get; set; }
        public required int category { get; set; }

    }
}
