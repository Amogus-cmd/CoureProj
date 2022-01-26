using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class AlbumTrackView:BaseViewModel
    {
        private CourseProj2Context context;

        private List<AlbumTrackList> contents;

        private List<AlbumTrackList> display;

        public List<AlbumTrackList> Display
        {
            get { return display; }
            set { display = value; OnPropertyChanged(nameof(Display)); }
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

        private Album textboxalbumid;
        public Album Textboxalbumid
        {
            get { return textboxalbumid; }
            set { textboxalbumid = value; OnPropertyChanged(nameof(Textboxalbumid)); }
        }


        private Track textboxtrackid;
        public Track Textboxtrackid
        {
            get { return textboxtrackid; }
            set { textboxtrackid = value; OnPropertyChanged(nameof(Textboxtrackid)); }
        }

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ??= new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); }); }
        }

        public AlbumTrackView()
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

        private RelayCommand insertAlbumTrack;

        public RelayCommand InsertAlbumTrack
        {
            get
            {
                return insertAlbumTrack ??= new RelayCommand((o) =>
                {
                    AlbumTrackList tmp = new AlbumTrackList();
                    tmp.TrackId = textboxtrackid.TrackId;
                    tmp.AlbumId = textboxalbumid.AlbumId;
                    try
                    {
                        if (context.AlbumTrackLists.Find(Textboxalbumid.AlbumId,Textboxtrackid.TrackId) == null)
                        {
                            context.AlbumTrackLists.Add(tmp);
                            context.SaveChanges();
                            Display = context.AlbumTrackLists.ToList();
                        }
                         var contentss = context.Albums.ToList();

                        foreach (var tmep in contentss)
                        {
                            List<AlbumTrackList> index = context.AlbumTrackLists.Where(a => a.AlbumId == tmep.AlbumId).ToList();
                            tmep.TotalLength = TimeSpan.Zero;
                            foreach (var i in index)
                            {
                                tmep.TotalLength += context.Tracks.First(a => a.TrackId == i.TrackId).TrackLength;
                            }
                            tmep.TrackCount = context.AlbumTrackLists.Where(a => a.AlbumId == tmep.AlbumId).Count();
                            context.Albums.Update(tmep);
                        }

                        context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                });
            }
        }

        private AlbumTrackList selectedObject;

        public AlbumTrackList SelectedObject
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
                        context.AlbumTrackLists.Remove(selectedObject);
                        context.SaveChanges();
                        Display = context.AlbumTrackLists.ToList();
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
