using Microsoft.EntityFrameworkCore;
using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.DB.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "admin",
                    Email = "admin@gmail.com",
                    Password = "admin"
                },
                new Employee
                {
                    Id = 2,
                    Name = "vladas",
                    Email = "vladas@gmail.com",
                    Password = "vladas",
                    LeaderId = 1
                },
                new Employee
                {
                    Id = 3,
                    Name = "ona",
                    Email = "ona@gmail.com",
                    Password = "ona",
                    LeaderId = 1
                },
                new Employee
                {
                    Id = 4,
                    Name = "ema",
                    Email = "ema@gmail.com",
                    Password = "ema",
                    LeaderId = 2
                },
                new Employee
                {
                    Id = 5,
                    Name = "matas",
                    Email = "matas@gmail.com",
                    Password = "matas",
                    LeaderId = 2
                },
                new Employee
                {
                    Id = 6,
                    Name = "zita",
                    Email = "zita@gmail.com",
                    Password = "zita",
                    LeaderId = 5
                }
                );
            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    Id = 1,
                    Name = "Java EE",
                    Description = "This course is about Java EE. Read more in https://www.oracle.com/java/technologies/java-ee-glance.html",
                },
                new Topic
                {
                    Id = 2,
                    Name = "Java EE JPA",
                    Description = "Java EE JPA course: https://www.javaworld.com/article/3379043/what-is-jpa-introduction-to-the-java-persistence-api.html",
                    ParentId = 1
                },
                new Topic
                {
                    Id = 3,
                    Name = "Java EE CDI",
                    Description = "Java EE CDI course: https://www.baeldung.com/java-ee-cdi",
                    ParentId = 1
                },
                new Topic
                {
                    Id = 4,
                    Name = "C# tutorials",
                    Description = "Link https://www.w3schools.com/cs/"
                }
                );
            modelBuilder.Entity<Recommendation>().HasData(
                new Recommendation
                {
                    Id = 1,
                    TopicId = 1,
                    ReceiverId = 4,
                    CreatorId = 2
                },
                new Recommendation
                {
                    Id = 2,
                    TopicId = 4,
                    ReceiverId = 4,
                    CreatorId = 2
                },
                new Recommendation
                {
                    Id = 3,
                    TopicId = 2,
                    ReceiverId = 2,
                    CreatorId = 1
                }
                );
            modelBuilder.Entity<AssignedTopic>().HasData(
                new AssignedTopic
                {
                    Id = 1,
                    IsCompleted = false,
                    TopicId = 1,
                    EmploeeId = 2
                },
                new AssignedTopic
                {
                    Id = 2,
                    IsCompleted = false,
                    TopicId = 2,
                    EmploeeId = 4
                },
                new AssignedTopic
                {
                    Id = 3,
                    IsCompleted = true,
                    CompletedOn = new DateTime(2020, 4, 7),
                    TopicId = 3,
                    EmploeeId = 5
                },
                new AssignedTopic
                {
                    Id = 4,
                    IsCompleted = false,
                    TopicId = 4,
                    EmploeeId = 4
                }
                ) ;
            modelBuilder.Entity<Plan>().HasData(
                new Plan
                {
                    Id = 1,
                    WorkDate = new DateTime(2020, 5, 11),
                    AssignedTopicId = 1
                },
                new Plan
                {
                    Id = 2,
                    WorkDate = new DateTime(2020, 5, 12),
                    AssignedTopicId = 1
                },
                new Plan
                {
                    Id = 3,
                    WorkDate = new DateTime(2020, 5, 14),
                    AssignedTopicId = 1
                },
                new Plan
                {
                    Id = 4,
                    WorkDate = new DateTime(2020, 5, 18),
                    AssignedTopicId = 1
                },
                new Plan
                {
                    Id = 5,
                    WorkDate = new DateTime(2020, 5, 11),
                    AssignedTopicId = 2
                },
                new Plan
                {
                    Id = 6,
                    WorkDate = new DateTime(2020, 5, 12),
                    AssignedTopicId = 2
                },
                new Plan
                {
                    Id = 7,
                    WorkDate = new DateTime(2020, 5, 13),
                    AssignedTopicId = 2
                },
                new Plan
                {
                    Id = 8,
                    WorkDate = new DateTime(2020, 6, 8),
                    AssignedTopicId = 4
                },
                new Plan
                {
                    Id = 9,
                    WorkDate = new DateTime(2020, 6, 9),
                    AssignedTopicId = 4
                }
                );
        }
    }
}
