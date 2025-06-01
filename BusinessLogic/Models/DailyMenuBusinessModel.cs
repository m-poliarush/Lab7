using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;

namespace BusinessLogic.Models
{
    public class DailyMenuBusinessModel
    {
        public int DayID { get; set; }
        public string DayOfWeek { get; set; }
        public ObservableCollection<BaseMenuItemBusinessModel> Dishes { get; set; }
    }
}
