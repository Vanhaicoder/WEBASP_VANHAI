using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBTHAY_LEVANHAI_2122110022.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<Brand> ListBrand { get; set; }
        public List<Product> RelatedProducts { get; set; }
        public Product Product { get; internal set; }
        public List<Banner> ListBanner { get; set; }
    }
}