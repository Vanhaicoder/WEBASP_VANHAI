using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.ListProduct = db.Products.ToList();
            model.ListCategory = db.Categories.ToList();
            model.ListBrand= db.Brands.ToList();
            model.ListBanner =db.Banners.ToList();
            model.ListPosts = db.Posts.ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Content()
        { 
            return View();
        }
        WebBanHangEntities db = new WebBanHangEntities();
        public ActionResult Detail(int id)
        {
            // Lấy thông tin sản phẩm theo ID
            Product product = db.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // Lấy các sản phẩm liên quan cùng danh mục, trừ sản phẩm hiện tại
            List<Product> relatedProducts = db.Products
                                              .Where(p => p.Category.id == product.Category.id && p.Id != id)
                                              .Take(4) 
                                              .ToList();

            // Khởi tạo HomeModel và gán giá trị cho các thuộc tính
            HomeModel model = new HomeModel
            {
                Product = product, // Đưa sản phẩm hiện tại vào thuộc tính Product của HomeModel
                RelatedProducts = relatedProducts
            };

            return View(model); 
        }

        public ActionResult TestDetail()
        {
            WebBanHangEntities db = new WebBanHangEntities();
            //lấy ra danh sách sản phẩm 
            List<Product> ketqua = db.Products.ToList();
            return View(ketqua);
        }

        public ActionResult GioiThieu()
        {
            return View();
        }
        public ActionResult ChinhSachDoiTra()
        {
            return View();
        }
        public ActionResult ChinhSachNhanChuyen()
        {
            return View();
        }
        public ActionResult DieuKhoanDichVu()
        {
            return View();
        }
        public ActionResult ChinhSachBaoMat()
        {
            return View();
        }
        public ActionResult HuongDanMuaHang()
        {
            return View();
        }
        public ActionResult ChinhSachSuDungWeb()
        {
            return View();
        }
    }
}