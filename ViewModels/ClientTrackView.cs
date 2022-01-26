using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class ClientTrackView:BaseViewModel
    {
        private CourseProj2Context context;

        private List<AlbumTrackList> contents;

        private List<AlbumTrackList> display;

        public List<AlbumTrackList> Display
        {
            get { return display; }
            set { display = value;OnPropertyChanged(nameof(Display)); }
        }

        private List<Album> albums;

        public List<Album> Albums
        {
            get { return albums; }
            set { albums = value; OnPropertyChanged(nameof(Albums)); }
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
            set { titlebox = value;OnPropertyChanged(nameof(Titlebox)); }
        }

        private string ratingbox;

        public string Ratingbox
        {
            get { return ratingbox; }
            set { ratingbox = value;OnPropertyChanged(nameof(Ratingbox)); }
        }

        private string albumbox;

        public string Albumbox
        {
            get { return albumbox; }
            set { albumbox = value;OnPropertyChanged(nameof(Albumbox)); }
        }

        private RelayCommand search;

        public RelayCommand Search
        {
            get { return search ??= new RelayCommand((o) =>
            {
                List<AlbumTrackList> obj = new List<AlbumTrackList>();
                contents = context.AlbumTrackLists.ToList();
                obj = contents.ToList();

                if(Ratingbox != null)
                obj = obj.Where(o => o.Track.Rating == Ratingbox).ToList();

                if(Titlebox != null)
                    obj = obj.Where(o => o.Track.Title.IndexOf(Titlebox) != -1).ToList();

                if(Albumbox != null)
                    obj = obj.Where(o => o.Album.Title.IndexOf(Albumbox) != -1).ToList();

                Display = obj;
            }); }
        }

        private RelayCommand clear;

        public RelayCommand Clear
        {
            get { return clear ??= new RelayCommand((o) =>
            {
                Ratingbox = null;
                Titlebox = null;
                Albumbox = null;
                Display = contents;
            }); }
        }


        public ClientTrackView()
        {
            context = new CourseProj2Context();
            Albums = context.Albums.ToList();
            Tracks = context.Tracks.ToList();
            contents = context.AlbumTrackLists.ToList();

            foreach (var tmp in contents)
            {
                tmp.Album = context.Albums.FirstOrDefault(a => a.AlbumId == tmp.AlbumId);
                tmp.Track = context.Tracks.FirstOrDefault(a => a.TrackId == tmp.TrackId);
            }

            Display = contents;

        }
    }

    }

