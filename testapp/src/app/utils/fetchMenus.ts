import { DailyMenu } from '../types/types';
process.env.NODE_TLS_REJECT_UNAUTHORIZED = '0';
export async function fetchDailyMenus(): Promise<DailyMenu[]> {
  const response = await fetch("https://localhost:7210/Menus/GetMenus");

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  return response.json();
}