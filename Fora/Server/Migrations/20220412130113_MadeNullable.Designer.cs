﻿// <auto-generated />
using System;
using Fora.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fora.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220412130113_MadeNullable")]
    partial class MadeNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Fora.Shared.InterestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Interests");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Games"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sports"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Politics"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Religion"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Design"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Garden"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Technology"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Pets"
                        });
                });

            modelBuilder.Entity("Fora.Shared.MessageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThreadId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Fora.Shared.ThreadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InterestId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InterestId");

                    b.HasIndex("UserId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Fora.Shared.UserInterestModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("InterestId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "InterestId");

                    b.HasIndex("InterestId");

                    b.ToTable("UserInterestModel");
                });

            modelBuilder.Entity("Fora.Shared.UserModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Banned")
                        .HasColumnType("bit");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fora.Shared.InterestModel", b =>
                {
                    b.HasOne("Fora.Shared.UserModel", "User")
                        .WithMany("Interests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fora.Shared.MessageModel", b =>
                {
                    b.HasOne("Fora.Shared.ThreadModel", "Thread")
                        .WithMany("Messages")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fora.Shared.UserModel", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Thread");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fora.Shared.ThreadModel", b =>
                {
                    b.HasOne("Fora.Shared.InterestModel", "Interest")
                        .WithMany("Threads")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fora.Shared.UserModel", "User")
                        .WithMany("Threads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Interest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fora.Shared.UserInterestModel", b =>
                {
                    b.HasOne("Fora.Shared.InterestModel", "Interest")
                        .WithMany("UserInterests")
                        .HasForeignKey("InterestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fora.Shared.UserModel", "User")
                        .WithMany("UserInterests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interest");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Fora.Shared.InterestModel", b =>
                {
                    b.Navigation("Threads");

                    b.Navigation("UserInterests");
                });

            modelBuilder.Entity("Fora.Shared.ThreadModel", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Fora.Shared.UserModel", b =>
                {
                    b.Navigation("Interests");

                    b.Navigation("Messages");

                    b.Navigation("Threads");

                    b.Navigation("UserInterests");
                });
#pragma warning restore 612, 618
        }
    }
}
