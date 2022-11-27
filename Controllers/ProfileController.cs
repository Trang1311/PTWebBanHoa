using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHoa.Models;

namespace WebBanHoa.Controllers
{

    public class ProfileController : Controller
    {
        WebBanHoaEntities data = new WebBanHoaEntities();

        // GET: Profile
        public ActionResult Index(  )
        {
          //  KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
          //  if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
          //  {
                
          //      return Redirect("~/User/DangNhap?id=2");
          //  }
          //var k = from s in data.KHACHHANGs where s.MaKH == id orderby id select s;
            return View();
        }
        
    }
}