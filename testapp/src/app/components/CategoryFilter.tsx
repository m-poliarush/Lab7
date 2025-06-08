import React from 'react';
import { DishCategory } from '../types/types';

interface CategoryFilterProps {
  categories: DishCategory[];
  selectedCategory: DishCategory | '';
  setSelectedCategory: (cat: DishCategory | '') => void;
  categoryToUkrainian: (cat: DishCategory) => string;
}

const CategoryFilter: React.FC<CategoryFilterProps> = ({
  categories,
  selectedCategory,
  setSelectedCategory,
  categoryToUkrainian,
}) => (
  <div className="flex text-black items-center mb-4 ml-6">
    <span className="mr-2">Фільтр категорій:</span>
    <select
      className="border rounded px-2 py-1 w-36 mr-2"
      value={selectedCategory}
      onChange={e =>
        setSelectedCategory(e.target.value === '' ? '' : Number(e.target.value) as DishCategory)
      }
    >
      <option value="">Усі категорії</option>
      {categories.map(cat => (
        <option key={cat} value={cat}>
          {categoryToUkrainian(cat)}
        </option>
      ))}
    </select>
    <button
      className="bg-gray-300 text-gray-800 px-4 py-1 rounded hover:bg-gray-400"
      onClick={() => setSelectedCategory('')}
    >
      Скинути фільтр
    </button>
  </div>
);

export default CategoryFilter;
