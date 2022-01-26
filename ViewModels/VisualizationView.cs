using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class VisualizationView: BaseViewModel
    {
        private CourseProj2Context context;

        private List<Track> contents;

        private List<Performer> performers;

        private List<AlbumTrackList> albumtracklists;
        private Visibility transparency = Visibility.Hidden;

        public Visibility Transparency
        {
            get { return transparency; }
            set { transparency = value; OnPropertyChanged(nameof(Transparency)); }
        }

        private Visibility prozrachnost = Visibility.Hidden;

        public Visibility Prozrachnost
        {
            get { return prozrachnost; }
            set { prozrachnost = value; OnPropertyChanged(nameof(Prozrachnost)); }
        }

        private Visibility nevidimost = Visibility.Hidden;

        public Visibility Nevidimost
        {
            get { return nevidimost; }
            set { nevidimost = value; OnPropertyChanged(nameof(Nevidimost));}
        }

        private List<KeyValuePair<string, int>> statistic = new List<KeyValuePair<string, int>>();
        public List<KeyValuePair<string, int>> Statistic
        {

            get { return statistic; }
            set
            {
                statistic = value;
                OnPropertyChanged(nameof(statistic));
            }
        }

        private List<KeyValuePair<string, int>> statistic2 = new List<KeyValuePair<string, int>>();
        public List<KeyValuePair<string, int>> Statistic2
        {

            get { return statistic2; }
            set
            {
                statistic2 = value;
                OnPropertyChanged(nameof(statistic2));
            }
        }

        private List<KeyValuePair<string, double>> statistic3 = new List<KeyValuePair<string, double>>();
        public List<KeyValuePair<string, double>> Statistic3
        {

            get { return statistic3; }
            set
            {
                statistic3 = value;
                OnPropertyChanged(nameof(statistic3));
            }
        }

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ?? (previous = new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); })); }
        }

        private RelayCommand firstDiagram;

        public RelayCommand FirstDiagram
        {
            get { return firstDiagram ??= new RelayCommand((o) =>
            {
                List<KeyValuePair<string, int>> obj = new List<KeyValuePair<string, int>>();
                foreach (var tmp in contents)
                {
                    obj.Add(new KeyValuePair<string, int>(tmp.Title, tmp.Rating.Length));
                }
                Statistic = obj;

                Prozrachnost = Visibility.Hidden;
                Nevidimost = Visibility.Hidden;
                Transparency = Visibility.Visible;

            }); }
        }

        private RelayCommand secondDiagram;

        public RelayCommand SecondDiagram
        {
            get { return secondDiagram ??= new RelayCommand((o) =>
            {
                List<KeyValuePair<string, int>> obj = new List<KeyValuePair<string, int>>();
                foreach (var tmp in performers)
                {
                    obj.Add(new KeyValuePair<string, int>(tmp.PerformerName,context.TrackPerformers.Where(a=>a.PerformerId == tmp.PerformerId).Count()));
                }

                Statistic2 = obj;
                Transparency = Visibility.Hidden;
                Nevidimost = Visibility.Hidden;
                Prozrachnost = Visibility.Visible;
            }); }
        }

        private RelayCommand thirdDiagram;

        public RelayCommand ThirdDiagram
        {
            get { return thirdDiagram ??= new RelayCommand((o) =>
            {
                List<KeyValuePair<string, double>> obj = new List<KeyValuePair<string, double>>();
                List<int> usedkeys = new List<int>();
                foreach (var tmp in albumtracklists)
                {

                    List<AlbumTrackList> index = context.AlbumTrackLists.Where(a => a.AlbumId == tmp.AlbumId).ToList();
                    double totalstars = 0;
                    tmp.Album = context.Albums.FirstOrDefault(a=>a.AlbumId == tmp.AlbumId);
                    if(!usedkeys.Contains(tmp.AlbumId)){
                    foreach (var i in index)
                    {
                        totalstars += context.Tracks.First(a => a.TrackId == i.TrackId).Rating.Length;
                    }
                    obj.Add(new KeyValuePair<string, double>(tmp.Album.Title,totalstars/(double)(context.Albums.Find(tmp.AlbumId).TrackCount)));
                    usedkeys.Add(tmp.AlbumId);
                }
                    }
                Statistic3 = obj;
                Transparency = Visibility.Hidden;
                Prozrachnost = Visibility.Hidden;
                Nevidimost = Visibility.Visible;
            }); }

        }

        public VisualizationView()
        {
            context = new CourseProj2Context();
            contents = context.Tracks.ToList();
            performers = context.Performers.ToList();
            albumtracklists = context.AlbumTrackLists.ToList();
        }

    }
}
