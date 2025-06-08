'use client';

import React, { useState } from 'react';
import { DailyMenu, DishCategory, BaseMenuItem, ComplexDish, Order } from '../types/types';
import { categoryToUkrainian } from '../utils/utils';
import DaySelector from '../components/DaySelector';
import CategoryFilter from '../components/CategoryFilter';
import DishList from '../components/DishList';
import OrderSummary from '../components/OrderSummary';
import createOrder from '../utils/orderService';

process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';

const MainClientComponent: React.FC<{ initialMenus: DailyMenu[] }> = ({ initialMenus }) => {
  const [menus] = useState<DailyMenu[]>(initialMenus);
  const [selectedDay, setSelectedDay] = useState<string>('Понеділок');
  const [selectedCategory, setSelectedCategory] = useState<DishCategory | ''>('');
  const [selectedDish, setSelectedDish] = useState<BaseMenuItem | ComplexDish | null>(null);
  const [order, setOrder] = useState<Order>({ orderId: 0, dishDTOs: [], totalCost: 0 });

  const categories: DishCategory[] = [
    DishCategory.Main,
    DishCategory.First,
    DishCategory.Side,
    DishCategory.Drink,
    DishCategory.Complex,
  ];

  // Фільтрація страв за днем і категорією
  const filteredDishes = menus
    .find(menu => menu.dayOfWeek === selectedDay)
    ?.baseMenuItems.filter(dish => selectedCategory === '' || dish.category === selectedCategory) || [];

  const addToOrder = (dish: BaseMenuItem | ComplexDish) => {
    setOrder(prev => {
      const alreadyInOrder = prev.dishDTOs.some(d => d.id === dish.id && d.name === dish.name);
      if (alreadyInOrder) return prev;
      return {
        ...prev,
        dishDTOs: [...prev.dishDTOs, dish],
        totalCost: prev.totalCost + dish.price,
      };
    });
  };

  const removeFromOrder = (dish: BaseMenuItem) => {
    const updatedDishes = order.dishDTOs.filter(d => d.id !== dish.id || d.name !== dish.name);
    setOrder({
      ...order,
      dishDTOs: updatedDishes,
      totalCost: updatedDishes.reduce((sum, d) => sum + d.price, 0),
    });
  };

const confirmOrder = async () => {
  const payload = {
    id: 0,
    dishDTOs: order.dishDTOs,
    totalCost: order.totalCost,
  };

  const result = await createOrder(payload);

  if (result.success) {
    alert('Замовлення оформлено!');
    setOrder({ orderId: order.orderId + 1, dishDTOs: [], totalCost: 0 });
  } else {
    alert('Не вдалося оформити замовлення: ' + (result.message || 'Невідома помилка'));
  }
};

  return (
    <div className="min-h-screen bg-gray-100 font-sans">
      <div className="grid grid-cols-4 gap-4 p-4 h-screen">
        <div className="col-span-3 flex flex-col">
          <DaySelector selectedDay={selectedDay} onSelectDay={setSelectedDay} />
          <CategoryFilter
            categories={categories}
            selectedCategory={selectedCategory}
            setSelectedCategory={setSelectedCategory}
            categoryToUkrainian={categoryToUkrainian}
          />
          <DishList
            dishes={filteredDishes}
            selectedDish={selectedDish}
            onSelectDish={setSelectedDish}
            onAddToOrder={addToOrder}
          />
        </div>
        <OrderSummary
          order={order}
          onRemove={removeFromOrder}
          onConfirm={confirmOrder}
        />
      </div>
    </div>
  );
};

export default MainClientComponent;
