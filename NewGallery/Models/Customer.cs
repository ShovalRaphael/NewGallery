using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string CustName { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public string PhoneNum { get; set; }
    }
}