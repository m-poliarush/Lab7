'use client';

import React from 'react';
import { BaseMenuItem } from '../types/types';

interface Props {
  dish: BaseMenuItem;
  onRemove: () => void;
}

const OrderItem: React.FC<Props> = ({ dish, onRemove }) => (
  <div className="border border-gray-200 rounded-lg p-3 bg-white flex justify-between items-center">
    <span className="text-sm text-black">{dish.name}</span>
    <div className="flex items-center">
      <span className="text-blue-600 font-semibold mr-3">{dish.price} грн</span>
      <button
        className="bg-red-500 text-white w-6 h-6 rounded-full flex items-center justify-center"
        onClick={onRemove}
      >
        ✕
      </button>
    </div>
  </div>
);

export default OrderItem;
