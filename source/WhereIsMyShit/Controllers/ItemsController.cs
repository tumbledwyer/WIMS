using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhereIsMyShit.App_Start;
using WhereIsMyShit.Models;

namespace WhereIsMyShit.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemRepository _itemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // GET: Items
        public ActionResult Index()
        {
            return View(_itemRepository.GetItems());
        }

        [HttpPost]
        public ActionResult Add(ItemModel itemModel)
        {
            if (!ModelState.IsValid) return View("Add", itemModel);
            _itemRepository.Add(itemModel);
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        //TODO Make this a post
        [HttpPost]
        public ActionResult Delete(string itemName)
        {
            if (_itemRepository.FindByName(itemName) != null)
            {
                _itemRepository.Delete(itemName);
            }
            return RedirectToAction("Index");
        }
    }
}