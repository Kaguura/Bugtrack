using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BackEnd.Controllers
{
    public class HomeController : Controller
    {
        private BugtrackContext _context;

        public HomeController(BugtrackContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult App()
        {
            return View();
        }

        /*
        public async Task<string> GetStatus()
        {
            var projects = await _context.Status.ToListAsync();
            string str = "";

            if (projects != null)
            {
                JSONDynamicContract contract = new JSONDynamicContract();
                contract.IncludeProperties(new List<string>() {"Id", "Name"});
                str = JsonConvert.SerializeObject(projects, Formatting.Indented,
                    new JsonSerializerSettings {ContractResolver = contract});
            }
            return str;
        }
        */
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
