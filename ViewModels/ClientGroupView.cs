using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class ClientGroupView: BaseViewModel
    {
        private CourseProj2Context context;

        private List<Group> contents;

        private List<Group> display;
        public List<Group> Display
        {
            get { return display; }
            set { display = value; OnPropertyChanged(nameof(Display)); }
        }

        private List<Performer> performers;
        public List<Performer> Performers
        {
            get { return performers; }
            set { performers = value; OnPropertyChanged(nameof(Performers)); }
        }

        private List<Artist> artists;

        public List<Artist> Artists
        {
            get { return artists; }
            set { artists = value; OnPropertyChanged(nameof(Artists)); }
        }

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ??= new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); }); }
        }

        private string textboxartist;

        public string Textboxartist
        {
            get { return textboxartist; }
            set { textboxartist = value;OnPropertyChanged(nameof(Textboxartist)); }
        }

        private string textboxperformer;

        public string Textboxperformer
        {
            get { return textboxperformer; }
            set { textboxperformer = value; OnPropertyChanged(nameof(textboxperformer)); }
        }
        private string textboxcountry;

        public string Textboxcountry
        {
            get { return textboxcountry; }
            set { textboxcountry = value; OnPropertyChanged(nameof(Textboxcountry)); }
        }
        
        private RelayCommand search;

        public RelayCommand Search
        {
            get
            {
                return search ??= new RelayCommand((o) =>
                {
                    List<Group> obj = new List<Group>();
                    contents = context.Groups.ToList();
                    obj = contents.ToList();

                    if (Textboxartist != null)
                        obj = obj.Where(o => o.Artist.ArtistName.IndexOf(Textboxartist) != -1).ToList();

                    if (Textboxperformer != null)
                        obj = obj.Where(o => o.Performer.PerformerName.IndexOf(textboxperformer) != -1).ToList();

                    if (Textboxcountry != null)
                        obj = obj.Where(o => o.Artist.Country.IndexOf(textboxcountry) != -1).ToList();

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
                    Textboxartist = null;
                    Textboxcountry = null;
                    Textboxperformer = null;
                    Display = contents;
                });
            }
        }

        public ClientGroupView()
        {
            context = new CourseProj2Context();
            Artists = context.Artists.ToList();
            Performers = context.Performers.ToList();
            contents = context.Groups.ToList();

            foreach (var tmp in contents)
            {
                tmp.Artist = context.Artists.FirstOrDefault(a => a.ArtistId == tmp.ArtistId);
                tmp.Performer = context.Performers.FirstOrDefault(a => a.PerformerId == tmp.PerformerId);
            }

            Display = contents;
        }

    }
}
