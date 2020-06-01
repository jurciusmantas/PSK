using PSK.Model.Entities;
using PSK.Model.Repository;
using System.Linq;
using System;
using System.Collections.Generic;

namespace PSK.DB.MockRepository
{
    public class DayMockRepository : IDayRepository
    {
        private static List<Day> _days = new List<Day>();
        public Day Add(Day day)
        {
            day.Id = _days.Count;
            _days.Add(day);
            return day;
        }

        public Day Delete(int id)
        {
            Day day = _days.Find(day => day.Id == id);
            if(day != null)
            {
                _days.Remove(day);
            }
            return day;
        }

        public Day Get(int id)
        {
            return _days.Find(day => day.Id == id);
        }

        public List<Day> Get()
        {
            throw new NotImplementedException();
        }

        public List<Day> GetEmployeeDays(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Day GetEmployeeFeatureDayByTopic(int topicId, int employeeId)
        {
            throw new NotImplementedException();
        }

        public Day Update(Day updatedDay)
        {
            Day day = Delete(updatedDay.Id);
            if(day != null)
            {
                _days.Add(updatedDay);
                _days = _days.OrderBy(day => day.Id).ToList();
            }
            return day;
        }
    }
}
