import { DailyMenu } from "./types/types";
export async function fetchDailyMenus(): Promise<DailyMenu[]> {
  const response = await fetch("https://localhost:7210/Menus/GetMenus");

  if (!response.ok) {
    throw new Error(`HTTP error! Status: ${response.status}`);
  }

  return response.json();
}