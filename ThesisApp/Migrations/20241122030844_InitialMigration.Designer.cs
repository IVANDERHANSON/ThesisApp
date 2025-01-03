﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThesisApp.Data;

#nullable disable

namespace ThesisApp.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241122030844_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ThesisApp.Models.MentorPair", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("MentorLecturerId")
                        .HasColumnType("int");

                    b.Property<int>("PreThesisId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("MentorLecturerId")
                        .IsUnique();

                    b.HasIndex("PreThesisId")
                        .IsUnique();

                    b.ToTable("MentorPairs");
                });

            modelBuilder.Entity("ThesisApp.Models.MentoringSession", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("MeetingLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MentorPairId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Schedule")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("MentorPairId");

                    b.ToTable("MentoringSessions");
                });

            modelBuilder.Entity("ThesisApp.Models.PreThesis", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("PreThesisLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreThesisName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("PreTheses");
                });

            modelBuilder.Entity("ThesisApp.Models.Thesis", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("ThesisLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThesisName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Theses");
                });

            modelBuilder.Entity("ThesisApp.Models.ThesisDefence", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("ExaminerLecturerId")
                        .HasColumnType("int");

                    b.Property<string>("MeetingLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MentorLecturerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Schedule")
                        .HasColumnType("datetime2");

                    b.Property<int>("ThesisId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ExaminerLecturerId")
                        .IsUnique();

                    b.HasIndex("MentorLecturerId")
                        .IsUnique();

                    b.HasIndex("ThesisId")
                        .IsUnique();

                    b.ToTable("ThesisDefences");
                });

            modelBuilder.Entity("ThesisApp.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ThesisApp.Models.MentorPair", b =>
                {
                    b.HasOne("ThesisApp.Models.User", "MentorLecturer")
                        .WithOne("MentorPair")
                        .HasForeignKey("ThesisApp.Models.MentorPair", "MentorLecturerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ThesisApp.Models.PreThesis", "PreThesis")
                        .WithOne("MentorPair")
                        .HasForeignKey("ThesisApp.Models.MentorPair", "PreThesisId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("MentorLecturer");

                    b.Navigation("PreThesis");
                });

            modelBuilder.Entity("ThesisApp.Models.MentoringSession", b =>
                {
                    b.HasOne("ThesisApp.Models.MentorPair", "MentorPair")
                        .WithMany("MentoringSessions")
                        .HasForeignKey("MentorPairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MentorPair");
                });

            modelBuilder.Entity("ThesisApp.Models.PreThesis", b =>
                {
                    b.HasOne("ThesisApp.Models.User", "Student")
                        .WithOne("PreThesis")
                        .HasForeignKey("ThesisApp.Models.PreThesis", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ThesisApp.Models.Thesis", b =>
                {
                    b.HasOne("ThesisApp.Models.User", "Student")
                        .WithOne("Thesis")
                        .HasForeignKey("ThesisApp.Models.Thesis", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ThesisApp.Models.ThesisDefence", b =>
                {
                    b.HasOne("ThesisApp.Models.User", "ExaminerLecturer")
                        .WithOne("ThesisDefenceForExaminer")
                        .HasForeignKey("ThesisApp.Models.ThesisDefence", "ExaminerLecturerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ThesisApp.Models.User", "MentorLecturer")
                        .WithOne("ThesisDefenceForMentor")
                        .HasForeignKey("ThesisApp.Models.ThesisDefence", "MentorLecturerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ThesisApp.Models.Thesis", "Thesis")
                        .WithOne("ThesisDefence")
                        .HasForeignKey("ThesisApp.Models.ThesisDefence", "ThesisId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ExaminerLecturer");

                    b.Navigation("MentorLecturer");

                    b.Navigation("Thesis");
                });

            modelBuilder.Entity("ThesisApp.Models.MentorPair", b =>
                {
                    b.Navigation("MentoringSessions");
                });

            modelBuilder.Entity("ThesisApp.Models.PreThesis", b =>
                {
                    b.Navigation("MentorPair")
                        .IsRequired();
                });

            modelBuilder.Entity("ThesisApp.Models.Thesis", b =>
                {
                    b.Navigation("ThesisDefence")
                        .IsRequired();
                });

            modelBuilder.Entity("ThesisApp.Models.User", b =>
                {
                    b.Navigation("MentorPair")
                        .IsRequired();

                    b.Navigation("PreThesis")
                        .IsRequired();

                    b.Navigation("Thesis")
                        .IsRequired();

                    b.Navigation("ThesisDefenceForExaminer")
                        .IsRequired();

                    b.Navigation("ThesisDefenceForMentor")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
