using System.Data.Common;
using System.Data.Entity;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.DbContexts
{
    public interface ICatalogueDbContext
    {
        IDbSet<LoanItem> LoanItems { get; set; }
        void SaveChanges();
    }

    public class CatalogueDbContext : DbContext, ICatalogueDbContext
    {
        static CatalogueDbContext()
        {
            Database.SetInitializer<CatalogueDbContext>(null);
        }
        public CatalogueDbContext(DbConnection connection ) : base(connection, false)
        {
            
        }
        public CatalogueDbContext() : base("DefaultConnection")
        {
            
        }

        public virtual IDbSet<LoanItem> LoanItems { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public System.Data.Entity.DbSet<WhereIsMyShit.Models.Borrower> Borrowers { get; set; }
    }
}