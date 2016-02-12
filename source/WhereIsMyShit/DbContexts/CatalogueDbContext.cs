﻿using System.Data.Common;
using System.Data.Entity;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.DbContexts
{
    public interface ICatalogueDbContext
    {
        IDbSet<LoanItem> Items { get; set; }
        void SaveChanges();
    }

    public class CatalogueDbContext : DbContext, ICatalogueDbContext
    {
        public CatalogueDbContext(DbConnection connection ) : base(connection, false)
        {
            
        }
        public CatalogueDbContext() : base("DefaultConnection")
        {
            
        }

        public virtual IDbSet<LoanItem> Items { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}