using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatServer
{
    public class IndexController : Controller
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View("InsertUserName");
        }

        [HttpPost]
        public IActionResult Index(string username)
        {
            return View("Index", username);
        }

    }
}
