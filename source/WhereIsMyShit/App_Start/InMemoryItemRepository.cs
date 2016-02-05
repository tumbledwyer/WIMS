using System.Collections.Generic;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.App_Start
{
    public class InMemoryItemRepository : IItemRepository
    {
        private readonly List<LoanItem> _itemModels = new List<LoanItem>();

        public List<LoanItem> GetItems()
        {
            return _itemModels;
        }

        public void Add(LoanItem loanItem)
        {
            _itemModels.Add(loanItem);
        }

        public void Delete(string itemName)
        {
            var itemWithName = FindByName(itemName);
            if (itemWithName != null)
            {
                _itemModels.Remove(itemWithName);
            }
        }

        public LoanItem FindByName(string name)
        {
            return _itemModels.Find(s => s.Name == name);
        }
    }
}