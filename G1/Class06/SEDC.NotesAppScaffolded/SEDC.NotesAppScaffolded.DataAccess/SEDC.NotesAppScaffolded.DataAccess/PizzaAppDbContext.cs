using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SEDC.NotesAppScaffolded.DataAccess.SEDC.NotesAppScaffolded.DataAccess;

public partial class PizzaAppDbContext : DbContext
{
    public PizzaAppDbContext()
    {
    }

    public PizzaAppDbContext(DbContextOptions<PizzaAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaOrder> PizzaOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\LocalTest;Initial Catalog=PizzaAppDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.Property(e => e.ImageUrl).HasDefaultValueSql("(N'')");
        });

        modelBuilder.Entity<PizzaOrder>(entity =>
        {
            entity.ToTable("PizzaOrder");

            entity.HasIndex(e => e.OrderId, "IX_PizzaOrder_OrderId");

            entity.HasIndex(e => e.PizzaId, "IX_PizzaOrder_PizzaId");

            entity.HasOne(d => d.Order).WithMany(p => p.PizzaOrders).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Pizza).WithMany(p => p.PizzaOrders).HasForeignKey(d => d.PizzaId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
