using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Services.Interfaces
{
    public interface IDailyMenuService
    {
        public List<DailyMenuBusinessModel> GetAllMenus();
        public void UpdateMenu(DailyMenuBusinessModel menu);
    }
}
