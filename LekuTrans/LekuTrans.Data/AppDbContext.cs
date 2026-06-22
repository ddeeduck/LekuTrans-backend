using Microsoft.EntityFrameworkCore;
using LekuTrans.Data.Models;

namespace LekuTrans.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
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

        modelBuilder.Entity<ClientCargo>()
            .HasOne(cc => cc.Client)
            .WithMany(cp => cp.ClientCargos)
            .HasForeignKey(cc => cc.ClientId);

        modelBuilder.Entity<ClientCargo>()
            .HasOne(cc => cc.Cargo)
            .WithMany(c => c.ClientCargos)
            .HasForeignKey(cc => cc.CargoId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany(cp => cp.Orders)
            .HasForeignKey(o => o.ClientId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.ClientCargo)
            .WithMany(cc => cc.Orders)
            .HasForeignKey(o => o.ClientCargoId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.LoadingInfo)
            .WithOne(li => li.Order)
            .HasForeignKey<LoadingInfo>(li => li.OrderId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Recipient)
            .WithOne(r => r.Order)
            .HasForeignKey<Recipient>(r => r.OrderId);

        modelBuilder.Entity<OrderStatusHistory>()
            .HasOne(h => h.Order)
            .WithMany(o => o.StatusHistory)
            .HasForeignKey(h => h.OrderId);

        modelBuilder.Entity<OrderStatusHistory>()
            .HasOne(h => h.ChangedByUser)
            .WithMany(u => u.StatusHistoryChanges)
            .HasForeignKey(h => h.ChangedBy);

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Order)
            .WithMany(o => o.Assignments)
            .HasForeignKey(a => a.OrderId);

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Vehicle)
            .WithMany(v => v.Assignments)
            .HasForeignKey(a => a.VehicleId);

        modelBuilder.Entity<Assignment>()
            .HasOne(a => a.Driver)
            .WithMany(d => d.Assignments)
            .HasForeignKey(a => a.DriverId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Order)
            .WithMany(o => o.Reviews)
            .HasForeignKey(r => r.OrderId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Client)
            .WithMany(cp => cp.Reviews)
            .HasForeignKey(r => r.ClientId);

        modelBuilder.Entity<Feedback>()
            .HasOne(f => f.User)
            .WithMany(u => u.Feedbacks)
            .HasForeignKey(f => f.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<ClientProfile>()
            .Property(cp => cp.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Cargo>()
            .Property(c => c.CargoType)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .Property(o => o.PaymentStatus)
            .HasConversion<string>();

        modelBuilder.Entity<LoadingInfo>()
            .Property(li => li.LoadingType)
            .HasConversion<string>();

        modelBuilder.Entity<Vehicle>()
            .Property(v => v.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Driver>()
            .Property(d => d.Status)
            .HasConversion<string>();
    }
}