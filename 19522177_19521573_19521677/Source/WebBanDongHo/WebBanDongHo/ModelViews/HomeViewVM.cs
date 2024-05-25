using System;
using WebBanDongHo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebBanDongHo.ModelViews
{
    public class HomeViewVM
    {
        public List<Tindang> TinTucs { get; set; }
        public List<ProductHomeVM> Products { get; set; }
        public Quangcao quangcao { get; set; }
    }
}

