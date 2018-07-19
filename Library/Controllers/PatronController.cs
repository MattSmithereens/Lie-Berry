using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

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
            return View();
        }

        [HttpPost("/patron/checkouts")]
        public IActionResult CheckOut(string patron)
        {
            return View();
        }
    }
}
