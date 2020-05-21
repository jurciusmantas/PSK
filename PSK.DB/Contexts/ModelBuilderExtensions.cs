using Microsoft.EntityFrameworkCore;
using PSK.Model.Entities;
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
                    ParentTopicId = 1
                },
                new Topic
                {
                    Id = 3,
                    Name = "Java EE CDI",
                    Description = "Java EE CDI course: https://www.baeldung.com/java-ee-cdi",
                    ParentTopicId = 1
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
            modelBuilder.Entity<TopicCompletion>().HasData(
                new TopicCompletion
                {
                    Id = 1,
                    CompletedOn = new DateTime(2020, 4, 7),
                    TopicId = 3,
                    EmployeeId = 5
                }
                );
            modelBuilder.Entity<Day>().HasData(
                new Day
                {
                    Id = 1,
                    Date = new DateTime(2020, 5, 11),
                    TopicId = 1,
                    EmployeeId = 2,   
                },
                new Day
                {
                    Id = 2,
                    Date = new DateTime(2020, 5, 12),
                    TopicId = 1,
                    EmployeeId = 3,
                },
                new Day
                {
                    Id = 3,
                    Date = new DateTime(2020, 5, 14),
                    TopicId = 1,
                    EmployeeId = 3
                },
                new Day
                {
                    Id = 4,
                    Date = new DateTime(2020, 5, 18),
                    TopicId = 1,
                    EmployeeId = 3
                },
                new Day
                {
                    Id = 5,
                    Date = new DateTime(2020, 5, 11),
                    TopicId = 2,
                    EmployeeId = 4
                },
                new Day
                {
                    Id = 6,
                    Date = new DateTime(2020, 5, 12),
                    TopicId = 2,
                    EmployeeId = 4
                },
                new Day
                {
                    Id = 7,
                    Date = new DateTime(2020, 5, 13),
                    TopicId = 2,
                    EmployeeId = 4
                },
                new Day
                {
                    Id = 8,
                    Date = new DateTime(2020, 6, 8),
                    TopicId = 4,
                    EmployeeId = 5
                },
                new Day
                {
                    Id = 9,
                    Date = new DateTime(2020, 6, 9),
                    TopicId = 4,
                    EmployeeId = 5
                }
                );
        }
    }
}
