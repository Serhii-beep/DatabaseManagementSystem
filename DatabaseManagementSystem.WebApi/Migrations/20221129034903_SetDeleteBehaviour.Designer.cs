﻿// <auto-generated />
using DatabaseManagementSystem.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseManagementSystem.WebApi.Migrations
{
    [DbContext(typeof(DBMSDbContext))]
    [Migration("20221129034903_SetDeleteBehaviour")]
    partial class SetDeleteBehaviour
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Database", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Databases");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DefaultValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Row", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.Property<string>("Values")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.ToTable("Rows");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Table", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DatabaseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DatabaseId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Field", b =>
                {
                    b.HasOne("DatabaseManagementSystem.WebApi.Models.Table", "Table")
                        .WithMany("Fields")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Row", b =>
                {
                    b.HasOne("DatabaseManagementSystem.WebApi.Models.Table", "Table")
                        .WithMany("Rows")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Table");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Table", b =>
                {
                    b.HasOne("DatabaseManagementSystem.WebApi.Models.Database", "Database")
                        .WithMany("Tables")
                        .HasForeignKey("DatabaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Database");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Database", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("DatabaseManagementSystem.WebApi.Models.Table", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("Rows");
                });
#pragma warning restore 612, 618
        }
    }
}
