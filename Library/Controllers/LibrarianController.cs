using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    public class LibrarianController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/librarian/catalogue")]
        public IActionResult Catalogue()
        {
            AuthorsBooks allAuthorsBooks = new AuthorsBooks();
            return View(allAuthorsBooks);
        }

        [HttpGet("librarian/patrons")]
        public IActionResult Patrons()
        {
            return View();
        }
    }
}
