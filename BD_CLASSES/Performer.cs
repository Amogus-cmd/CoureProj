using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp2
{
    public partial class Performer
    {
        public Performer()
        {
            Groups = new HashSet<Group>();
            TrackPerformers = new HashSet<TrackPerformer>();
        }

        public int PerformerId { get; set; }
        public string PerformerName { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<TrackPerformer> TrackPerformers { get; set; }
    }
}
