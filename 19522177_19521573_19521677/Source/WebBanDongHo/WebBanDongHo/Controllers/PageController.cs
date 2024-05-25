using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanDongHo.Models;

namespace WebBanDongHo.Controllers
{
    public class PageController : Controller
    {
        private readonly bandonghoContext _context;
        public PageController(bandonghoContext context)
        {
            _context = context;
        }

        [Route("/page/{Alias}", Name = "PageDetails")]
        public IActionResult Detail(string Alias)
        {
            if (string.IsNullOrEmpty(Alias)) return RedirectToAction("Index", "Home");
            var page = _context.Pages.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
            if (page == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(page);
        }
    }
}

