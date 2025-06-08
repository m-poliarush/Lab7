'use client';

import React from 'react';
import { BaseMenuItem, ComplexDish } from '../types/types';
import { categoryToUkrainian } from '../utils/utils';

interface Props {
  dish: BaseMenuItem | ComplexDish;
  isSelected: boolean;
  onSelect: () => void;
  onAddToOrder: () => void;
}

const DishCard: React.FC<Props> = ({ dish, isSelected, onSelect, onAddToOrder }) => {
  const isComplexDish = 'dishList' in dish;
  const description = isComplexDish
    ? dish.dishList.map((d) => d.name).join(', ')
    : dish.description;

  return (
    <div
      className={`w-64 h-40 border-2 rounded-lg p-4 bg-white cursor-pointer
        ${isSelected ? 'border-blue-600' : 'border-gray-200'}`}
      onClick={onSelect}
    >
      <div className="grid grid-rows-4 h-full">
        <h3 className="font-semibold  text-black">{dish.name}</h3>
        <p className="text-sm text-gray-600">{categoryToUkrainian(dish.category)}</p>
        <p className="text-sm text-gray-600 truncate">{description}</p>
        <div className="flex justify-between items-center">
          <span className="text-blue-600 font-semibold">{dish.price} грн</span>
          <button
            className="bg-blue-600 text-white px-3 py-1 rounded hover:bg-blue-700"
            onClick={(e) => {
              e.stopPropagation();
              onAddToOrder();
            }}
          >
            Додати
          </button>
        </div>
      </div>
    </div>
  );
};

export default DishCard;
