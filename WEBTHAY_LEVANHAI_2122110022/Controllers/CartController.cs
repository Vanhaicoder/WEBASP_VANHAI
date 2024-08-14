﻿using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Linq;

using WEBTHAY_LEVANHAI_2122110022.Models;

public class CartController : Controller
{
    WebBanHangEntities objAspNetWebEntities = new WebBanHangEntities();

    // GET: Cart
    public ActionResult Index()
    {
        return View((List<CartModel>)Session["cart"]);
    }

    public ActionResult AddToCart(int id, int quantity)
    {
        if (Session["cart"] == null)
        {
            List<CartModel> cart = new List<CartModel>();
            cart.Add(new CartModel { Product = objAspNetWebEntities.Products.Find(id), Quantity = quantity });
            Session["cart"] = cart;
            Session["count"] = 1;
        }
        else
        {
            List<CartModel> cart = (List<CartModel>)Session["cart"];
            int index = isExist(id);
            if (index != -1)
            {
                cart[index].Quantity += quantity;
            }
            else
            {
                cart.Add(new CartModel { Product = objAspNetWebEntities.Products.Find(id), Quantity = quantity });
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            Session["cart"] = cart;
        }
        return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
    }

    private int isExist(int id)
    {
        List<CartModel> cart = (List<CartModel>)Session["cart"];
        for (int i = 0; i < cart.Count; i++)
            if (cart[i].Product.Id.Equals(id))
                return i;
        return -1;
    }

    public ActionResult Remove(int Id)
    {
        List<CartModel> li = (List<CartModel>)Session["cart"];
        li.RemoveAll(x => x.Product.Id == Id);
        Session["cart"] = li;
        Session["count"] = Convert.ToInt32(Session["count"]) - 1;
        return Json(new { Message = "Thành công" }, JsonRequestBehavior.AllowGet);
    }

    // Phương thức lấy sản phẩm trong giỏ hàng từ Session
    private List<CartModel> GetCartItems()
    {
        // Kiểm tra nếu giỏ hàng chưa tồn tại trong session
        if (Session["cart"] == null)
        {
            // Tạo giỏ hàng trống nếu không có sản phẩm nào
            Session["cart"] = new List<CartModel>();
        }
        // Trả về danh sách sản phẩm trong giỏ hàng
        return (List<CartModel>)Session["cart"];
    }

    // Trang thanh toán
    public ActionResult Payment()
    {
        var cart = GetCartItems();
        var model = new PaymentModel
        {
            CartItems = cart,
            TotalPrice = cart.Sum(item => (decimal)((item.Product.Sale_price ?? item.Product.Price) * item.Quantity))
        };

        // Sau khi thanh toán thành công, xóa sản phẩm khỏi giỏ hàng
        Session["cart"] = null;
        Session["count"] = 0;

        return View(model);
    }


}
