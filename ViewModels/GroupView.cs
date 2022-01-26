using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class GroupView: BaseViewModel
    {

        private CourseProj2Context context;

        private List<Group> contents;

        private List<Group> display;

        private List<Performer> performers;

        private Visibility transparency;

        public Visibility Transparency
        {
            get { return transparency; }
            set { transparency = value; OnPropertyChanged(nameof(Transparency)); }
        }

        private Visibility transparencyactive;

        public Visibility Transparencyactive
        {
            get { return transparencyactive; }
            set { transparencyactive = value; OnPropertyChanged(nameof(Transparencyactive)); }
        }

        public List<Performer> Performers
        {
            get { return performers;}
            set { performers = value;OnPropertyChanged(nameof(Performers));}
        }

        private List<Artist> artists;

        public List<Artist> Artists
        {
            get { return artists; }
            set { artists = value;OnPropertyChanged(nameof(Artists));}
        }

        private bool active;

        public bool Active
        {
            get { return active;}
            set
            {
                active = value;OnPropertyChanged(nameof(Active));
                if (active == true)
                {
                    Transparencyactive = Visibility.Hidden;
                    datetimeboxend = DateTime.Now;
                }
                else
                {
                    Transparencyactive = Visibility.Visible;
                }
            }
        }

        private Performer textboxperfid;
        public Performer Textboxperfid
        {
            get { return textboxperfid; }
            set { textboxperfid = value; OnPropertyChanged(nameof(textboxperfid)); }
        }


        private Artist textboxartistid;
        public Artist Textboxartistid
        {
            get { return textboxartistid; }
            set { textboxartistid = value; OnPropertyChanged(nameof(textboxartistid)); }
        }

        private DateTime? datetimeboxbegin = DateTime.Now;

        public DateTime? Datetimeboxbegin
        {
            get { return datetimeboxbegin; }
            set { datetimeboxbegin = value; OnPropertyChanged(nameof(datetimeboxbegin)); }
        }

        private DateTime? datetimeboxend = DateTime.Now;

        public DateTime? Datetimeboxend
        {
            get { return datetimeboxend; }
            set { datetimeboxend = value; OnPropertyChanged(nameof(datetimeboxend)); }
        }

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ??= new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); }); }
        }

        public GroupView()
        {
           
            context = new CourseProj2Context();
            performers = context.Performers.ToList();
           
            artists = context.Artists.ToList();
            contents = context.Groups.ToList();
            foreach (var tmp in contents)
            {
                tmp.Performer = context.Performers.FirstOrDefault(a => a.PerformerId == tmp.PerformerId);
                tmp.Artist = context.Artists.FirstOrDefault(a => a.ArtistId == tmp.ArtistId);
            }
            display = contents;

        }
        public List<Group> Display
        {
            get { return display; }
            set { display = value; OnPropertyChanged(nameof(Display)); }
        }

        private RelayCommand insertGroup;

        public RelayCommand InsertGroup
        {
            get
            {
                return insertGroup ??= new RelayCommand((o) =>
                {
                    if ( datetimeboxbegin <= DateTime.Now && datetimeboxend<=DateTime.Now && datetimeboxbegin < datetimeboxend)
                    {
                        Group tmp = new Group();
                        tmp.ArtistId = textboxartistid.ArtistId;
                        tmp.PerformerId = textboxperfid.PerformerId;
                        if (active) tmp.DateEnd = null;
                        else tmp.DateEnd = datetimeboxend;
                        tmp.DateStart = (DateTime)datetimeboxbegin;
                        try
                        {
                            if (context.Groups.Find(textboxperfid.PerformerId,textboxartistid.ArtistId,datetimeboxbegin) == null)
                            {
                                context.Groups.Add(tmp);
                                context.SaveChanges();
                                Display = context.Groups.ToList();
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }

                    }
                });
            }
        }

        private Group selectedObject;

        public Group SelectedObject
        {
            get { return selectedObject; }
            set
            {

                selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
                if (selectedObject != null)
                {
                    Transparency = Visibility.Hidden;
                    Datetimeboxbegin = SelectedObject.DateStart;
                    Datetimeboxend = SelectedObject.DateEnd;
                }
            }
        }

        private RelayCommand delete;

        public RelayCommand Delete
        {
            get
            {
                return delete ??= new RelayCommand((o =>
                {
                    try
                    {
                        context.Groups.Remove(selectedObject);
                        context.SaveChanges();
                        Display = context.Groups.ToList();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    SelectedObject = null;
                    Transparency = Visibility.Visible;

                }));
            }
        }

        private RelayCommand update;

        public RelayCommand Update
        {
            get
            {
                return update ??= new RelayCommand((o =>
                {
                    if (datetimeboxend <= DateTime.Now && datetimeboxbegin <= DateTime.Now && datetimeboxbegin < datetimeboxend)
                    {
                        if (SelectedObject != null)
                        {
                            Group tmp = context.Groups.Find(selectedObject.PerformerId,selectedObject.ArtistId,selectedObject.DateStart);
                            tmp.DateEnd = datetimeboxend;
                            try
                            {
                                context.Groups.Update(tmp);
                                context.SaveChanges();
                                Display = context.Groups.ToList();
                            }
                            catch
                            {
                            }

                            Transparency = Visibility.Visible;
                            SelectedObject = null;

                        }
                    }
                }));
            }
        }

    }
}
