export enum DishCategory {
  Main = 0,
  First = 1,
  Side = 2,
  Drink = 3,
  Complex = 4,
}


export interface BaseMenuItem {
  id: number;
  name: string;
  description: string;
  price: number;
  category: DishCategory;
}

export interface ComplexDish extends BaseMenuItem {
  dishList: BaseMenuItem[];
}

export interface Order {
  orderId: number;
  dishDTOs: BaseMenuItem[];
  totalCost: number;
}

export interface DailyMenu {
    id:number;

  dayOfWeek: string;
  baseMenuItems: (BaseMenuItem | ComplexDish)[];
}