using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        [HttpGet("/new-author")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("new-author")]
        public ActionResult CreatePost()
        {
            string name = Request.Form["name"];
            Author newAuthor = new Author(name);
            newAuthor.Save();

            return RedirectToAction("ViewAll");
        }

        [HttpGet("view-authors")]
        public ActionResult ViewAll()
        {
            List<Author> allAuthors = Author.GetAll();
            return View(allAuthors);
        }

        [HttpGet("author/{id}/details")]
        public ActionResult Details(int id)
        {
            Author newAuthors = Author.Find(id);
            return View(newAuthors);
        }

        [HttpGet("author/{id}/update")]
        public ActionResult Edit(int id)
        {
            Author newAuthors = Author.Find(id);
            return View(newAuthors);
        }

        //[HttpPost("author/{id}/update")]
        //public ActionResult EditDetails(int id)
        //{
        //    string newName = Request.Form["newName"];
        //    Authors newAuthors = Authors.Find(id);
        //    newAuthors.Edit(newName);
        //    return RedirectToAction("ViewAll");
        //}

        [HttpPost("author/{id}/delete")]
        public ActionResult Delete(int id)
        {
            Author newAuthors = Author.Find(id);
            newAuthors.Delete();
            return RedirectToAction("ViewAll");
        }
    }
}