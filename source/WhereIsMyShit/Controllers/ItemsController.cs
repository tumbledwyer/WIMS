using System.Web.Mvc;
using WhereIsMyShit.Models;
using WhereIsMyShit.Repositories;

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
        public ActionResult Add(LoanItem loanItem)
        {
            if (!ModelState.IsValid) return View("Add", loanItem);
            _itemRepository.Add(loanItem);
            return RedirectToAction("Index");
        }

        public ActionResult Add()
        {
            return View();
        }

        //TODO Make this a post
        public ActionResult Delete(int itemId)
        {
            if (_itemRepository.FindById(itemId) != null)
            {
                _itemRepository.Delete(itemId);
            }
            return RedirectToAction("Index");
        }
    }
}