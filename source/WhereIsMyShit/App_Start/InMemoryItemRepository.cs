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

    public class InMemoryItemRepository : IItemRepository
    {
        private readonly List<ItemModel> _itemModels = new List<ItemModel>();

        public List<ItemModel> GetItems()
        {
            return _itemModels;
        }

        public void Add(ItemModel item)
        {
            _itemModels.Add(item);
        }

        public void Delete(string itemName)
        {
            var itemWithName = FindByName(itemName);
            if (itemWithName != null)
            {
                _itemModels.Remove(itemWithName);
            }
        }

        public ItemModel FindByName(string name)
        {
            return _itemModels.Find(s => s.Name == name);
        }
    }
}