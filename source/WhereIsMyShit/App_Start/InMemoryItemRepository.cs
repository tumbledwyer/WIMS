using System.Collections.Generic;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.App_Start
{
    public interface IItemRepository
    {
        List<ItemModel> GetItems();
        void Add(ItemModel itemModel);
    }

    public class InMemoryItemRepository : IItemRepository
    {
        private readonly List<ItemModel> _itemModels = new List<ItemModel>
        {
            new ItemModel {Name = "guitar"},
            new ItemModel {Name = "book"},
            new ItemModel {Name = "slinky"}
        };


        public List<ItemModel> GetItems()
        {
            return _itemModels;
        }

        public void Add(ItemModel itemModel)
        {
            _itemModels.Add(itemModel);
        }
    }
}