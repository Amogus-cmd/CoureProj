using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp2
{
    public partial class Track
    {
        public Track()
        {
            AlbumTrackLists = new HashSet<AlbumTrackList>();
            TrackPerformers = new HashSet<TrackPerformer>();
        }

        public int TrackId { get; set; }
        public string Title { get; set; }
        public string Lenght { get; set; }
        public string Rating { get; set; }
        public TimeSpan? TrackLength { get; set; }
        public string TrackFile { get; set; }

        public virtual ICollection<AlbumTrackList> AlbumTrackLists { get; set; }
        public virtual ICollection<TrackPerformer> TrackPerformers { get; set; }
    }
}
