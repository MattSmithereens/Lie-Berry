using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        [HttpGet("/patron")]
        public IActionResult Index()
        {
            return View(Patron.GetAll());
        }

        [HttpPost("/patron")]
        public ActionResult AddPatron(string name)
        {
            Patron newPatron = new Patron(name);
            newPatron.Save();

            return View("Index", Patron.GetAll());
        }

        [HttpPost("/patron/check-ins")]
        public IActionResult CheckIn(string patron)
        {
            PatronsBooks newPatronsBooks = new PatronsBooks(patron);

            return View(newPatronsBooks);
        }

        [HttpPost("/patron/checkouts")]
        public IActionResult CheckOut(string patron)
        {
            PatronsBooks newPatronsBooks = new PatronsBooks(patron);

            return View(newPatronsBooks);
        }

        [HttpPost("/patron/checkouts/{id}/success")]
        public IActionResult CheckedOut(string id, string thisBookId)
        {
            PatronsBooks newPatronsBooks = new PatronsBooks(id);
            newPatronsBooks.ThisBook(thisBookId);
            int properId = Int32.Parse(thisBookId);
            Book newBook = Book.Find(properId);
            newPatronsBooks.patron.CheckOut(newBook);
            return View("Success", newPatronsBooks);
        }
    }
}
