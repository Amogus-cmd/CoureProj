using System;
using System.Collections.Generic;

#nullable disable

namespace WpfApp2
{
    public partial class TrackPerformer
    {
        public int TrackId { get; set; }
        public int PerformerId { get; set; }

        public virtual Performer Performer { get; set; }
        public virtual Track Track { get; set; }
    }
}
