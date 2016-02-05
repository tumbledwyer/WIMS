using System;
using System.Collections.Generic;
using System.Linq;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.App_Start
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
            return _catalogue.Items.ToList();
            //return new List<LoanItem>();
        }

        public void Add(LoanItem loanItem)
        {
            //if (item == null) return;
            //_catalogue.Items.Add(item);
            //_catalogue.SaveChanges();
        }

        public void Delete(string itemName)
        {
            //var itemWithName = FindByName(itemName);
            //if (itemWithName != null)
            //{
            //    _itemModels.Remove(itemWithName);
            //}
        }

        public LoanItem FindByName(string name)
        {
            //return _catalogue.Items.FirstOrDefault(s => s.Name == name);
            return null;
        }
    }
}