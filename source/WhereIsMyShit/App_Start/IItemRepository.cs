using System.Collections.Generic;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.App_Start
{
    public interface IItemRepository
    {
        List<LoanItem> GetItems();
        void Add(LoanItem loanItem);
        void Delete(string itemName);
        LoanItem FindByName(string name);
    }
}