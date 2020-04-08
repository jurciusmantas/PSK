﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSK.DB.Contexts;

namespace PSK.DB.Migrations
{
    [DbContext(typeof(PSKDbContext))]
    partial class PSKDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PSK.Model.BusinessEntities.AssignedTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmploeeId")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TopicId");

                    b.ToTable("AssignedTopics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmploeeId = 2,
                            IsCompleted = false,
                            TopicId = 1
                        },
                        new
                        {
                            Id = 2,
                            EmploeeId = 4,
                            IsCompleted = false,
                            TopicId = 2
                        },
                        new
                        {
                            Id = 3,
                            CompletedOn = new DateTime(2020, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmploeeId = 5,
                            IsCompleted = true,
                            TopicId = 3
                        },
                        new
                        {
                            Id = 4,
                            EmploeeId = 4,
                            IsCompleted = false,
                            TopicId = 4
                        });
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            Name = "admin",
                            Password = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Email = "vladas@gmail.com",
                            LeaderId = 1,
                            Name = "vladas",
                            Password = "vladas"
                        },
                        new
                        {
                            Id = 3,
                            Email = "ona@gmail.com",
                            LeaderId = 1,
                            Name = "ona",
                            Password = "ona"
                        },
                        new
                        {
                            Id = 4,
                            Email = "ema@gmail.com",
                            LeaderId = 2,
                            Name = "ema",
                            Password = "ema"
                        },
                        new
                        {
                            Id = 5,
                            Email = "matas@gmail.com",
                            LeaderId = 2,
                            Name = "matas",
                            Password = "matas"
                        },
                        new
                        {
                            Id = 6,
                            Email = "zita@gmail.com",
                            LeaderId = 5,
                            Name = "zita",
                            Password = "zita"
                        });
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.EmployeeRestriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("RestrictionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("RestrictionId");

                    b.ToTable("EmployeeRestriction");
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.IncomingEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("LeaderId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.ToTable("IncomingEmployees");
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AssignedTopicId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WorkDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AssignedTopicId");

                    b.ToTable("Plans");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssignedTopicId = 1,
                            WorkDate = new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            AssignedTopicId = 1,
                            WorkDate = new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            AssignedTopicId = 1,
                            WorkDate = new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            AssignedTopicId = 1,
                            WorkDate = new DateTime(2020, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            AssignedTopicId = 2,
                            WorkDate = new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 6,
                            AssignedTopicId = 2,
                            WorkDate = new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 7,
                            AssignedTopicId = 2,
                            WorkDate = new DateTime(2020, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 8,
                            AssignedTopicId = 4,
                            WorkDate = new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 9,
                            AssignedTopicId = 4,
                            WorkDate = new DateTime(2020, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Recommendation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int?>("RecommendedToId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("RecommendedToId");

                    b.HasIndex("TopicId");

                    b.ToTable("Recommendations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatorId = 2,
                            ReceiverId = 4,
                            TopicId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatorId = 2,
                            ReceiverId = 4,
                            TopicId = 4
                        },
                        new
                        {
                            Id = 3,
                            CreatorId = 1,
                            ReceiverId = 2,
                            TopicId = 2
                        });
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Restriction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConsecutiveDays")
                        .HasColumnType("int");

                    b.Property<bool>("Global")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MaxDaysPerMonth")
                        .HasColumnType("int");

                    b.Property<int>("MaxDaysPerQuarter")
                        .HasColumnType("int");

                    b.Property<int>("MaxDaysPerYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Restrictions");
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(12000)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("ParentTopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentTopicId");

                    b.ToTable("Topics");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "This course is about Java EE. Read more in https://www.oracle.com/java/technologies/java-ee-glance.html",
                            Name = "Java EE"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Java EE JPA course: https://www.javaworld.com/article/3379043/what-is-jpa-introduction-to-the-java-persistence-api.html",
                            Name = "Java EE JPA",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Java EE CDI course: https://www.baeldung.com/java-ee-cdi",
                            Name = "Java EE CDI",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 4,
                            Description = "Link https://www.w3schools.com/cs/",
                            Name = "C# tutorials"
                        });
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.AssignedTopic", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("PSK.Model.BusinessEntities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Employee", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId");
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.EmployeeRestriction", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Employee")
                        .WithMany("EmployeeRestrictions")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSK.Model.BusinessEntities.Restriction", "Restriction")
                        .WithMany("RestrictionEmployees")
                        .HasForeignKey("RestrictionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.IncomingEmployee", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Plan", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.AssignedTopic", "AssignedTopic")
                        .WithMany()
                        .HasForeignKey("AssignedTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Recommendation", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("PSK.Model.BusinessEntities.Employee", "RecommendedTo")
                        .WithMany()
                        .HasForeignKey("RecommendedToId");

                    b.HasOne("PSK.Model.BusinessEntities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Topic", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Topic", "ParentTopic")
                        .WithMany()
                        .HasForeignKey("ParentTopicId");
                });
#pragma warning restore 612, 618
        }
    }
}
