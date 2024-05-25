using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebBanDongHo.Extension;
using WebBanDongHo.Helpper;
using WebBanDongHo.Models;
using WebBanDongHo.ModelViews;

namespace WebBanDongHo.Controllers
{

    [Authorize]
    public class AccountController : Controller
    {
        private readonly bandonghoContext _context=new bandonghoContext();
        public INotyfService _notifyService { get; }
        public AccountController(bandonghoContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidatePhone(string Phone)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Phone.ToLower() == Phone);
                if (khachhang != null)
                {
                    return Json(data: "Số : " + Phone + " này đã được sử dụng");
                }
                return Json(data: true);
            }
            catch
            {

                return Json(data: true);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ValidateEmail(string Email)
        {
            try
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.ToLower() == Email.ToLower());
                if (khachhang != null)
                {
                    return Json(data: "Email : " + Email + " này đã được sử dụng");
                }
                return Json(data: true);
            }
            catch
            {

                return Json(data: true);
            }
        }
        /* 
      [HttpGet]
      [AllowAnonymous]
   public IActionResult ValidateUserName(string Username)
      {
          try
          {
              var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.User.ToLower() == Username.ToLower());
              if (khachhang != null)
              {
                  return Json(data: "Tên tài khoản : " + Username + " này đã được sử dụng");
              }
              return Json(data: true);
          }
          catch
          {

              return Json(data: true);
          }
      }
     */

        [Route("tai-khoan-cua-toi.html", Name = "Dashboard")]
        public IActionResult Dashboard()
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.CustomerId == Convert.ToInt32(taikhoanID));
            if (khachhang!= null)
                {
                    var lsDonHang = _context.Orders
                        .Include(x=>x.TransactStatus)
                        .AsNoTracking()
                        .Where(x => x.CustomerId == khachhang.CustomerId)
                        .OrderByDescending(x => x.OrderDate).ToList();
                    ViewBag.DonHang = lsDonHang;
                    return View(khachhang);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("dang-xuat.html", Name = "DangXuat")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("CustomerId");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public IActionResult DangKyTaiKhoan()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-ky.html", Name = "DangKy")]
        public async Task<IActionResult> DangKyTaiKhoan(RegisterVM taikhoan, Microsoft.AspNetCore.Http.IFormFile fAvatar)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string salt = Utilities.GetRandomKey();
                    Customer khachhang = new Customer
                    {
                        FullName = taikhoan.FullName,
                        Phone = taikhoan.Phone.Trim().ToLower(),
                        Email = taikhoan.Email.Trim().ToLower(),
                        Password = (taikhoan.Password + salt.Trim()).ToMD5(),
                        Active = true,
                        Salt = salt,
                        CreateDate = DateTime.Now,

                        };
                    try
                    {
                        _context.Customers.Add(khachhang);
                        _notifyService.Success("Tạo thành công!");
                        await _context.SaveChangesAsync();

                        //Lưu luôn Session đỡ phải Login lại
                        //     HttpContext.Current.Session.Clear();
                        //Lưu Session cho CustomerId
                        HttpContext.Session.SetString("CustomerId", khachhang.CustomerId.ToString());
                        var taikhoanID = HttpContext.Session.GetString("CustomerId");
                        //Identity
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, khachhang.FullName),
                            new Claim("CustomerId", khachhang.CustomerId.ToString())
                        };
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,"login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);

                        return RedirectToAction("Dashboard","Account");
                    }
                    catch (Exception ex)
                    {
                        _notifyService.Error("Lỗi khi tạo tài khoản! Chuyển hướng về trang Đăng Ký");
                        return RedirectToAction("DangKyTaiKhoan", "Account");
                    }
                }
                else
                {
                    return View(taikhoan);
                }
            }
            catch
            {

                return View(taikhoan);
            }
        }

        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public IActionResult Login(string returnUrl = null)
        {
            var taikhoanID = HttpContext.Session.GetString("CustomerId");
            if (taikhoanID != null)
            {
                return RedirectToAction("Dashboard", "Account");
            }
         //   ViewBag.ReturnURL = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("dang-nhap.html", Name = "DangNhap")]
        public async Task<IActionResult> Login(LoginViewModel customer, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    bool isEmail = Utilities.IsValidEmail(customer.UserName);
                    if (!isEmail) { return View(customer); }
               
                  //==>  var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email.Trim() == customer.UserName);
                 var khachhang = _context.Customers.AsNoTracking().SingleOrDefault(x => x.Email== customer.UserName);
             
                 // thằng email trong db có thể là null ---> mà null thì ko có trim() dc.==> lỗi
                    if (khachhang == null) 
                    {
                        _notifyService.Error("Bạn chưa đăng kí tài khoản");
                        return RedirectToAction("DangKyTaiKhoan");
                    }

                    string pass = (customer.Password + khachhang.Salt.Trim()).ToMD5();
 
                    //Kiểm tra pass có giống vs Password ko
                    if (khachhang.Password != pass)
                    {
                        _notifyService.Error("Password không chính xác");
                        return View(customer);
                    }

                    //Kiểm tra Acc có bị Disable không
                    if (khachhang.Active == false)
                    {
                        _notifyService.Warning("Tài khoản của bạn đang bị khóa, vui lòng liên hệ Admin!");
                        return RedirectToAction("ThongBao", "Account");
                    }
                    //Lưu luôn Session đỡ phải Login lại
                    //Lưu Session cho CustomerId
                    HttpContext.Session.SetString("CustomerId",khachhang.CustomerId.ToString() );
                    var taikhoanID = HttpContext.Session.GetString("CustomerId");
                    //Identity
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, khachhang.FullName),
                            new Claim("CustomerId", khachhang.CustomerId.ToString())
                        };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    _notifyService.Success("Đăng nhập thành công!");
                    return RedirectToAction("Dashboard", "Account");
                }
               //_notifyService.Error("Thông tin đăng nhập không chính xác");
              //  return RedirectToAction("Register", "Accounts");
            }
            catch(Exception ex)
            {
                _notifyService.Error("Bị lỗi gì đó r :D");
                return RedirectToAction("DangKyTaiKhoan", "Account");
            }
            return View(customer);
        }


        #region //Change Pass

        [HttpGet]
        [Route("ChangePassword.html", Name = "DoiMatKhau")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ChangePassword.html", Name = "DoiMatKhau")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var taikhoanID = HttpContext.Session.GetString("CustomerId");
                if (taikhoanID == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                if (ModelState.IsValid)
                {
                    var taikhoan = _context.Customers.Find(Convert.ToInt32(taikhoanID));
                    if (taikhoan == null) return RedirectToAction("Login", "Account");

                    var pass = (model.PasswordNow.Trim() + taikhoan.Salt.Trim()).ToMD5();
                    if (pass == taikhoan.Password)
                    {
                        string newpass = (model.Password.Trim() + taikhoan.Salt.Trim()).ToMD5();
                        taikhoan.Password = newpass;
                        _context.Update(taikhoan);
                        _notifyService.Success("Update thành công");
                        _context.SaveChanges();
                        return RedirectToAction("Dashboard", "Account");
                    }
                }
                
            }
            catch
            {
                _notifyService.Error("Update 0 thành công");
                return RedirectToAction("Dashboard", "Account");
            }
            _notifyService.Error("Update 0 thành công");
            return RedirectToAction("Dashboard", "Account");
        }
        #endregion

        /* #region  //Edit
        // GET: Admin/AdminCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var AccID = HttpContext.Session.GetString("CustommerId"); // So sánh ID trong Session với ID của tài khoản đang được Edit
            if (id != Convert.ToInt32(AccID))
            {
                return RedirectToAction("Logout", "Accounts");
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        POST: Admin/AdminCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustommerId,Username,Password,FullName,BirthDay,Avatar,Address,Mail,Phone,Province,District,Ward,CreateDate,LastLogin,IsActived,Randomkey")] Customer customer, Microsoft.AspNetCore.Http.IFormFile fAvatar)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.FullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(customer.FullName);
                    if (fAvatar != null)
                    {
                        string extennsion = Path.GetExtension(fAvatar.FileName);
                        image = Utilities.ToUrlFriendly(customer.FullName) + extennsion;
                        customer.Avatar = await Utilities.UploadFile(fAvatar, @"User", image.ToLower());
                    }
                    if (string.IsNullOrEmpty(customer.Avatar)) customer.Avatar = "avatar.png";
                    customer.LastLogin = DateTime.Now;
                    _notyfService.Success("Sửa thành công!");
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustommerId))
                    {
                        _notyfService.Error("Lỗi!!!!!!!!!!!!");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        private bool CustomerExists(int UserId)
        {
            return _context.Customers.Any(e => e.CustommerId == UserId);
        }
        #endregion
        */
    }
}
