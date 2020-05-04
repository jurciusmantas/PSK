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

            modelBuilder.Entity("PSK.Model.BusinessEntities.Day", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TopicId");

                    b.ToTable("Days");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 2,
                            TopicId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 3,
                            TopicId = 1
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 3,
                            TopicId = 1
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2020, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 3,
                            TopicId = 1
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2020, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 4,
                            TopicId = 2
                        },
                        new
                        {
                            Id = 6,
                            Date = new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 4,
                            TopicId = 2
                        },
                        new
                        {
                            Id = 7,
                            Date = new DateTime(2020, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 4,
                            TopicId = 2
                        },
                        new
                        {
                            Id = 8,
                            Date = new DateTime(2020, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 5,
                            TopicId = 4
                        },
                        new
                        {
                            Id = 9,
                            Date = new DateTime(2020, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 5,
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

            modelBuilder.Entity("PSK.Model.BusinessEntities.EmployeesToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiredAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeesTokens");
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

            modelBuilder.Entity("PSK.Model.BusinessEntities.Recommendation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ReceiverId");

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

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
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

                    b.HasIndex("CreatorId");

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
                            ParentTopicId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Java EE CDI course: https://www.baeldung.com/java-ee-cdi",
                            Name = "Java EE CDI",
                            ParentTopicId = 1
                        },
                        new
                        {
                            Id = 4,
                            Description = "Link https://www.w3schools.com/cs/",
                            Name = "C# tutorials"
                        });
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.TopicCompletion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CompletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TopicId");

                    b.ToTable("TopicCompletions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompletedOn = new DateTime(2020, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeId = 5,
                            TopicId = 3
                        });
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Day", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("PSK.Model.BusinessEntities.EmployeesToken", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
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

            modelBuilder.Entity("PSK.Model.BusinessEntities.Recommendation", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSK.Model.BusinessEntities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Restriction", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.Topic", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Topic", "ParentTopic")
                        .WithMany()
                        .HasForeignKey("ParentTopicId");
                });

            modelBuilder.Entity("PSK.Model.BusinessEntities.TopicCompletion", b =>
                {
                    b.HasOne("PSK.Model.BusinessEntities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSK.Model.BusinessEntities.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
