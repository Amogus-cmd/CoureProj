using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp2
{
    public partial class Artist
    {
        public Artist()
        {
            Groups = new HashSet<Group>();
        }

        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}
