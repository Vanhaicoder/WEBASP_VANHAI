using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBTHAY_LEVANHAI_2122110022.Models
{
    public class PaymentModel
    {
        public List<CartModel> CartItems { get; set; }
        public decimal TotalPrice { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}