import React from 'react';
import { BaseMenuItem, ComplexDish } from '../types/types';
import DishCard from './DishCard';

interface DishListProps {
  dishes: (BaseMenuItem | ComplexDish)[];
  selectedDish: BaseMenuItem | ComplexDish | null;
  onSelectDish: (dish: BaseMenuItem | ComplexDish) => void;
  onAddToOrder: (dish: BaseMenuItem | ComplexDish) => void;
}

const DishList: React.FC<DishListProps> = ({ dishes, selectedDish, onSelectDish, onAddToOrder }) => (
  <div className="grid grid-cols-3 gap-4 p-4 overflow-auto">
    {dishes.map(dish => (
      <DishCard
        key={`${dish.id}-${dish.name}`}
        dish={dish}
        isSelected={selectedDish?.id === dish.id && selectedDish?.name === dish.name}
        onSelect={() => onSelectDish(dish)}
        onAddToOrder={() => onAddToOrder(dish)}
      />
    ))}
  </div>
);

export default DishList;
