using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{

    public class ProductController : Controller

    {
        // GET: Product
        public ActionResult ListGrid(string query = "", int page = 1, int pageSize = 6)
        {
            WebBanHangEntities dbss = new WebBanHangEntities();
            HomeModel model = new HomeModel();

            // Nếu có chuỗi tìm kiếm, tìm kiếm sản phẩm theo tên
            if (!string.IsNullOrEmpty(query))
            {
                model.ListProduct = dbss.Products
                                        .Where(p => p.Name.Contains(query))
                                        .OrderBy(p => p.Id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();
            }
            else
            {
                // Nếu không có chuỗi tìm kiếm, hiển thị tất cả sản phẩm theo trang
                model.ListProduct = dbss.Products
                                        .OrderBy(p => p.Id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();
            }

            // Lấy tổng số lượng sản phẩm (theo truy vấn nếu có)
            var totalProducts = !string.IsNullOrEmpty(query) ? dbss.Products.Count(p => p.Name.Contains(query)) : dbss.Products.Count();

            // Tính toán số trang
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.Query = query;

            return View(model);
        }
        public ActionResult ListLarge(int page = 1, int pageSize = 6) 
        {
            WebBanHangEntities dbs = new WebBanHangEntities();
            HomeModel model = new HomeModel();
            model.ListCategory = dbs.Categories.ToList();
            model.ListBrand = dbs.Brands.ToList();
            var totalProducts = dbs.Products.Count();

            model.ListProduct = dbs.Products
                                   .OrderBy(p => p.Id)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToList();

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);
            ViewBag.CurrentPage = page;

            return View(model);
        }
        
        public ActionResult onethamso(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }
        public ActionResult twothamso(int id,string name)
        {
            ViewBag.ProductId = id;
            ViewBag.Name = name;
            return View();
        }      
    }
}