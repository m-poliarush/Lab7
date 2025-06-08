
import { BaseMenuItem, ComplexDish } from "../types/types";
interface CreateOrderPayload {
  dishDTOs: (BaseMenuItem | ComplexDish)[];
  totalCost: number;
}


async function createOrder(order: CreateOrderPayload): Promise<{ success: boolean; message?: string }> {
    console.log(order);
    try {
    const response = await fetch('https://localhost:7210/Orders/CreateOrder', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(order),
    });

    if (!response.ok) {
      // Сервер повернув помилку
      const errorData = await response.json();
      return { success: false, message: errorData.message || 'Помилка сервера' };
    }

    // Успішна відповідь
    return { success: true };
  } catch (error) {
    // Помилка мережі або інша помилка fetch
    return { success: false, message: (error as Error).message || 'Помилка мережі' };
  }
}

export default createOrder;
