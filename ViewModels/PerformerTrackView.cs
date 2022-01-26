using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class PerformerTrackView:BaseViewModel
    {
        private CourseProj2Context context;

        private List<TrackPerformer> contents;

        private List<TrackPerformer> display;

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
            set { tracks = value;OnPropertyChanged(nameof(Tracks)); }
        }


        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ??= new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); }); }
        }

        public List<TrackPerformer> Display
        {
            get { return display; }
            set { display = value;OnPropertyChanged(nameof(Display)); }
        }

        public PerformerTrackView()
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

        private Performer textboxperfid;
        public Performer Textboxperfid
        {
            get { return textboxperfid; }
            set { textboxperfid = value; OnPropertyChanged(nameof(Textboxperfid)); }
        }


        private Track textboxtrackid;
        public Track Textboxtrackid
        {
            get { return textboxtrackid; }
            set { textboxtrackid = value; OnPropertyChanged(nameof(Textboxtrackid)); }
        }


        private RelayCommand insertPerformerTrack;

        public RelayCommand InsertPerformerTrack
        {
            get
            {
                return insertPerformerTrack ??= new RelayCommand((o) =>
                {
                    TrackPerformer tmp = new TrackPerformer();
                        tmp.TrackId = textboxtrackid.TrackId;
                        tmp.PerformerId = textboxperfid.PerformerId;
                        try
                        {
                            if (context.TrackPerformers.Find(Textboxtrackid.TrackId,Textboxperfid.PerformerId) == null)
                            {
                                context.TrackPerformers.Add(tmp);
                                context.SaveChanges();
                                Display = context.TrackPerformers.ToList();
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }

                });
            }
        }

        private TrackPerformer selectedObject;

        public TrackPerformer SelectedObject
        {
            get { return selectedObject; }
            set
            {

                selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
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
                        context.TrackPerformers.Remove(selectedObject);
                        context.SaveChanges();
                        Display = context.TrackPerformers.ToList();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    SelectedObject = null;

                }));
            }
        }


    }
}
