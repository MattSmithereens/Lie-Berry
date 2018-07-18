﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
    public class PatronController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
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