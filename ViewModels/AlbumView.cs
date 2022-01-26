using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class AlbumView: BaseViewModel
    {
        private CourseProj2Context context;

        private List<Album> contents;

        private List<Album> display;

        private DefaultDialogService fileform = new DefaultDialogService();

        public List<Album> Display
        {
            get { return display; }
            set { display = value;OnPropertyChanged(nameof(Display)); }
        }

        private Visibility transparency;

        public Visibility Transparency
        {
            get { return transparency; }
            set { transparency = value; OnPropertyChanged(nameof(Transparency)); }
        }

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ??= new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); }); }
        }

        private string textboxid;
        public string Textboxid
        {
            get { return textboxid; }
            set { textboxid = value; OnPropertyChanged(nameof(textboxid)); }
        }

        private string textboxname;

        public string Textboxname
        {
            get { return textboxname; }
            set { textboxname = value; OnPropertyChanged(nameof(textboxname)); }
        }

        private RelayCommand openfile;

        public RelayCommand OpenFile
        {

            get
            {
                return openfile ??= new RelayCommand((o) =>
                {
                    fileform.OpenFileDialog();
                });
            }
        }

        private DateTime? datetimebox = DateTime.Now;

        public DateTime? Datetimebox
        {
            get { return datetimebox; }
            set { datetimebox = value; OnPropertyChanged(nameof(datetimebox)); }
        }

        private RelayCommand insertAlbum;

        public RelayCommand InsertAlbum
        {
            get
            {
                return insertAlbum ?? (insertAlbum = new RelayCommand((o) =>
                {
                    if (fileform.FilePath == null)
                        fileform.FilePath = "D:\\User\\Pictures\\placeholder-image-gray-16x9-1.png";
                    if (int.TryParse(textboxid, out int number) && fileform.FilePath.Length <= 50 && textboxname.Length <=20)
                    {
                        Album tmp = new Album();
                        tmp.AlbumId = Math.Abs(number);
                        tmp.Title = textboxname;
                        tmp.Cover = fileform.FilePath;
                        tmp.ReleaseDate = datetimebox;
                        try
                        {
                            if (context.Albums.Find(number) == null)
                            {
                                context.Albums.Add(tmp);
                                context.SaveChanges();
                                Display = context.Albums.ToList();
                            }
                        }
                        catch (Exception e)
                        {
                            fileform.FilePath = null;
                            MessageBox.Show(e.Message);
                        }

                    }
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
                    if (fileform.FilePath == null)
                        fileform.FilePath = "D:\\User\\Pictures\\placeholder-image-gray-16x9-1.png";
                    if (datetimebox <= DateTime.Now && fileform.FilePath.Length <=50 && textboxname.Length <= 20)
                    {
                        if (SelectedObject != null)
                        {
                            Album tmp = context.Albums.Find(selectedObject.AlbumId);
                            tmp.ReleaseDate = datetimebox;
                            tmp.Cover = fileform.FilePath;
                            tmp.Title = textboxname;
                            try
                            {
                                context.Albums.Update(tmp);
                                context.SaveChanges();
                                Display = context.Albums.ToList();
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

        private Album selectedObject;

        public Album SelectedObject
        {
            get { return selectedObject; }
            set
            {

                selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
                if (selectedObject != null)
                {
                    Transparency = Visibility.Hidden;
                    Textboxid = selectedObject.AlbumId.ToString();
                    Textboxname = selectedObject.Title;
                    Datetimebox = selectedObject.ReleaseDate;
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
                        context.Albums.Remove(selectedObject);
                        context.SaveChanges();
                        Display = context.Albums.ToList();
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

        public AlbumView()
        {
            context = new CourseProj2Context();
            contents = context.Albums.ToList();

            foreach (var tmp in contents)
            {
                List<AlbumTrackList> index = context.AlbumTrackLists.Where(a => a.AlbumId == tmp.AlbumId).ToList();
                tmp.TotalLength = TimeSpan.Zero;
                foreach (var i in index)
                {
                    tmp.TotalLength += context.Tracks.First(a => a.TrackId == i.TrackId).TrackLength;
                }
                tmp.TrackCount = context.AlbumTrackLists.Where(a => a.AlbumId == tmp.AlbumId).Count();
            }

            Display = contents;
        }

    }
}
