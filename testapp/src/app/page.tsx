import { fetchDailyMenus } from './utils/fetchMenus';
import MainClientComponent from './components/MainClientComponent';

export default async function Page() {
  const initialMenus = await fetchDailyMenus();
  return <MainClientComponent initialMenus={initialMenus} />;
}