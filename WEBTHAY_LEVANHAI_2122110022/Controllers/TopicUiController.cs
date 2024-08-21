using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WEBTHAY_LEVANHAI_2122110022.Models;

namespace WEBTHAY_LEVANHAI_2122110022.Controllers
{
    public class TopicController : Controller
    {
        WebBanHangEntities db = new WebBanHangEntities();

        // Hiển thị tất cả các chủ đề (Topic)
        public ActionResult Index()
        {
            var listTopic = db.Topics.ToList(); // Giả sử bạn có bảng 'Topics'
            return View(listTopic);
        }

        // Hiển thị tất cả bài viết theo Topic
        public ActionResult PostsByTopicId(int id)
        {
            var posts = db.Posts.Where(p => p.Topic_id == id).ToList();
            var topic = db.Topics.FirstOrDefault(t => t.Topic_id == id);

            if (posts == null || !posts.Any())
            {
                return HttpNotFound();
            }

            ViewBag.TopicName = topic?.Name ?? "Chủ đề không xác định";
            return View(posts);
        }

    }
}
