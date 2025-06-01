using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;

namespace BusinessLogic.Models
{
    public class OrderBusinessModel
    {
        public int OrderID { get; set; }

        public int TotalCost { get; private set; }

        private List<BaseMenuItemBusinessModel> _dishes = new();
        public List<BaseMenuItemBusinessModel> dishes
        {
            get => _dishes;
            set => _dishes = value;
        }


        public void AddDish(BaseMenuItemBusinessModel dish)
        {
            _dishes.Add(dish);
            UpdateTotalCost();
        }
        public void RemoveDish(BaseMenuItemBusinessModel dish)
        {
            _dishes.Remove(dish);
            UpdateTotalCost();
        }
        public void ClearOrderList()
        {
            _dishes.Clear();
            UpdateTotalCost();
        }

        private void UpdateTotalCost()
        {
            TotalCost = _dishes.Sum(dish => dish.Price);
        }
    }
}
