using System;
using System.Data;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.NameTranslation;
using Ottobo.Entities;

namespace Ottobo.Infrastructure.Data.PostgreSql
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);
            //modelBuilder.ForNpgsqlUseSequenceHiLo();
            modelBuilder.UseHiLo();
            

            //MASTER DATA FLUENT API
            modelBuilder.Entity<MasterData>()
                .HasOne(s => s.PurchaseType)
                .WithMany(e => e.MasterDataList)
                .HasForeignKey(a => a.PurchaseTypeId);
            
            //ORDER FLUENT API
            modelBuilder.Entity<Order>()
                .HasOne(e => e.OrderType)
                .WithMany(s => s.OrderList)
                .HasForeignKey(e => e.OrderTypeId);
            
            //ORDER DETAIL FLUENT API
            modelBuilder.Entity<OrderDetail>()
                .HasOne(e => e.Stock)
                .WithMany(s => s.OrderDetailList)
                .HasForeignKey(e => e.StockId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(e => e.Order)
                .WithMany(e => e.OrderDetailList)
                .HasForeignKey(e => e.OrderId);
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(e => e.RobotTask)
                .WithMany(e => e.OrderDetailList)
                .HasForeignKey(e => e.RobotTaskId);
            
            //ROBOT TASK FLUENT API
            modelBuilder.Entity<RobotTask>()
                .HasOne(e => e.Robot)
                .WithMany(s => s.RobotTaskList)
                .HasForeignKey(e => e.RobotId);
            
            
            //STOCK FLUENT API...
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.StockType)
                .WithMany(e => e.StockList)
                .HasForeignKey(s => s.StockTypeId);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Location)
                .WithMany(e => e.StockList)
                .HasForeignKey(s => s.LocationId);
            
            
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.MasterData)
                .WithOne(e => e.Stock)
                .HasForeignKey<Stock>(s => s.MasterDataId);
            
            
            modelBuilder.Entity<User>()
                .HasOne(e => e.Role)
                .WithMany(s => s.Users)
                .HasForeignKey(e => e.RoleId);


            ApplySnakeCaseNames(modelBuilder);


            SeedData(modelBuilder);


        }

        private void ApplySnakeCaseNames(ModelBuilder modelBuilder)
        {
            var mapper = new NpgsqlSnakeCaseNameTranslator();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // modify column names
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(mapper.TranslateMemberName(property.GetColumnName()));
                }

                // modify table name
                entity.SetTableName(mapper.TranslateMemberName(entity.GetTableName()));

                // move asp_net tables into schema 'identity'
                if (entity.GetTableName().StartsWith("asp_net_"))
                {
                    entity.SetTableName(entity.GetTableName().Replace("asp_net_", string.Empty));
                    entity.SetSchema("identity");
                }
            }
        }

        public void SeedData(ModelBuilder modelBuilder)
        {


        }

        public DbSet<Location> Location { get; set; }
         
         public DbSet<MasterData> MasterData { get; set; }
         
         public DbSet<Order> Orders { get; set; }
         
         public DbSet<OrderDetail> OrderDetails { get; set; }
         
         public DbSet<OrderType> OrderTypes { get; set; }
         
         public DbSet<PurchaseType> PurchaseTypes { get; set; }
         
         public DbSet<Robot> Robot { get; set; }
         
         public DbSet<RobotTask> RobotTask { get; set; }

         public DbSet<Stock> Stocks { get; set; }
         
         public DbSet<StockType> StockTypes { get; set; }
         
         public DbSet<ApiLog> ApiLogs { get; set; }
         
         public DbSet<User> Users { get; set; }
         
         public DbSet<Role> Roles { get; set; }
         
    }
}