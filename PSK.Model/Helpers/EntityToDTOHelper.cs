namespace PSK.Model.Helpers
{
    public static class EntityToDTOHelper
    {
        public static DTO.Day ToDTO(this Entities.Day day)
        {
            return new DTO.Day()
            {
                Id = day.Id,
                EmployeeId = day.EmployeeId,
                EmployeeName = day.Employee.Name,
                TopicId = day.TopicId,
                TopicName = day.Topic.Name,
                Date = day.Date.ToShortDateString()
            };
        }

        public static DTO.Recommendation ToDTO(this Entities.Recommendation entity)
        {
            return new DTO.Recommendation()
            {
                Id = entity.Id,
                ReceiverId = entity.ReceiverId,
                CreatorId = entity.CreatorId,
                TopicId = entity.TopicId,
                CreatorName = entity.Creator?.Name,
                ReceiverName = entity.Receiver?.Name,
                TopicName = entity.Topic?.Name
            };
        }

        public static DTO.Employee ToDTO(this Entities.Employee entity)
        {
            return new DTO.Employee()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                LeaderId = entity.LeaderId
            };
        }

        public static DTO.Topic ToDTO(this Entities.Topic topic)
        {
            return new DTO.Topic
            {
                Id = topic.Id,
                Name = topic.Name,
                Description = topic.Description,
                ParentId = topic.ParentTopicId,
            };
        }
    }
}
