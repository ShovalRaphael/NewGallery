using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewGallery.Models
{
    public class Artist
    {
        public int ArtistID { get; set; }

        public string ArtistName { get; set; }

        public string Email { get; set; }
        public string FavoriteStyle { get; set; }

        public int Rate { get; set; }

        public virtual ICollection<Paint> Paints { get; set; }
    }
}