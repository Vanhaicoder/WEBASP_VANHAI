using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;

public class RouteConfig
{
    public static void RegisterRoutes(RouteCollection routes)
    {
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        routes.MapRoute(
           name: "Index",
           url: "trang-chu",
           defaults: new { controller = "Home", action = "Index" }
       );
        routes.MapRoute(
           name: "CategoryIndex",
           url: "danh-muc",
           defaults: new { controller = "Category", action = "Index" }
       );


        routes.MapRoute(
            name: "BrandRoute",
            url: "thuong-hieu/{id}",
            defaults: new { controller = "Brand", action = "ProductByBrandId" },
            namespaces: new[] { "WEBTHAY_LEVANHAI_2122110022.Controllers" }
        );
        routes.MapRoute(
           name: "ProductListGrid",
           url: "san-pham/dang-luoi",
           defaults: new { controller = "Product", action = "ListGrid" },
           namespaces: new[] { "WEBTHAY_LEVANHAI_2122110022.Controllers" }
       );
        routes.MapRoute(
           name: "BrandIndex",
           url: "thuong-hieu",
           defaults: new { controller = "Brand", action = "Index" },
           namespaces: new[] { "WEBTHAY_LEVANHAI_2122110022.Controllers" }
       );
        routes.MapRoute(
           name: "PostUi",
           url: "bai-viet",
           defaults: new { controller = "PostUi", action = "Index" }
       );
        routes.MapRoute(
           name: "Topic",
           url: "chu-de",
           defaults: new { controller = "Topic", action = "Index" },
           namespaces: new[] { "WEBTHAY_LEVANHAI_2122110022.Controllers" }
       );
        routes.MapRoute(
            name: "UserLogin",
            url: "dang-nhap",
            defaults: new { controller = "User", action = "Login" },
            namespaces: new[] { "WEBTHAY_LEVANHAI_2122110022.Controllers" }
        );
        routes.MapRoute(
            name: "UserRegister",
            url: "dang-ki",
            defaults: new { controller = "User", action = "Register" },
            namespaces: new[] { "WEBTHAY_LEVANHAI_2122110022.Controllers" }
        );
        routes.MapRoute(
            name: "HomeDetail",
            url: "trang-chu/chi-tiet/{id}",
            defaults: new { controller = "Home", action = "Detail", id = UrlParameter.Optional }
        );
        routes.MapRoute(
            name: "ProductListLarge",
            url: "san-pham/danh-sach",
            defaults: new { controller = "Product", action = "ListLarge" }
        );
        routes.MapRoute(
            name: "Cart",
            url: "gio-hang",
            defaults: new { controller = "Cart", action = "Index" }
        );
        routes.MapRoute(
           name: "GioiThieu",
           url: "gioi-thieu",
           defaults: new { controller = "Home", action = "GioiThieu" }
       );
        routes.MapRoute(
            name: "CategoryProducts",
            url: "danh-muc/{id}",
            defaults: new { controller = "Category", action = "ProductByCategoryId", id = UrlParameter.Optional }
        );
        routes.MapRoute(
            name: "ChinhSachDoiTra",
            url: "chinh-sach-doi-tra",
            defaults: new { controller = "Home", action = "ChinhSachDoiTra" }
        );

        routes.MapRoute(
            name: "ChinhSachNhanChuyen",
            url: "chinh-sach-nhan-chuyen",
            defaults: new { controller = "Home", action = "ChinhSachNhanChuyen" }
        );

        routes.MapRoute(
            name: "DieuKhoanDichVu",
            url: "dieu-khoan-dich-vu",
            defaults: new { controller = "Home", action = "DieuKhoanDichVu" }
        );

        routes.MapRoute(
            name: "ChinhSachBaoMat",
            url: "chinh-sach-bao-mat",
            defaults: new { controller = "Home", action = "ChinhSachBaoMat" }
        );

        routes.MapRoute(
            name: "HuongDanMuaHang",
            url: "huong-dan-mua-hang",
            defaults: new { controller = "Home", action = "HuongDanMuaHang" }
        );

        routes.MapRoute(
            name: "ChinhSachSuDungWeb",
            url: "chinh-sach-su-dung-web",
            defaults: new { controller = "Home", action = "ChinhSachSuDungWeb" }
        );

        routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "WEBTHAY_LEVANHAI_2122110022.Controllers" }
        );
    }
}
