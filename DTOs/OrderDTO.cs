using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public record OrderDTO
    {
        public int id {  get; set; } 
        public required int totalCost { get; set; }
        public required List<DishDTO> dishDTOs { get; set; }

    }
}
