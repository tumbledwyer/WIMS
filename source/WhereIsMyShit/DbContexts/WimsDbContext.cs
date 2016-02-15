using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.DbContexts
{
    public interface IWimsDbContext
    {
        IDbSet<LoanItem> LoanItems { get; set; }
        IDbSet<Borrower> Borrowers { get; set; }
        void SaveChanges();
    }

    public class WimsDbContext : DbContext, IWimsDbContext
    {
        static WimsDbContext()
        {
            Database.SetInitializer<WimsDbContext>(null);
        }
        public WimsDbContext(DbConnection connection ) : base(connection, false)
        {
            
        }
        public WimsDbContext() : base("DefaultConnection")
        {
            
        }

        public virtual IDbSet<LoanItem> LoanItems { get; set; }
        public virtual IDbSet<Borrower> Borrowers { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}