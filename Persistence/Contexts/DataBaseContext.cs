using Application.Interface.Context;
using Domain.Attributes;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class DataBaseContext : DbContext, IDatabaseContext
    {
        //public DbSet<User> users { get; set; }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

          
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //یک فیلد تعریف کردیم در جدول بدون اینکه در کلاس فیلد تعریف کنیم
            //modelBuilder.Entity<User>().Property<DateTime?>("InsertTime");
            //modelBuilder.Entity<User>().Property<DateTime?>("UpdateTime");

            //مشکلی که کد های بالا داره اینکه اگر شادو پراپرتی ها زیاد باشد باید تکرار کنیم

            //برای این از رفلکشن ها استفاده میکنیم
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {

                if (item.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity(item.Name).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(item.Name).Property<bool>("IsRemove");

                    modelBuilder.Entity(item.Name).Property<DateTime?>("InsertTime");
                    modelBuilder.Entity(item.Name).Property<DateTime?>("UpdateTime");


                }

            }


            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
            //نوع عملیات رو گرفته
            var ModifidEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified ||
            p.State == EntityState.Deleted ||
            p.State == EntityState.Added
            );


            foreach (var item in ModifidEntries)
            {
                var EntityType = item.Context.Model.FindEntityType(item.Entity.GetType());
                var RemoveTime = EntityType.FindProperty("RemoveTime");
                var IsRemove = EntityType.FindProperty("IsRemove");
                var InsertTime = EntityType.FindProperty("InsertTime");
                var UpdateTime = EntityType.FindProperty("UpdateTime");

                if (item.State == EntityState.Deleted && RemoveTime != null && RemoveTime != null)
                {
                    item.Property("IsRemove").CurrentValue = true;
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;

                }
                else if (item.State == EntityState.Added && InsertTime != null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;

                }

                else if (item.State == EntityState.Modified && UpdateTime != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;

                }
            }


            return base.SaveChanges();
        }

    }
}
