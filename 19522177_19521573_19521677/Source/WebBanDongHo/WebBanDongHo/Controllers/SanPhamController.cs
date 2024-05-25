using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanDongHo.Models;
/*
namespace WebBanDongHo.Controllers
{
    public class SanPhamController : Controller
    {

        private readonly bandonghoContext _context;
        public SanPhamController(bandonghoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            var product = _context.Products.Include(x => x.Cat).FirstOrDefault(MySqlX => MySqlX.ProductId == id);
                if (product == null)
            {
                return RedirectToAction("Index");
            }
         
                return View(product);
        }
    }
}*/


namespace WebBanDongHo.Controllers
{
    public class SanPhamController : Controller
    {

        private readonly bandonghoContext _context;
        public SanPhamController(bandonghoContext context)
        {
            _context = context;
        }

        [Route("shop.html", Name = "ShopProduct")]
        public IActionResult Index(int? page)
        {
            try
            {
                var pageNumber = page == null || page <= 0 ? 1 : page.Value;
                var pageSize = 10;
                var lsTinDangs = _context.Products
                    .AsNoTracking()
                    .Where(x => x.UnitsInStock > 0 )
                    .Where(x=> x.Active==true)
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsTinDangs, pageNumber, pageSize);
                ViewBag.CurrentPage = pageNumber;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Contact");
            }

        }
        [Route("/{Alias}", Name = "ListProduct")]
        public IActionResult List(string Alias, int page = 1)
        {
            try
            {
                var pageSize = 10;
                var danhmuc = _context.Categories.AsNoTracking().SingleOrDefault(x => x.Alias == Alias);
                var lsTinDangs = _context.Products
                    .AsNoTracking()
                    .Where(x => x.CatId == danhmuc.CatId )
                    .OrderByDescending(x => x.DateCreated);
                PagedList<Product> models = new PagedList<Product>(lsTinDangs, page, pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.CurrentCat = danhmuc;
                return View(models);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }

        }
        [Route("/{Alias}-{id}.html", Name = "ProductDetails")]
        public IActionResult Detail(int id)
        {
            try
            {
                var product = _context.Products.Include(x => x.Cat).FirstOrDefault(MySqlX => MySqlX.ProductId == id);
                if (product == null)
                {
                    return RedirectToAction("Index");
                }
                var lsProduct = _context.Products.AsNoTracking().Where(x => x.CatId == product.CatId && x.ProductId != id && x.Active == true).OrderBy(x => x.DateCreated).Take(4).ToList();

                ViewBag.SanPham = lsProduct;
                return View(product);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
