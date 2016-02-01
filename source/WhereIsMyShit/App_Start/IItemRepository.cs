using System.Collections.Generic;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.App_Start
{
    public interface IItemRepository
    {
        List<ItemModel> GetItems();
        void Add(ItemModel item);
        void Delete(string itemName);
        ItemModel FindByName(string name);
    }
}