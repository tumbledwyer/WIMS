using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhereIsMyShit.Models;
using WhereIsMyShit.Repositories;

namespace WhereIsMyShit.Controllers
{
    public class BorrowersController : Controller
    {
        private readonly IBorrowerRepository _repository;

        public BorrowersController(IBorrowerRepository repository)
        {
            _repository = repository;
        }

        // GET: Borrower
        public ActionResult Index()
        {
            return View("Create");
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
        public ActionResult Create(Borrower borrower)
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
