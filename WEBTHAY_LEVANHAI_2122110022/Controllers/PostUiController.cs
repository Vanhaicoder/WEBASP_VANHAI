using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{
    public class PostUiController : Controller
    {
        // GET: PostUi
        WebBanHangEntities db = new WebBanHangEntities();
        public ActionResult Index()
        {
            PostModel model = new PostModel();
            model.ListPosts = db.Posts.ToList();
           
            return View(model);
        }
    }
}