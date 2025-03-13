using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.ValueObjects;

namespace EmployeeManagement.Infrastructure;
public class EmployeeDBContext : DbContext
{
    public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) 
        : base(options) { }
    
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Cafe> Cafe { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        SeedData(modelBuilder);
        base.OnModelCreating(modelBuilder); 
    } 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var employee1Id = EmployeeId.GenerateID();
        var employee2Id = EmployeeId.GenerateID();

        // Seed employee records
        modelBuilder.Entity<Employee>().HasData(
            new
            {
                Id = employee1Id,
                Name = "Employee_1",
                Gender = Gender.Male,
            },
            new
            {
                Id = employee2Id,
                Name = "Employee_2",
                Gender = Gender.Female,
            }
        );


        modelBuilder.Entity<Employee>()
            .OwnsOne(e => e.EmailAddress)
            .HasData(
                new
                {
                    EmployeeId = employee1Id,
                    Value = "abc@mail.com"
                },
                new
                {
                    EmployeeId = employee2Id,
                    Value = "edf@mail.com"
                }
            );

        modelBuilder.Entity<Employee>()
            .OwnsOne(e => e.PhoneNumber)
            .HasData(
                new
                {
                    EmployeeId = employee1Id,
                    Value = "97654321"
                },
                new
                {
                    EmployeeId = employee2Id,
                    Value = "87654321"
                }
            );
    }
}
