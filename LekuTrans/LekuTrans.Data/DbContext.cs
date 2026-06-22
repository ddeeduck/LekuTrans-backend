using Microsoft.EntityFrameworkCore;
using LekuTrans.Data.Models;

namespace LekuTrans.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ClientProfile> ClientProfiles { get; set; }
    public DbSet<CompanyClient> CompanyClients { get; set; }
    public DbSet<IndividualClient> IndividualClients { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<ClientCargo> ClientCargos { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<LoadingInfo> LoadingInfos { get; set; }
    public DbSet<Recipient> Recipients { get; set; }
    public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ClientProfile>()
            .HasOne(cp => cp.User)
            .WithOne(u => u.ClientProfile)
            .HasForeignKey<ClientProfile>(cp => cp.UserId);

        modelBuilder.Entity<CompanyClient>()
            .HasOne(cc => cc.ClientProfile)
            .WithOne(cp => cp.CompanyClient)
            .HasForeignKey<CompanyClient>(cc => cc.UserId);

        modelBuilder.Entity<IndividualClient>()
            .HasOne(ic => ic.ClientProfile)
            .WithOne(cp => cp.IndividualClient)
            .HasForeignKey<IndividualClient>(ic => ic.UserId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.LoadingInfo)
            .WithOne(li => li.Order)
            .HasForeignKey<LoadingInfo>(li => li.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Recipient)
            .WithOne(r => r.Order)
            .HasForeignKey<Recipient>(r => r.OrderId);
    }
}