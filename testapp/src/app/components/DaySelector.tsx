import React from 'react';

interface DaySelectorProps {
  selectedDay: string;
  onSelectDay: (day: string) => void;
}

const days = ['Понеділок', 'Вівторок', 'Середа', 'Четвер', "П'ятниця", 'Субота', 'Неділя'];

const DaySelector: React.FC<DaySelectorProps> = ({ selectedDay, onSelectDay }) => (
  <div className="flex justify-center space-x-2 mb-4">
    {days.map(day => (
      <button
        key={day}
        className={`px-4 py-2 rounded ${
          selectedDay === day ? 'bg-blue-600 text-white' : 'bg-gray-200 text-gray-800'
        } hover:bg-blue-500 hover:text-white`}
        onClick={() => onSelectDay(day)}
      >
        {day}
      </button>
    ))}
  </div>
);

export default DaySelector;
