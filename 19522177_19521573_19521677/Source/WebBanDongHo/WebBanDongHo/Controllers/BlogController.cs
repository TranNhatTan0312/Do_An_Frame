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
    public class BlogController : Controller
    {
        private readonly bandonghoContext _context;
        public BlogController(bandonghoContext context)
        {
            _context = context;
        }

        [Route("blogs.html",Name ="Blog")]
        public IActionResult Index(int? page)
        {

            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTindangs = _context.Tindangs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<Tindang> models = new PagedList<Tindang>(lsTindangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }

        [Route("/tin-tuc/{Alias}-{id}.html", Name = "TinDetails")]
        public IActionResult Detail(int id)
        {
            var tindang = _context.Tindangs.AsNoTracking().SingleOrDefault(x=>x.PostId==id);
            if (tindang == null)
            {
                return RedirectToAction("Index");
            }
            var lsBaivietlienquan = _context.Tindangs
                .AsNoTracking()
                .Where(x=>x.Published == true && x.PostId != id)
                .Take(3).OrderByDescending(x=>x.CreatedDate)
                .ToList();
            ViewBag.Baivietlienquan = lsBaivietlienquan;

            return View(tindang);
        }
    }
}
