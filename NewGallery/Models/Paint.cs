using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class Paint
    {
        public int PaintID { get; set; }

        public string CreateDate { get; set; }

        public string Size { get; set; }

        public int Price { get; set; }

        public string Type { get; set; }

        public int ArtistID { get; set; }
        public Artist Artist { get; set; }
    }
}