using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using WhereIsMyShit.Controllers;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.App_Start
{
    public class Bootstrapper
    {
        private static WindsorContainer _container;

        public static void Init()
        {
            _container = new WindsorContainer();
            _container.Register(Component.For<IItemRepository>().ImplementedBy<ItemRepository>());
            _container.Register(Component.For(typeof(ItemsController)).UsingFactoryMethod(CreateItemsController).LifestylePerWebRequest());
        }

        private static ItemsController CreateItemsController()
        {
            return new ItemsController(_container.Resolve<IItemRepository>());
            
        }
    }

    public class ItemRepository : IItemRepository
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
    }

    public interface IItemRepository
    {
        List<ItemModel> GetItems();
    }
}