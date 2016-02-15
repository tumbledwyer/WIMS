using System;
using System.Collections.Generic;
using System.Linq;
using WhereIsMyShit.DbContexts;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly IWimsDbContext _dbContext;

        public ItemRepository(IWimsDbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;
        }

        public List<LoanItem> GetItems()
        {
            return _dbContext.LoanItems.ToList();
        }

        public void Add(LoanItem loanItem)
        {
            _dbContext.LoanItems.Add(loanItem);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var loanItem = FindById(id);
            if (loanItem == null) return;
            _dbContext.LoanItems.Remove(loanItem);
            _dbContext.SaveChanges();
        }

        public LoanItem FindByName(string name)
        {
            return _dbContext.LoanItems.SingleOrDefault(s => s.Name == name);
        }

        public LoanItem FindById(int id)
        {
            return _dbContext.LoanItems.SingleOrDefault(i => i.Id == id);
        }
    }
}