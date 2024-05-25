using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebBanDongHo.Extension;
using WebBanDongHo.Models;
using WebBanDongHo.ModelViews;

namespace WebBanDongHo.Controllers
{

    public class ShoppingCartController : Controller
    {
        private readonly bandonghoContext _context = new bandonghoContext();

        public INotyfService _notifyService { get; }
        public ShoppingCartController(bandonghoContext context, INotyfService notyfService)
        {
            _context = context;
            _notifyService = notyfService;
        }

        public List<CartItem> GioHang
        {
            get
            {
                var gh = HttpContext.Session.Get<List<CartItem>>("GioHang"); // Lấy từ trong Session
                if (gh == default(List<CartItem>)) //có thì thôi
                {
                    gh = new List<CartItem>(); // ko thì tạo
                                               // HttpContext.Session.Set<List<CartItem>>("GioHang", gh);
                }
                return gh;
            }
        }


        //1 Thêm
        [HttpPost]
        [Route("api/cart/add")]
        public IActionResult AddToCart(int productID, int? amount)
        {
            List<CartItem> cart = GioHang;
            try
            {
                // Thêm sản phẩm vào giỏ hàng
                CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null) // giỏ hàng có đồ
                {
                        item.amount = item.amount + amount.Value; // số lượng = số lượng nhập vào
                        HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }

                else
                    {
                        Product hh = _context.Products.SingleOrDefault(p => p.ProductId == productID);
                        item = new CartItem
                        {
                            amount = amount.HasValue ? amount.Value : 1,
                            product = hh
                        };
                        cart.Add(item);
                    }
                //Luu lai session
                HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                _notifyService.Success("Thêm thành công vào giỏ hàng");
                return Json(new { succcess = true });
            }
            catch(Exception ex)
            {
                return Json(new { succcess = false });
            }
        }
        //2 xoá
        [HttpPost]
        [Route("api/cart/remove")]
        public IActionResult Remove(int productID)
        {
            try
            {
                List<CartItem> gioHang = GioHang;
                CartItem item = gioHang.SingleOrDefault(p => p.product.ProductId == productID);
                if (item != null)
                {
                    gioHang.Remove(item);
                }
                // lưu lại session
                HttpContext.Session.Set<List<CartItem>>("GioHang", gioHang);
                return Json(new { succcess = true });
            }
            catch (Exception ex)
            {
                return Json(new { succcess = false });
            }
        }

        //3 cập nhật giỏ hàng
        [HttpPost]
        [Route("api/cart/update")]
        public IActionResult UpdateCart(int productID, int? amount)
        {
            // lấy giỏ hàng ra để xử lý
            var cart = HttpContext.Session.Get<List<CartItem>>("GioHang");
            try
            {
                if (cart != null)
                {
                    CartItem item = cart.SingleOrDefault(p => p.product.ProductId == productID);
                    if (item != null && amount.HasValue) // giỏ hàng có đồ--> cập nhật số lượng
                    {
                        item.amount = amount.Value; // số lượng = số lượng nhập vào
                    }
                    //Lưu lại session
                    HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
                }
                return Json(new { succcess = true });
            }
            catch
            {
                return Json(new { succcess = false });
            }
        }
        //4 index
        [Route("cart.html", Name = "Cart")]
        public IActionResult Index()
        {
            return View(GioHang);
        }

    }
}
