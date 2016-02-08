using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WhereIsMyShit.Models;

namespace WhereIsMyShit
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