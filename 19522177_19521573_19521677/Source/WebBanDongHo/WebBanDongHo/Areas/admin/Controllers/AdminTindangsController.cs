using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebBanDongHo.Helpper;
using WebBanDongHo.Models;

namespace WebBanDongHo.Areas.admin.Controllers
{
    [Area("admin")]
    public class AdminTindangsController : Controller
    {
        private readonly bandonghoContext _context;
        public INotyfService _notifyService { get; }

        public AdminTindangsController(bandonghoContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }
        // GET: admin/AdminTindangs
        public IActionResult Index(int? page)
        {
            
            var collection = _context.Tindangs.AsNoTracking().ToList();
            foreach (var item in collection)
            {
                if (item.CreatedDate == null){
                    item.CreatedDate = DateTime.Now;
                    _context.Update(item);
                    _context.SaveChanges();
                }
            }
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsTindangs = _context.Tindangs
                .AsNoTracking()
                .OrderByDescending(x => x.PostId);
            PagedList<Tindang> models = new PagedList<Tindang>(lsTindangs, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);

        }


        // GET: admin/AdminTindangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tindang = await _context.Tindangs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tindang == null)
            {
                return NotFound();
            }

            return View(tindang);
        }

        // GET: admin/AdminTindangs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/AdminTindangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] Tindang tindang, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {

                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(tindang.Title) + extension;
                    tindang.Thumb = await Utilities.UploadFile(fThumb, @"tins", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(tindang.Thumb))
                    tindang.Thumb = "default.png";
                tindang.Alias = Utilities.SEOUrl(tindang.Title);
                tindang.CreatedDate = DateTime.Now; 
                _context.Add(tindang);
                _notifyService.Success("Tạo mới thành công");
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tindang);
        }

        // GET: admin/AdminTindangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tindang = await _context.Tindangs.FindAsync(id);
            if (tindang == null)
            {
                return NotFound();
            }
            return View(tindang);
        }

        // POST: admin/AdminTindangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,Title,Scontents,Contents,Thumb,Published,Alias,CreatedDate,Author,AccountId,Tags,CatId,IsHot,IsNewfeed,MetaKey,MetaDesc,Views")] Tindang tindang, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != tindang.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (fThumb != null)
                    {
                        string extension = Path.GetExtension(fThumb.FileName);
                        string imageName = Utilities.SEOUrl(tindang.Title) + extension;
                        tindang.Thumb = await Utilities.UploadFile(fThumb, @"tins", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(tindang.Thumb))
                        tindang.Thumb = "default.png";
                    tindang.Alias = Utilities.SEOUrl(tindang.Title);

                    _context.Update(tindang);
                    _notifyService.Success("Sửa thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TindangExists(tindang.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tindang);
        }

        // GET: admin/AdminTindangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tindang = await _context.Tindangs
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (tindang == null)
            {
                return NotFound();
            }

            return View(tindang);
        }

        // POST: admin/AdminTindangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tindang = await _context.Tindangs.FindAsync(id);
            _context.Tindangs.Remove(tindang);
            await _context.SaveChangesAsync();
            _notifyService.Success("Xoá thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool TindangExists(int id)
        {
            return _context.Tindangs.Any(e => e.PostId == id);
        }
    }
}
