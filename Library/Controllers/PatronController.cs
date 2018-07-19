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

        [HttpGet("/patron/check-ins")]
        public IActionResult CheckIn()
        {
            return View();
        }

        [HttpGet("/patron/checkouts")]
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
