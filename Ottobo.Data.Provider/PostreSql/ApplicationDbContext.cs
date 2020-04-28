using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.NameTranslation;
using Ottobo.Entities;

namespace Ottobo.Data.Provider.PostgreSql
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long, IdentityUserClaim<long>, IdentityUserRole<long>, IdentityUserLogin<long>, IdentityRoleClaim<long>, IdentityUserToken<long>>
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

            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole<long>>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim<long>>().ToTable("UsersClaim");
            modelBuilder.Entity<IdentityRoleClaim<long>>().ToTable("RolesClaim");

            modelBuilder.Entity<IdentityUserLogin<long>>().ToTable("UsersLogin");
            modelBuilder.Entity<IdentityUserRole<long>>().ToTable("UsersRole");
            modelBuilder.Entity<IdentityUserToken<long>>().ToTable("UsersToken");


            //STOCK FLUENT API...
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.StockType)
                .WithMany(e => e.Stocks)
                .HasForeignKey(e => e.StockTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            //ORDER DETAIL FLUENT API
            modelBuilder.Entity<OrderDetail>()
                .HasOne(s => s.OrderType)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.OrderTypeId)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(s => s.Order)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            modelBuilder.Entity<OrderDetail>()
                .HasOne(s => s.Stock)
                .WithMany(e => e.OrderDetails)
                .HasForeignKey(e => e.StockId)
                .OnDelete(DeleteBehavior.Cascade);
                
                


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


         public DbSet<StockType> StockTypes { get; set; }
         public DbSet<OrderType> OrderTypes { get; set; }
         
         public DbSet<Stock> Stocks { get; set; }

         public DbSet<Order> Orders { get; set; }

         public DbSet<OrderDetail> OrderDetails { get; set; }


    }
}