﻿using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class DatabaseContext:DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().Property<DateTime?>("InsertTime");
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute),true).Length > 0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("InsertTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(entityType.Name).Property<bool>("IsRemoved");
                }
            }
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Added || 
                p.State == EntityState.Modified || 
                p.State == EntityState.Deleted);

            foreach (var item in modifiedEntities)
            {
                var entityType = item.Context.Model.FindEntityType(item.GetType());

                var inserted = entityType.FindProperty("InsertTime");
                var updated = entityType.FindProperty("UpdateTime");
                var removed = entityType.FindProperty("RemoveTime");
                var isRemoved = entityType.FindProperty("IsRemoved");

                if (item.State == EntityState.Added && inserted is not null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                else if (item.State == EntityState.Modified && updated is not null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                else if (item.State == EntityState.Deleted && removed is not null && isRemoved is not null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;

                }
            }
            return base.SaveChanges();
        }
    }
}