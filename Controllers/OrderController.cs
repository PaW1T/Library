using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult CreateOrder()
        {
            LibraryContext db = new LibraryContext();

            List<Record> records = db.Records.ToList();
            List<string> rec = new List<string>();
            foreach (var item in records)
            {
                rec.Add(item.RecordName);
            }

            List<ReadersCard> names = db.ReaderCards.ToList();
            List<string> nam = new List<string>();
            foreach (var item in names)
            {
                nam.Add(item.Surname);
            }

            SelectList books = new SelectList(rec, "Book");
            SelectList readers = new SelectList(nam, "Reader");

            ViewBag.Books = books;
            ViewBag.Readers = readers;
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(string BookId, string CardId)
        {
            LibraryContext db = new LibraryContext();
            ReadersCard rd = db.ReaderCards.First(x => x.Surname == CardId);
            Record rec = db.Records.First(x => x.RecordName == BookId);
            if (db.OrderBookReaders.First(x => x.BookId == rec.RecordId) != null)
            {
                db.OrderBookReaders.First(x => x.BookId == rec.RecordId).Amount++;
            }
            else
            {
                BookReader br = new BookReader
                {
                    BookId = rec.RecordId,
                    ReaderId = rd.Id
                };
                db.OrderBookReaders.Add(br);
            }

            db.SaveChanges();
            return RedirectToAction("CreateOrder");
        }
    }
}
