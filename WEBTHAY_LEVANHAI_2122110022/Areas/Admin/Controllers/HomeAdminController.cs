using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Areas.Admin.Controllers
{
    
    public class HomeAdminController : Controller
    {
        private WebBanHangEntities db = new WebBanHangEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            var totalProducts = db.Products.Count();
            var totalCategories = db.Categories.Count();
            var totalOrders = db.Orders.Count(); // Thêm thống kê đơn hàng nếu cần
            var totalUsers = db.Users.Count(); // Thêm thống kê người dùng nếu cần
            var totalBrand = db.Brands.Count();
            ViewBag.TotalProducts = totalProducts;
            ViewBag.TotalBrand = totalBrand;
            ViewBag.TotalCategories = totalCategories;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalUsers = totalUsers;
            return View();
            
        }
    }
}