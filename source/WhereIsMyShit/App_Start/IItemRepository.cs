using System.Collections.Generic;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.App_Start
{
    public interface IItemRepository
    {
        List<LoanItem> GetItems();
        void Add(LoanItem loanItem);
        void Delete(int id);
        LoanItem FindByName(string name);
        LoanItem FindById(int itemId);
    }
}