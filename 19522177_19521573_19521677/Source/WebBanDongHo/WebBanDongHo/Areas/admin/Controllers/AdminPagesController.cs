using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebBanDongHo.Models;
using WebBanDongHo.Helpper;

namespace WebBanDongHo.Areas.admin.Controllers
{
    [Area("admin")]
    public class AdminPagesController : Controller
    {
        private readonly bandonghoContext _context;
        public INotyfService _notifyService { get; }

        public AdminPagesController(bandonghoContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }
        // GET: admin/AdminPages
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 10;
            var lsPages = _context.Pages
                .AsNoTracking()
                .OrderByDescending(x => x.PageId);
            PagedList<Page> models = new PagedList<Page>(lsPages, pageNumber, pageSize);
            ViewBag.CurrentPage = pageNumber;
            return View(models);
     
        }

        // GET: admin/AdminPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // GET: admin/AdminPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: admin/AdminPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedDate,Ordering")] Page page, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                if (fThumb != null)
                {
                    string extension = Path.GetExtension(fThumb.FileName);
                    string imageName = Utilities.SEOUrl(page.PageName)+ extension;          
                    page.Thumb = await Utilities.UploadFile(fThumb, @"pages", imageName.ToLower());
                }
                if (string.IsNullOrEmpty(page.Thumb))
                    page.Thumb = "default.png";
                page.Alias = Utilities.SEOUrl(page.PageName);
               
                _context.Add(page);

                await _context.SaveChangesAsync();
                _notifyService.Success("Tạo mới thành công");
                return RedirectToAction(nameof(Index));
            }
            return View(page);
        }

        // GET: admin/AdminPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound();
            }
            return View(page);
        }

        // POST: admin/AdminPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageId,PageName,Contents,Thumb,Published,Title,MetaDesc,MetaKey,Alias,CreatedDate,Ordering")] Page page, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (id != page.PageId)
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
                        string imageName = Utilities.SEOUrl(page.PageName) + extension;
                        page.Thumb = await Utilities.UploadFile(fThumb, @"pages", imageName.ToLower());
                    }
                    if (string.IsNullOrEmpty(page.Thumb))
                        page.Thumb = "default.png";

                    _context.Update(page);
                    _notifyService.Success("Chỉnh sửa thành công");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PageExists(page.PageId))
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
            return View(page);
        }

        // GET: admin/AdminPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var page = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (page == null)
            {
                return NotFound();
            }

            return View(page);
        }

        // POST: admin/AdminPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();
            _notifyService.Success("Xoá thành công");
            return RedirectToAction(nameof(Index));
        }

        private bool PageExists(int id)
        {
            return _context.Pages.Any(e => e.PageId == id);
        }
    }
}
