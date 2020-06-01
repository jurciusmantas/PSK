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
                },
                new Employee
                {
                    Id = 7, 
                    Name = "gerda",
                    Email = "gerda@gmail.com",
                    Password = "gNtzUNfmP4zx17KZqhZOVrRZq7LlEtjihxgifaD++coEGt4R", //new
                    LeaderId = 1
                },
                new Employee
                {
                    Id = 8,
                    Name = "jonas",
                    Email = "jonas@gmail.com",
                    Password = "dwuCXS1FIDzjkkQ+9KlFKdwZf7zwMzwChWj5TaqrZklsu8NS",  //new
                    LeaderId = 1
                },
                new Employee
                {
                    Id = 9,
                    Name = "elena",
                    Email = "elena@gmail.com",
                    Password = "CB4ltXA0mmqJy/8y5xq+cNqBJ95ykdJ17JT1amz1JuMHTpke",  //new
                    LeaderId = 2
                },
                new Employee
                {
                    Id = 10,
                    Name = "vytas",
                    Email = "vytas@gmail.com",
                    Password = "WJhLLap3r2gIReQyuqplu/UpE8LCjg7gCquuKomeEnVNhhL8",  //new
                    LeaderId = 1
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
                },
                new Topic
                {
                    Id = 5,
                    Name = "Java EE JPA.Introduction",
                    Description = "Link https://www.javaworld.com/article/3379043/what-is-jpa-introduction-to-the-java-persistence-api.html",
                    ParentTopicId = 2
                },
                new Topic
                {
                    Id = 6,
                    Name = "Java EE JPA.CRUD Example",
                    Description = "Link http://www.thejavageek.com/2014/01/12/jpa-crud-example/",
                    ParentTopicId = 2
                },
                new Topic
                {
                    Id = 7,
                    Name = "C# LINQ",
                    Description = "Link https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/",
                    ParentTopicId = 4
                },
                new Topic
                {
                    Id = 8,
                    Name = "C# Entity Framework",
                    Description = "Documentation: https://docs.microsoft.com/en-us/ef/",
                    ParentTopicId = 4,
                },
                new Topic
                {
                    Id = 9,
                    Name = "C# EF Code first",
                    Description = "Link https://docs.microsoft.com/en-us/ef/ef6/modeling/code-first/workflows/new-database",
                    ParentTopicId = 8
                },
                new Topic
                {
                    Id = 10,
                    Name = "C# EF DB first",
                    Description = "Link https://docs.microsoft.com/en-us/ef/ef6/modeling/designer/workflows/database-first",
                    ParentTopicId = 8
                },
                new Topic
                {
                    Id = 11,
                    Name = "SQL",
                    Description = "Link https://www.w3schools.com/sql/",
                },
                new Topic
                {
                    Id = 12,
                    Name = "MySQL",
                    Description = "Link https://www.mysql.com/",
                    ParentTopicId = 11
                },
                new Topic
                {
                    Id = 13,
                    Name = "PostgreSQL",
                    Description = "Link https://www.postgresql.org/",
                    ParentTopicId = 11
                },
                new Topic
                {
                    Id = 14,
                    Name = "MySQL DB creation",
                    Description = "Link https://dev.mysql.com/doc/refman/8.0/en/creating-database.html",
                    ParentTopicId = 12
                },
                new Topic
                {
                    Id = 15,
                    Name = "MySQL Triggers",
                    Description = "Link https://dev.mysql.com/doc/refman/8.0/en/trigger-syntax.html",
                    ParentTopicId = 12
                },
                new Topic
                {
                    Id = 16,
                    Name = "MySQL Queries",
                    Description = "Link https://dev.mysql.com/doc/mysql-tutorial-excerpt/8.0/en/examples.html",
                    ParentTopicId = 12
                },
                new Topic
                {
                    Id = 17,
                    Name = "MySQL Select",
                    Description = "Link https://dev.mysql.com/doc/refman/8.0/en/select.html",
                    ParentTopicId = 16
                },
                new Topic
                {
                    Id = 18,
                    Name = "MySQL Insert Into",
                    Description = "Link https://www.w3schools.com/sql/sql_insert.asp",
                    ParentTopicId = 16
                },
                new Topic
                {
                    Id = 19,
                    Name = "MySQL Update",
                    Description = "Link https://www.w3schools.com/sql/sql_update.asp",
                    ParentTopicId = 16
                },
                new Topic
                {
                    Id = 20,
                    Name = "MySQL Select with Join",
                    Description = "Link https://www.w3schools.com/sql/sql_join.asp",
                    ParentTopicId = 17
                },
                new Topic
                {
                    Id = 21,
                    Name = "MySQL Select with Limit",
                    Description = "Link https://www.w3schools.com/php/php_mysql_select_limit.asp",
                    ParentTopicId = 17
                },
                new Topic
                {
                    Id = 22,
                    Name = "MySQL Delete",
                    Description = "Link https://dev.mysql.com/doc/refman/8.0/en/delete.html",
                    ParentTopicId = 16
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
                },
                new Recommendation
                {
                    Id = 4,
                    TopicId = 5,
                    ReceiverId = 3,
                    CreatorId = 1
                },
                new Recommendation
                {
                    Id = 5,
                    TopicId = 6,
                    ReceiverId = 4,
                    CreatorId = 1
                },
                new Recommendation
                {
                    Id = 6,
                    TopicId = 7,
                    ReceiverId = 5,
                    CreatorId = 2
                },
                new Recommendation
                {
                    Id = 7,
                    TopicId = 8,
                    ReceiverId = 9,
                    CreatorId = 1
                },
                new Recommendation
                {
                    Id = 8,
                    TopicId = 8,
                    ReceiverId = 8,
                    CreatorId = 1
                },
                new Recommendation
                {
                    Id = 9,
                    TopicId = 8, 
                    ReceiverId = 2,
                    CreatorId = 1
                },
                new Recommendation
                {
                    Id = 10,
                    TopicId = 9,
                    ReceiverId = 3,
                    CreatorId = 1
                },
                new Recommendation
                {
                    Id = 11,
                    TopicId = 10,
                    ReceiverId = 5,
                    CreatorId = 2
                },
                new Recommendation
                {
                    Id = 12,
                    TopicId = 14,
                    ReceiverId = 5,
                    CreatorId = 2
                }
        
                );
            modelBuilder.Entity<TopicCompletion>().HasData(
                new TopicCompletion
                {
                    Id = 1,
                    CompletedOn = new DateTime(2020, 4, 7),
                    TopicId = 3,
                    EmployeeId = 5
                },
                new TopicCompletion
                {
                    Id = 2,
                    CompletedOn = new DateTime(2020, 6, 2),
                    TopicId = 4,
                    EmployeeId = 3
                },
                new TopicCompletion
                {
                    Id = 3,
                    CompletedOn = new DateTime(2020, 5, 23),
                    TopicId = 3,
                    EmployeeId = 2
                },
                new TopicCompletion
                {
                    Id = 4,
                    CompletedOn = new DateTime(2020, 5, 24),
                    TopicId = 3,
                    EmployeeId = 3
                },
                new TopicCompletion
                {
                    Id = 5,
                    CompletedOn = new DateTime(2020, 5, 25),
                    TopicId = 3,
                    EmployeeId = 4
                },
                new TopicCompletion
                {
                    Id = 6,
                    CompletedOn = new DateTime(2020, 5, 26),
                    TopicId = 20,
                    EmployeeId = 5
                },
                new TopicCompletion
                {
                    Id = 7,
                    CompletedOn = new DateTime(2020, 5, 27),
                    TopicId = 19,
                    EmployeeId = 2
                },
                new TopicCompletion
                {
                    Id = 8,
                    CompletedOn = new DateTime(2020, 5, 27),
                    TopicId = 18,
                    EmployeeId = 3
                },
                new TopicCompletion
                {
                    Id = 9,
                    CompletedOn = new DateTime(2020, 5, 20),
                    TopicId = 4,
                    EmployeeId = 5
                },
                new TopicCompletion
                {
                    Id = 10,
                    CompletedOn = new DateTime(2020, 5, 15),
                    TopicId = 5,
                    EmployeeId = 3
                },
                new TopicCompletion
                {
                    Id = 11,
                    CompletedOn = new DateTime(2020, 5, 16),
                    TopicId = 3,
                    EmployeeId = 2
                },
                new TopicCompletion
                {
                    Id = 12,
                    CompletedOn = new DateTime(2020, 5, 17),
                    TopicId = 3,
                    EmployeeId = 3
                },
                new TopicCompletion
                {
                    Id = 13,
                    CompletedOn = new DateTime(2020, 5, 29),
                    TopicId = 3,
                    EmployeeId = 4
                },
                new TopicCompletion
                {
                    Id = 14,
                    CompletedOn = new DateTime(2020, 5, 26),
                    TopicId = 20,
                    EmployeeId = 5
                },
                new TopicCompletion
                {
                    Id = 15,
                    CompletedOn = new DateTime(2020, 5, 7),
                    TopicId = 14,
                    EmployeeId = 2
                },
                new TopicCompletion
                {
                    Id = 16,
                    CompletedOn = new DateTime(2020, 5, 6),
                    TopicId = 13,
                    EmployeeId = 3
                }
                );
            modelBuilder.Entity<Day>().HasData(
                new Day
                {
                    Id = 1,
                    Date = new DateTime(2020, 6, 11),
                    TopicId = 1,
                    EmployeeId = 2,   
                },
                new Day
                {
                    Id = 2,
                    Date = new DateTime(2020, 6, 12),
                    TopicId = 1,
                    EmployeeId = 3,
                },
                new Day
                {
                    Id = 3,
                    Date = new DateTime(2020, 6, 14),
                    TopicId = 1,
                    EmployeeId = 3
                },
                new Day
                {
                    Id = 4,
                    Date = new DateTime(2020, 6, 18),
                    TopicId = 1,
                    EmployeeId = 3
                },
                new Day
                {
                    Id = 5,
                    Date = new DateTime(2020, 6, 11),
                    TopicId = 2,
                    EmployeeId = 4
                },
                new Day
                {
                    Id = 6,
                    Date = new DateTime(2020, 6, 12),
                    TopicId = 2,
                    EmployeeId = 4
                },
                new Day
                {
                    Id = 7,
                    Date = new DateTime(2020, 6, 13),
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
                },
                new Day
                {
                    Id = 10,
                    Date = new DateTime(2020, 6, 11),
                    TopicId = 1,
                    EmployeeId = 9,
                },
                new Day
                {
                    Id = 11,
                    Date = new DateTime(2020, 6, 12),
                    TopicId = 1,
                    EmployeeId = 9,
                },
                new Day
                {
                    Id = 12,
                    Date = new DateTime(2020, 6, 14),
                    TopicId = 1,
                    EmployeeId = 1
                },
                new Day
                {
                    Id = 13,
                    Date = new DateTime(2020, 6, 18),
                    TopicId = 1,
                    EmployeeId = 1
                },
                new Day
                {
                    Id = 14,
                    Date = new DateTime(2020, 6, 11),
                    TopicId = 2,
                    EmployeeId = 2
                },
                new Day
                {
                    Id = 15,
                    Date = new DateTime(2020, 6, 12),
                    TopicId = 2,
                    EmployeeId = 1
                },
                new Day
                {
                    Id = 16,
                    Date = new DateTime(2020, 6, 13),
                    TopicId = 16,
                    EmployeeId = 2
                },
                new Day
                {
                    Id = 17,
                    Date = new DateTime(2020, 5, 8),
                    TopicId = 4,
                    EmployeeId = 3
                },
                new Day
                {
                    Id = 18,
                    Date = new DateTime(2020, 6, 9),
                    TopicId = 4,
                    EmployeeId = 5
                },
                new Day
                {
                    Id = 19,
                    Date = new DateTime(2020, 5, 23),
                    TopicId = 3,
                    EmployeeId = 8
                },
                new Day
                {
                    Id = 20,
                    Date = new DateTime(2020, 5, 24),
                    TopicId = 3,
                    EmployeeId = 3
                },
                new Day
                {
                    Id = 21,
                    Date = new DateTime(2020, 5, 25),
                    TopicId = 3,
                    EmployeeId = 4
                },
                new Day
                {
                    Id = 22,
                    Date = new DateTime(2020, 5, 26),
                    TopicId = 20,
                    EmployeeId = 5
                },
                new Day
                {
                    Id = 23,
                    Date = new DateTime(2020, 5, 27),
                    TopicId = 19,
                    EmployeeId = 2
                },
                new Day
                {
                    Id = 24,
                    Date = new DateTime(2020, 5, 27),
                    TopicId = 18,
                    EmployeeId = 3
                },
                new Day
                {
                    Id = 25,
                    Date = new DateTime(2020, 5, 20),
                    TopicId = 4,
                    EmployeeId = 5
                },
                new Day
                {
                    Id = 26,
                    Date = new DateTime(2020, 5, 15),
                    TopicId = 5,
                    EmployeeId = 1
                },
                new Day
                {
                    Id = 27,
                    Date = new DateTime(2020, 5, 16),
                    TopicId = 3,
                    EmployeeId = 2
                },
                new Day
                {
                    Id = 28,
                    Date = new DateTime(2020, 5, 17),
                    TopicId = 3,
                    EmployeeId = 2
                },
                new Day
                {
                    Id = 29,
                    Date = new DateTime(2020, 5, 29),
                    TopicId = 3,
                    EmployeeId = 4
                },
                new Day
                {
                    Id = 30,
                    Date = new DateTime(2020, 5, 26),
                    TopicId = 20,
                    EmployeeId = 5
                },
                new Day
                {
                    Id = 31,
                    Date = new DateTime(2020, 5, 7),
                    TopicId = 14,
                    EmployeeId = 2
                },
                new Day
                {
                    Id = 32,
                    Date = new DateTime(2020, 5, 6),
                    TopicId = 13,
                    EmployeeId = 1
                }
                );
            modelBuilder.Entity<Restriction>().HasData(
                new Restriction
                {
                    Id = 1,
                    ConsecutiveDays = 3,
                    MaxDaysPerYear = 36, 
                    MaxDaysPerQuarter = 9,
                    MaxDaysPerMonth = 3,
                    Global = true,
                    CreationDate = new DateTime(2020, 3, 8),
                    CreatorId = 1,
                },
                new Restriction
                {
                    Id = 2,
                    ConsecutiveDays = 3,
                    MaxDaysPerYear = 48,
                    MaxDaysPerQuarter = 12,
                    MaxDaysPerMonth = 4,
                    Global = false,
                    CreationDate = new DateTime(2020, 5, 8),
                    CreatorId = 1
                },
                new Restriction
                {
                    Id = 3,
                    ConsecutiveDays = 3,
                    MaxDaysPerYear = 30,
                    MaxDaysPerQuarter = 16,
                    MaxDaysPerMonth = 3,
                    Global = false,
                    CreationDate = new DateTime(2020, 5, 9),
                    CreatorId = 1
                },
                new Restriction
                {
                    Id = 4,
                    ConsecutiveDays = 5,
                    MaxDaysPerYear = 60,
                    MaxDaysPerQuarter = 15,
                    MaxDaysPerMonth = 5,
                    Global = false,
                    CreationDate = new DateTime(2020, 5, 10),
                    CreatorId = 2
                }
            );
            modelBuilder.Entity<EmployeeRestriction>().HasData(
                new EmployeeRestriction
                {
                    Id = 1,
                    RestrictionId = 1,
                    EmployeeId = 2
                },
                new EmployeeRestriction
                {
                    Id = 2,
                    RestrictionId = 1,
                    EmployeeId = 3
                },
                new EmployeeRestriction
                {
                    Id = 3,
                    RestrictionId = 1,
                    EmployeeId = 4
                },
                new EmployeeRestriction
                {
                    Id = 4,
                    RestrictionId = 1,
                    EmployeeId = 5
                },
                new EmployeeRestriction
                {
                    Id = 5,
                    RestrictionId = 1,
                    EmployeeId = 6
                },
                new EmployeeRestriction
                {
                    Id = 6,
                    RestrictionId = 1,
                    EmployeeId = 7
                },
                new EmployeeRestriction
                {
                    Id = 7,
                    RestrictionId = 1,
                    EmployeeId = 8
                },
                new EmployeeRestriction
                {
                    Id = 8,
                    RestrictionId = 1,
                    EmployeeId = 9
                },
                new EmployeeRestriction
                {
                    Id = 9,
                    RestrictionId = 1,
                    EmployeeId = 10
                },
                new EmployeeRestriction
                {
                    Id = 10,
                    RestrictionId = 2,
                    EmployeeId = 2,
                },
                new EmployeeRestriction
                {
                    Id = 11,
                    RestrictionId = 2,
                    EmployeeId = 3
                },
                new EmployeeRestriction
                {
                    Id = 12,
                    RestrictionId = 2,
                    EmployeeId = 7
                },
                new EmployeeRestriction
                {
                    Id = 13,
                    RestrictionId = 2,
                    EmployeeId = 10
                },
                new EmployeeRestriction
                {
                    Id = 14,
                    RestrictionId = 3,
                    EmployeeId = 2
                },
                new EmployeeRestriction
                {
                    Id = 15,
                    RestrictionId = 3,
                    EmployeeId = 10
                },
                new EmployeeRestriction
                {
                    Id = 16,
                    RestrictionId = 4,
                    EmployeeId = 4
                },
                new EmployeeRestriction
                {
                    Id = 17,
                    RestrictionId = 4,
                    EmployeeId = 5
                },
                new EmployeeRestriction
                {
                    Id = 18,
                    RestrictionId = 4,
                    EmployeeId = 9
                }
            );
        }
    }
}
