import { DishCategory } from '../types/types';

export const categoryToUkrainian = (category: DishCategory): string => {
  const map: Record<DishCategory, string> = {
    [DishCategory.Main]: 'Основна страва',
    [DishCategory.First]: 'Перша страва',
    [DishCategory.Side]: 'Гарнір',
    [DishCategory.Drink]: 'Напій',
    [DishCategory.Complex]: 'Комплексне',
  };
  return map[category];
};