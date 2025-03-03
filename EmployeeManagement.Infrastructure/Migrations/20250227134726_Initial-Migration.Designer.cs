﻿// <auto-generated />
using System;
using EmployeeManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeManagement.Infrastructure.Migrations
{
    [DbContext(typeof(EmployeeDBContext))]
    [Migration("20250227134726_Initial-Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeManagement.Domain.Entities.Cafe", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("location");

                    b.Property<string>("Logo")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("logo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_cafe");

                    b.ToTable("cafe", (string)null);
                });

            modelBuilder.Entity("EmployeeManagement.Domain.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("id")
                        .HasColumnOrder(0);

                    b.Property<Guid?>("CafeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("cafe_id");

                    b.Property<Guid?>("CurrentCafe")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("current_cafe");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("Date")
                        .HasColumnName("start_date");

                    b.HasKey("Id")
                        .HasName("pk_employee");

                    b.HasIndex("CafeId")
                        .HasDatabaseName("ix_employee_cafe_id");

                    b.HasIndex("CurrentCafe")
                        .HasDatabaseName("ix_employee_current_cafe");

                    b.ToTable("employee", (string)null);
                });

            modelBuilder.Entity("EmployeeManagement.Domain.Entities.Employee", b =>
                {
                    b.HasOne("EmployeeManagement.Domain.Entities.Cafe", null)
                        .WithMany("Employees")
                        .HasForeignKey("CafeId")
                        .HasConstraintName("fk_employee_cafe_cafe_id");

                    b.HasOne("EmployeeManagement.Domain.Entities.Cafe", null)
                        .WithMany()
                        .HasForeignKey("CurrentCafe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_employee_cafe_current_cafe");

                    b.OwnsOne("EmployeeManagement.Domain.ValueObjects.Email", "EmailAddress", b1 =>
                        {
                            b1.Property<string>("EmployeeId")
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("email");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId")
                                .HasConstraintName("fk_employee_employee_id");
                        });

                    b.OwnsOne("EmployeeManagement.Domain.ValueObjects.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<string>("EmployeeId")
                                .HasColumnType("nvarchar(450)")
                                .HasColumnName("id");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("nvarchar(15)")
                                .HasColumnName("phone_number");

                            b1.HasKey("EmployeeId");

                            b1.ToTable("employee");

                            b1.WithOwner()
                                .HasForeignKey("EmployeeId")
                                .HasConstraintName("fk_employee_employee_id");
                        });

                    b.Navigation("EmailAddress")
                        .IsRequired();

                    b.Navigation("PhoneNumber")
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeManagement.Domain.Entities.Cafe", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
