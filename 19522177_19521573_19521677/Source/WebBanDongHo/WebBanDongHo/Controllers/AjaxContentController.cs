using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanDongHo.Controllers
{
    public class AjaxContentController : Controller
    {
        public INotyfService _notifyService { get; } 

        public AjaxContentController(INotyfService notyfService)
        {
            _notifyService = notyfService;
        }

        public IActionResult HeaderCart()
        {
            return ViewComponent("HeaderCart");
        }
        public IActionResult NumberCart()
        {
            return ViewComponent("NumberCart");
        }
    }
}
