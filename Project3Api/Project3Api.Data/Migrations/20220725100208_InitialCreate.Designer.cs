﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project3Api.Data;

#nullable disable

namespace Project3Api.Data.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20220725100208_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project3Api.Core.Entities.Desk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GridPositionX")
                        .HasColumnType("int");

                    b.Property<int>("GridPositionY")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Orientation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Desks");

                    b.HasCheckConstraint("CK_Desk_Orientation", "[Orientation] >= 0 AND [Orientation] <= 3");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.DeskAllocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DeskId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReservedFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReservedUntil")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DeskId");

                    b.HasIndex("UserId");

                    b.ToTable("DeskAllocation");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.Resource", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.ResourceAllocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AllocatedFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("AllocatedUntil")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ResourceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.HasIndex("UserId");

                    b.ToTable("ResourceAllocation");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasCheckConstraint("CK_Users_Role", "[Role] >= 0 AND [Role] <= 1");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.DeskAllocation", b =>
                {
                    b.HasOne("Project3Api.Core.Entities.Desk", "Desk")
                        .WithMany("DeskAllocations")
                        .HasForeignKey("DeskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project3Api.Core.Entities.User", "User")
                        .WithMany("DeskAllocations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Desk");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.ResourceAllocation", b =>
                {
                    b.HasOne("Project3Api.Core.Entities.Resource", "Resource")
                        .WithMany("ResourceAllocations")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project3Api.Core.Entities.User", "User")
                        .WithMany("ResourceAllocations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resource");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.Desk", b =>
                {
                    b.Navigation("DeskAllocations");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.Resource", b =>
                {
                    b.Navigation("ResourceAllocations");
                });

            modelBuilder.Entity("Project3Api.Core.Entities.User", b =>
                {
                    b.Navigation("DeskAllocations");

                    b.Navigation("ResourceAllocations");
                });
#pragma warning restore 612, 618
        }
    }
}
