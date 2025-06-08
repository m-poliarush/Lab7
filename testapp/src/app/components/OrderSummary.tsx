import React from 'react';
import { Order, BaseMenuItem } from '../types/types';
import OrderItem from './OrderItem';

interface OrderSummaryProps {
  order: Order;
  onRemove: (dish: BaseMenuItem) => void;
  onConfirm: () => void;
}

const OrderSummary: React.FC<OrderSummaryProps> = ({ order, onRemove, onConfirm }) => (
  <div className="col-span-1 bg-gray-50 p-4 rounded-lg flex flex-col">
    <h2 className="text-xl text-black font-semibold mb-4">Замовлення</h2>
    <div className="flex-1 overflow-auto space-y-2">
      {order.dishDTOs.map((dish, index) => (
        <OrderItem
          key={`${dish.id}-${dish.name}-${index}`}
          dish={dish}
          onRemove={() => onRemove(dish)}
        />
      ))}
    </div>
    <div className="mt-4">
      <p className="text-gray-600 font-semibold">Загальна ціна:</p>
      <p className="text-blue-600 text-xl font-bold">{order.totalCost} грн</p>
    </div>
    <button
      className="bg-blue-600 text-white px-4 py-3 rounded mt-4 hover:bg-blue-700"
      onClick={onConfirm}
    >
      Оформити замовлення
    </button>
  </div>
);

export default OrderSummary;
