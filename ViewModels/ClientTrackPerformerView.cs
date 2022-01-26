using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class ClientTrackPerformerView:BaseViewModel
    {
        private CourseProj2Context context;

        private List<TrackPerformer> contents;

        private List<TrackPerformer> display;

        public List<TrackPerformer> Display
        {
            get { return display; }
            set { display = value;OnPropertyChanged(nameof(Display)); }
        }

        private List<Performer> performers;

        public List<Performer> Performers
        {
            get { return performers; }
            set { performers = value; OnPropertyChanged(nameof(Performers)); }
        }

        private List<Track> tracks;

        public List<Track> Tracks
        {
            get { return tracks; }
            set { tracks = value; OnPropertyChanged(nameof(Tracks)); }
        }


        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ??= new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); }); }
        }

        private string titlebox;

        public string Titlebox
        {
            get { return titlebox; }
            set { titlebox = value; OnPropertyChanged(nameof(Titlebox)); }
        }

        private string ratingbox;

        public string Ratingbox
        {
            get { return ratingbox; }
            set { ratingbox = value; OnPropertyChanged(nameof(Ratingbox)); }
        }

        private string textboxperformer;

        public string Textboxperformer
        {
            get { return textboxperformer; }
            set { textboxperformer = value; OnPropertyChanged(nameof(textboxperformer)); }
        }


        private RelayCommand search;

        public RelayCommand Search
        {
            get
            {
                return search ??= new RelayCommand((o) =>
                {
                    List<TrackPerformer> obj = new List<TrackPerformer>();
                    contents = context.TrackPerformers.ToList();
                    obj = contents.ToList();

                    if (Titlebox != null)
                        obj = obj.Where(o => o.Track.Title.IndexOf(Titlebox) != -1).ToList();

                    if (Textboxperformer != null)
                        obj = obj.Where(o => o.Performer.PerformerName.IndexOf(textboxperformer) != -1).ToList();

                    if (Ratingbox != null)
                        obj = obj.Where(o => o.Track.Rating == Ratingbox).ToList();

                    Display = obj;
                });
            }
        }

        private RelayCommand clear;

        public RelayCommand Clear
        {
            get
            {
                return clear ??= new RelayCommand((o) =>
                {
                    Ratingbox = null;
                    Titlebox = null;
                    Textboxperformer = null;
                    Display = contents;
                });
            }
        }

        public ClientTrackPerformerView()
        {
            context = new CourseProj2Context();
            Performers = context.Performers.ToList();
            Tracks = context.Tracks.ToList();
            contents = context.TrackPerformers.ToList();

            foreach (var tmp in contents)
            {
                tmp.Performer = context.Performers.FirstOrDefault(a => a.PerformerId == tmp.PerformerId);
                tmp.Track = context.Tracks.FirstOrDefault(a => a.TrackId == tmp.TrackId);
            }

            Display = contents;
        }

    }
}
