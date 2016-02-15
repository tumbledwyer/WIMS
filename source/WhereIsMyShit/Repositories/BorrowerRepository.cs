using System;
using System.Collections.Generic;
using System.Linq;
using WhereIsMyShit.DbContexts;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.Repositories
{
    public interface IBorrowerRepository
    {
    }

    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly IWimsDbContext _dbContext;

        public BorrowerRepository(IWimsDbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;
        }

        public List<Borrower> GetItems()
        {
            return _dbContext.Borrowers.ToList();
        }
    }
}