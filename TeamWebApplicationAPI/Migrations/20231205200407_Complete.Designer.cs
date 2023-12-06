﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TeamWebApplicationAPI.Data.Database;

#nullable disable

namespace TeamWebApplicationAPI.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20231205200407_Complete")]
    partial class Complete
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CommentId"));

                    b.Property<string>("CommentatorName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CommentatorSurname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserComment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CommentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");

                    b.HasData(
                        new
                        {
                            CommentId = 40000,
                            CommentatorName = "Jonas",
                            CommentatorSurname = "Paguzinskas",
                            CourseId = 10000,
                            CreationTime = new DateTime(2023, 12, 5, 22, 4, 7, 151, DateTimeKind.Local).AddTicks(7869),
                            UserComment = "Sus course",
                            UserId = 20000
                        },
                        new
                        {
                            CommentId = 40001,
                            CommentatorName = "Alita",
                            CommentatorSurname = "Stuknaite",
                            CourseId = 10001,
                            CreationTime = new DateTime(2023, 12, 5, 22, 4, 7, 151, DateTimeKind.Local).AddTicks(7873),
                            UserComment = "good",
                            UserId = 20002
                        },
                        new
                        {
                            CommentId = 40003,
                            CommentatorName = "Alita",
                            CommentatorSurname = "Stuknaite",
                            CourseId = 10000,
                            CreationTime = new DateTime(2023, 12, 5, 22, 4, 7, 151, DateTimeKind.Local).AddTicks(7876),
                            UserComment = "Cool",
                            UserId = 20002
                        });
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CourseId"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 10000,
                            CreationDate = new DateTime(2023, 12, 5, 22, 4, 7, 150, DateTimeKind.Local).AddTicks(3240),
                            Description = "Course for computer architecture",
                            IsPublic = false,
                            IsVisible = true,
                            Name = "Computer Architecture"
                        },
                        new
                        {
                            CourseId = 10001,
                            CreationDate = new DateTime(2023, 12, 5, 22, 4, 7, 150, DateTimeKind.Local).AddTicks(3274),
                            Description = "Course for functional programming",
                            IsPublic = false,
                            IsVisible = false,
                            Name = "Functional Programming"
                        },
                        new
                        {
                            CourseId = 10002,
                            CreationDate = new DateTime(2023, 12, 5, 22, 4, 7, 150, DateTimeKind.Local).AddTicks(3276),
                            Description = "Course for database systems",
                            IsPublic = true,
                            IsVisible = true,
                            Name = "Database Systems"
                        });
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.CourseUser", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("CourseId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CourseUsers");

                    b.HasData(
                        new
                        {
                            CourseId = 10000,
                            UserId = 20000
                        },
                        new
                        {
                            CourseId = 10001,
                            UserId = 20000
                        },
                        new
                        {
                            CourseId = 10000,
                            UserId = 20001
                        },
                        new
                        {
                            CourseId = 10002,
                            UserId = 20001
                        },
                        new
                        {
                            CourseId = 10001,
                            UserId = 20002
                        });
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PostId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PostType")
                        .HasColumnType("integer");

                    b.HasKey("PostId");

                    b.HasIndex("CourseId");

                    b.ToTable("Posts");

                    b.HasDiscriminator<int>("PostType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Rating", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<decimal>("UserRating")
                        .HasColumnType("numeric");

                    b.HasKey("UserId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            UserId = 20000,
                            CourseId = 10000,
                            UserRating = 3m
                        },
                        new
                        {
                            UserId = 20000,
                            CourseId = 10001,
                            UserRating = 5m
                        },
                        new
                        {
                            UserId = 20001,
                            CourseId = 10000,
                            UserRating = 4m
                        },
                        new
                        {
                            UserId = 20002,
                            CourseId = 10000,
                            UserRating = 3m
                        });
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Faculty")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<int>("Specialization")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 20000,
                            Email = "j.paguzinskas@mif.stuf.vu.lt",
                            Faculty = 0,
                            Name = "Jonas",
                            Password = "qweryty1",
                            Role = 0,
                            Specialization = 0,
                            Surname = "Paguzinskas"
                        },
                        new
                        {
                            UserId = 20001,
                            Email = "a.jarasunas@mif.stuf.vu.lt",
                            Faculty = 2,
                            Name = "Armandas",
                            Password = "aludaris17",
                            Role = 0,
                            Specialization = 4,
                            Surname = "Jarasunas"
                        },
                        new
                        {
                            UserId = 20002,
                            Email = "a.stuknyte@mif.stuf.vu.lt",
                            Faculty = 0,
                            Name = "Alita",
                            Password = "metupataikau695",
                            Role = 1,
                            Specialization = 0,
                            Surname = "Stuknaite"
                        });
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.FilePost", b =>
                {
                    b.HasBaseType("TeamWebApplicationAPI.Models.Post");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.ToTable("Posts");

                    b.HasDiscriminator().HasValue(1);

                    b.HasData(
                        new
                        {
                            PostId = 30003,
                            CourseId = 10001,
                            CreationDate = new DateTime(2023, 12, 5, 22, 4, 7, 151, DateTimeKind.Local).AddTicks(7820),
                            IsVisible = true,
                            Name = "File",
                            PostType = 0,
                            FileName = "tvarkarastis.jpg"
                        });
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.TextPost", b =>
                {
                    b.HasBaseType("TeamWebApplicationAPI.Models.Post");

                    b.Property<string>("TextContent")
                        .HasColumnType("text");

                    b.ToTable("Posts");

                    b.HasDiscriminator().HasValue(0);

                    b.HasData(
                        new
                        {
                            PostId = 30000,
                            CourseId = 10000,
                            CreationDate = new DateTime(2023, 12, 5, 22, 4, 7, 151, DateTimeKind.Local).AddTicks(7788),
                            IsVisible = true,
                            Name = "Introduction",
                            PostType = 0,
                            TextContent = "This is a placeholder"
                        },
                        new
                        {
                            PostId = 30001,
                            CourseId = 10002,
                            CreationDate = new DateTime(2023, 12, 5, 22, 4, 7, 151, DateTimeKind.Local).AddTicks(7800),
                            IsVisible = true,
                            Name = "Knowledge",
                            PostType = 0,
                            TextContent = "This is once more a placeholder https://www.youtube.com/watch?v=dQw4w9WgXcQ"
                        });
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Comment", b =>
                {
                    b.HasOne("TeamWebApplicationAPI.Models.Course", null)
                        .WithMany("Comments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWebApplicationAPI.Models.User", null)
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.CourseUser", b =>
                {
                    b.HasOne("TeamWebApplicationAPI.Models.Course", "Course")
                        .WithMany("Users")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWebApplicationAPI.Models.User", "User")
                        .WithMany("Courses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Post", b =>
                {
                    b.HasOne("TeamWebApplicationAPI.Models.Course", "Course")
                        .WithMany("Posts")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Rating", b =>
                {
                    b.HasOne("TeamWebApplicationAPI.Models.Course", "Course")
                        .WithMany("Ratings")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TeamWebApplicationAPI.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.Course", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Posts");

                    b.Navigation("Ratings");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TeamWebApplicationAPI.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Courses");

                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}