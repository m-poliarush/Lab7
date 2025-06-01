using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class DailyMenuDTO
    {
        public int id {  get; set; }
        public string dayOfWeek { get; set; }
        public List<BaseMenuItemDTO> baseMenuItems { get; set; }
    }
}
