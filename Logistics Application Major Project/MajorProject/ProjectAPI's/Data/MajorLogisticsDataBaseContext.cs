using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProjectAPI_s.Models;

namespace ProjectAPI_s.Data;

public partial class MajorLogisticsDataBaseContext : DbContext
{
    public MajorLogisticsDataBaseContext()
    {
    }

    public MajorLogisticsDataBaseContext(DbContextOptions<MajorLogisticsDataBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Assign> Assigns { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartView> CartViews { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<DriverDetailsView> DriverDetailsViews { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderDetailsView> OrderDetailsViews { get; set; }

    public virtual DbSet<Resource> Resources { get; set; }

    public virtual DbSet<ResourcesView> ResourcesViews { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<ShipmentView> ShipmentViews { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__719FE488AB9352E6");

            entity.Property(e => e.AdminName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.AdminPassword)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Assign>(entity =>
        {
            entity.HasKey(e => e.AssignedId).HasName("PK__Assign__4A3E391998AC9D25");

            entity.ToTable("Assign", tb => tb.HasTrigger("assign_trigger"));

            entity.Property(e => e.AssignedId).ValueGeneratedNever();
            entity.Property(e => e.Destination)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.VehicleNum)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.DriverDetailsNavigation).WithMany(p => p.Assigns)
                .HasForeignKey(d => d.DriverDetails)
                .HasConstraintName("FK__Assign__DriverDe__403A8C7D");

            entity.HasOne(d => d.Order).WithMany(p => p.Assigns)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Assign__OrderID__412EB0B6");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD7B757B68DCA");

            entity.ToTable("Cart");

            entity.Property(e => e.ItemPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPriceOfItem)
                .HasComputedColumnSql("([ItemPrice]*[ItemQuantity])", false)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Cart__CustomerId__367C1819");

            entity.HasOne(d => d.Item).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Cart__ItemId__37703C52");
        });

        modelBuilder.Entity<CartView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CartView");

            entity.Property(e => e.ItemImg).IsUnicode(false);
            entity.Property(e => e.ItemName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalPriceOfItem).HasColumnType("decimal(21, 2)");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_Pk");

            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerPassword)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CustomerPhoneNo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CustomerWallet).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.DriverId).HasName("PK__Drivers__F1B1CD044FC07DBD");

            entity.ToTable(tb => tb.HasTrigger("resourse_driver_add_trigger"));

            entity.Property(e => e.DriverName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.DriverPassword)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.DriverPhoneNo)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DriverDetailsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DriverDetailsView");

            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Destination)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.DriverName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("Inventory_ItemId_Pk");

            entity.ToTable("Inventory");

            entity.Property(e => e.ItemImg).IsUnicode(false);
            entity.Property(e => e.ItemName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Price)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WarehouseLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Hyderabad")
                .HasColumnName("Warehouse_location");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.ManagerId).HasName("PK__Managers__3BA2AAE1B6DBC81D");

            entity.Property(e => e.ManagerName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ManagerPassword)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ManagerPhonoNo)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFBD2DC303");

            entity.Property(e => e.Destination)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.OrderPlacedDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("Order Placed");
            entity.Property(e => e.TotalBillAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("totalBillAmount");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__300424B4");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC0761F0A878");

            entity.ToTable(tb => tb.HasTrigger("Quantity_Trigger"));

            entity.Property(e => e.ExpectedDelivery).HasColumnName("expectedDelivery");
            entity.Property(e => e.ItemPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalItemPrice)
                .HasComputedColumnSql("([ItemQuantity]*[ItemPrice])", false)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__OrderDeta__ItemI__65370702");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__6442E2C9");
        });

        modelBuilder.Entity<OrderDetailsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OrderDetailsView");

            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Destination)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.ExpectedDelivery).HasColumnName("expectedDelivery");
            entity.Property(e => e.ItemImg).IsUnicode(false);
            entity.Property(e => e.ItemName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ItemPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalItemPrice).HasColumnType("decimal(21, 2)");
        });

        modelBuilder.Entity<Resource>(entity =>
        {
            entity.HasKey(e => e.ResourceId).HasName("PK__Resource__4ED1814F71F50729");

            entity.Property(e => e.ResourceId)
                .ValueGeneratedNever()
                .HasColumnName("ResourceID");
            entity.Property(e => e.DriverAvailabilityStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("yes");
            entity.Property(e => e.VehicleAllocated)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.CurrentAssignmentNavigation).WithMany(p => p.Resources)
                .HasForeignKey(d => d.CurrentAssignment)
                .HasConstraintName("FK__Resources__Curre__5AB9788F");

            entity.HasOne(d => d.Driver).WithMany(p => p.Resources)
                .HasForeignKey(d => d.DriverId)
                .HasConstraintName("FK__Resources__Drive__58D1301D");
        });

        modelBuilder.Entity<ResourcesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ResourcesView");

            entity.Property(e => e.Currentassignment).HasColumnName("currentassignment");
            entity.Property(e => e.DriverAvailabilityStatus)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.DriverName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.DriverPhoneNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ResourceId).HasColumnName("ResourceID");
            entity.Property(e => e.VehicleAllocated)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.ShipmentId).HasName("PK__Shipment__5CAD37ED81563DC9");

            entity.ToTable(tb => tb.HasTrigger("updatetriggerOnOrders"));

            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Ordered");
            entity.Property(e => e.Destination)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Hyderabad");
        });

        modelBuilder.Entity<ShipmentView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ShipmentView");

            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Destination)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Origin)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B5492D80056CA");

            entity.Property(e => e.VehicleAvailabilityStatus)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasDefaultValue("yes");
            entity.Property(e => e.VehicleName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
