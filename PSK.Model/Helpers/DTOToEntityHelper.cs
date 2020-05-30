using System;

namespace PSK.Model.Helpers
{
    public static class DTOToEntityHelper
    {
        public static Entities.Day DTOToEntity(this DTO.Day day)
        {
            DateTime date = DateTime.Parse(day.Date);
            return new Entities.Day()
            {
                Id = day.Id,
                Date = date,
                EmployeeId = day.EmployeeId,
                TopicId = day.TopicId,
            };
        }

        public static Entities.Recommendation DTOToEntity(this DTO.Recommendation dto)
        {
            return new Entities.Recommendation()
            {
                Id = dto.Id,
                CreatorId = dto.CreatorId,
                ReceiverId = dto.ReceiverId,
                TopicId = dto.TopicId
            };
        }

        public static Entities.Restriction DTOToEntity(this DTO.RestrictionArgs restrictionArgs)
        {
            return new Entities.Restriction
            {
                ConsecutiveDays = restrictionArgs.ConsecutiveDays,
                MaxDaysPerMonth = restrictionArgs.MaxDaysPerMonth,
                MaxDaysPerQuarter = restrictionArgs.MaxDaysPerQuarter,
                MaxDaysPerYear = restrictionArgs.MaxDaysPerYear,
                Global = false,
                CreatorId = restrictionArgs.CreatorId,
                CreationDate = DateTime.Now,
            };
        }
    }
}
