﻿using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.EntityFramework.Convertors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.EntityFramework;
public class DbSetCafe : IEntityTypeConfiguration<Cafe>
{
    public void Configure(EntityTypeBuilder<Cafe> builder) 
    {
        builder.ToTable("cafe");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion<CafeIdConvertor>()
            .HasColumnOrder(0)
            .HasColumnName("id");

        builder.Property(c => c.Name)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(c => c.Location)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Logo)
            .HasMaxLength(2000);

        builder.HasMany<Employee>()
            .WithOne()
            .HasForeignKey(e => e.CurrentCafe)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            Cafe.Create
            (
                "Cafe ABC",
                "Test edscription ABC",
                "Colombo"
            ).Value,
            Cafe.Create
            (
                "Cafe DEF",
                "Test edscription DEF",
                "Galle"
            ).Value
        );
    }
}
