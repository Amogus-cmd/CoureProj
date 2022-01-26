using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp2
{
    public partial class Group
    {
        public int PerformerId { get; set; }
        public int ArtistId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Performer Performer { get; set; }
    }
}
