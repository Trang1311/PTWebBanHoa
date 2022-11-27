using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHoa.Models
{
    public class GioHang
    {
        WebBanHoaEntities db = new WebBanHoaEntities();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get { return dDonGia * iSoLuong; } }
        public GioHang(int ms)
        {
            iMaSP = ms;
            SANPHAM s = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = s.TenSP;
            sAnhBia = s.HinhMinhHoa;
            dDonGia = double.Parse(s.DonGia.ToString());
            iSoLuong = 1;

        }
    }
}