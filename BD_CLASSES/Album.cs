using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp2
{
    public partial class Album
    {
        public Album()
        {
            AlbumTrackLists = new HashSet<AlbumTrackList>();
        }

        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public TimeSpan? TotalLength { get; set; }
        public int? TrackCount { get; set; }

        public virtual ICollection<AlbumTrackList> AlbumTrackLists { get; set; }
    }
}
