using System;
using System.Collections.Generic;
using System.Linq;
using WhereIsMyShit.DbContexts;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ICatalogueDbContext _catalogue;

        public ItemRepository(ICatalogueDbContext catalogue)
        {
            if (catalogue == null) throw new ArgumentNullException(nameof(catalogue));
            _catalogue = catalogue;
        }

        public List<LoanItem> GetItems()
        {
            return _catalogue.LoanItems.ToList();
        }

        public void Add(LoanItem loanItem)
        {
            _catalogue.LoanItems.Add(loanItem);
            _catalogue.SaveChanges();
        }

        public void Delete(int id)
        {
            var loanItem = FindById(id);
            if (loanItem == null) return;
            _catalogue.LoanItems.Remove(loanItem);
            _catalogue.SaveChanges();
        }

        public LoanItem FindByName(string name)
        {
            return _catalogue.LoanItems.SingleOrDefault(s => s.Name == name);
        }

        public LoanItem FindById(int id)
        {
            return _catalogue.LoanItems.SingleOrDefault(i => i.Id == id);
        }
    }
}