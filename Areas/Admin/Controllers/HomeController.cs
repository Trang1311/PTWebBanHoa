using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebBanHoa.Models;

namespace WebBanHoa.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: User
        WebBanHoaEntities data = new WebBanHoaEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogoutPartial");
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            var sTenDNAdmin = f["TenDNAdmin"];
            var sMatKhauAdmin = f["MatKhauAdmin"];
            if (String.IsNullOrEmpty(sTenDNAdmin))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatKhauAdmin))
            {
                ViewData["Err1"] = "Phải nhập mật khẩu";
            }
            else
            {
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.TenDNAdmin == sTenDNAdmin && n.MatKhauAdmin == sMatKhauAdmin);
                if (ad != null)
                {

                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TaikhoanAdmin"] = ad;
                    
                        return RedirectToAction("Index", "Home");
                   
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";

                }
            }
            return View();
        }

    }
}