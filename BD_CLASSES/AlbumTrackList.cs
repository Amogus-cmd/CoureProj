using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp2
{
    public partial class AlbumTrackList
    {
        public int AlbumId { get; set; }
        public int TrackId { get; set; }

        public virtual Album Album { get; set; }
        public virtual Track Track { get; set; }
    }
}
