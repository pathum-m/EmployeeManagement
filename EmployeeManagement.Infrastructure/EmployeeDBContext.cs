using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Domain.Entities;


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
        base.OnModelCreating(modelBuilder); 
    } 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
    }
}
