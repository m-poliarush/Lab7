using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuManager.DB.Models
{
    public class DailyMenu
    {
        [Key]
        public int DayID { get; set; }
        public string DayOfWeek { get; set; }
        public ObservableCollection<BaseMenuItem> Dishes { get; set; }
    }
}
