using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class ReaderCardController : Controller
    {

        // GET: ReaderCard
        public ActionResult AllCards()
        {
            LibraryContext db = new LibraryContext();
            return View(db.ReaderCards.ToList());
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ReadersCard model)
        {
            using (LibraryContext db = new LibraryContext())
            {
                if (ModelState.IsValid)
                {
                    ReadersCard newCard = new ReadersCard();
                    newCard = model;
                    newCard.ApplicationDate = DateTime.Now;
                    db.ReaderCards.Add(newCard);
                    db.SaveChanges();
                    return Redirect("AllCards");
                }
                else
                {
                    return View(model);
                }
            }
        }
        public ActionResult Edit(int id)
        {
            LibraryContext db = new LibraryContext();
            return View(db.ReaderCards.First(x => x.Id == id));
        }

        [HttpPost]
        public ActionResult Edit(ReadersCard model)
        {
            LibraryContext db = new LibraryContext();
            db.ReaderCards.First(x => x.Id == model.Id).Name = model.Name;
            db.ReaderCards.First(x => x.Id == model.Id).Phone = model.Phone;
            db.ReaderCards.First(x => x.Id == model.Id).Position = model.Position;
            db.ReaderCards.First(x => x.Id == model.Id).SecondName = model.SecondName;
            db.ReaderCards.First(x => x.Id == model.Id).Surname = model.Surname;
            db.ReaderCards.First(x => x.Id == model.Id).City = model.City;

            db.SaveChanges();
            return RedirectToAction("AllCards");
        }

        public ActionResult Delete(int id)
        {
            LibraryContext db = new LibraryContext();
            db.ReaderCards.Remove(db.ReaderCards.First(x => x.Id == id));
            db.SaveChanges();
            return RedirectToAction("AllCards");
        }

        public ActionResult ShowBooks(int id)
        {
            LibraryContext db = new LibraryContext();
            List<BookReader> br = db.OrderBookReaders.Where(x => x.ReaderId == id).ToList();
            List<int> idList = new List<int>();
            List<int> amountList = new List<int>();
            foreach (var item in br)
            {
                idList.Add(item.BookId);
                amountList.Add(item.Amount);
            }

            List<Record> books = db.Records.Where(x => idList.Contains(x.RecordId)).ToList();
            ViewBag.amounts = amountList;

            return View(books);
        }

        public ActionResult Passed(int id)
        {
            LibraryContext db = new LibraryContext();
            try
            {
                db.OrderBookReaders.Remove(db.OrderBookReaders.First(x => x.BookId == id));
            }
            catch (Exception)
            {

            }
            db.SaveChanges();
            return RedirectToAction("AllCards");
        }
    }
}