using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.EntityFramework.Convertors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Infrastructure.EntityFramework;
public class DbSetEmployee : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employee");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasConversion<EmployeeIdConvertor>()
            .HasColumnOrder(0)
            .HasColumnName("id");

        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name")
            .IsRequired();

        builder.OwnsOne(e => e.EmailAddress,
        navigationBuilder => navigationBuilder.Property(e => e.Value)
                            .HasColumnName("email")
                            .HasMaxLength(50));
        builder.OwnsOne(e => e.PhoneNumber,
                 navigationBuilder => navigationBuilder.Property(e => e.Value)
                                     .HasColumnName("phone_number")
                                     .HasMaxLength(15));

        builder.Property(e => e.Gender)
           .HasConversion<GenderConvertor>()
           .HasColumnName("gender")
           .HasMaxLength(10)
           .IsRequired();

        builder.Property(e => e.StartDate)
            .HasConversion<DateOnlyConvertor>()
            .HasColumnType("Date");

        builder.Property(e => e.CurrentCafe)
           .HasConversion<CafeIdConvertor>();

        builder.HasOne<Cafe>()
            .WithMany()
            .HasForeignKey(e => e.CurrentCafe);
    }
}
