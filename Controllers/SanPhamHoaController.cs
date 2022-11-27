using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebBanHoa.Models;
using PagedList;
using PagedList.Mvc;

namespace WebBanHoa.Controllers
{
    public class SanPhamHoaController : Controller
    {
        // GET: SanPhamHoa
        private WebBanHoaEntities data = new WebBanHoaEntities();
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }
        public ActionResult Index()
        {
            return View(data.SANPHAMs.Take(10));
        }
        public ActionResult PhanLoai()
        {
            var pl = from PL in data.PHANLOAIs select PL;
            return PartialView(pl) ;
        }
        public ActionResult SPTheoPL(int id, int? page)
        {
            ViewBag.MaPL = id;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sp = from s in data.SANPHAMs where s.MaPL == id orderby id select s;
            return View(sp.ToPagedList(iPageNum, iSize));
        }
        public ActionResult NhaCungCap()
        {

            var NCC = from NhaCC in data.NHACUNGCAPs select NhaCC;
            return PartialView(NCC);
        }
        public ActionResult SPTheoNCC(int id, int? page)
        {
            ViewBag.MaNCC = id;
            int iSize = 3;
            int iPageNum = (page ?? 1);
            var sp = from s in data.SANPHAMs where s.MaNCC == id orderby id select s;
            return View(sp.ToPagedList(iPageNum, iSize));
        }
        public ActionResult Chitietsanpham(int id)
        {
            var sp = from s in data.SANPHAMs
                       where s.MaSP == id
                       select s;
            return View(sp.Single());
        }
    }
}