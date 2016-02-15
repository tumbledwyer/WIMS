using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WhereIsMyShit.Controllers
{
    public class BorrowersController : Controller
    {
        // GET: Borrower
        public ActionResult Index()
        {
            return View("Add");
        }

        // GET: Borrower/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Borrower/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Borrower/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Borrower/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Borrower/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Borrower/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Borrower/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
