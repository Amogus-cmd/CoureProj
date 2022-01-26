using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class TrackView:BaseViewModel
    {
        private CourseProj2Context context;

        private List<Track> contents;

        private List<Track> display;

        private Visibility transparency;

        public Visibility Transparency
        {
            get { return transparency; }
            set { transparency = value; OnPropertyChanged(nameof(Transparency)); }
        }

        public List<Track> Display
        {
            get { return display; }
            set { display = value;OnPropertyChanged(nameof(Display)); }
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

        private string textboxlength;

        public string Textboxlength
        {
            get { return textboxlength; }
            set { textboxlength = value;OnPropertyChanged(nameof(Textboxlength));}
        }

        private string ratingbox;

        public string Ratingbox
        {
            get { return ratingbox; }
            set { ratingbox = value;OnPropertyChanged(nameof(Ratingbox));}
        }
        public TrackView()
        {
            context = new CourseProj2Context();
            contents = context.Tracks.ToList();
            display = contents;
        }

        private RelayCommand insertTrack;

        public RelayCommand InsertTrack
        {
            get
            {
                return insertTrack ??= new RelayCommand((o) =>
                {
                    if (int.TryParse(textboxid, out int number)&& TimeSpan.TryParse(Textboxlength,out TimeSpan vremya) && textboxname.Length <=20)
                    {
                        Track tmp = new Track();
                        tmp.TrackId = Math.Abs(number);
                        tmp.Title = textboxname;
                        tmp.Rating = Ratingbox;
                        tmp.TrackLength = vremya;
                        try
                        {
                            if (context.Tracks.Find(number) == null)
                            {
                                context.Tracks.Add(tmp);
                                context.SaveChanges();
                                Display = context.Tracks.ToList();
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

        private Track selectedObject;

        public Track SelectedObject
        {
            get { return selectedObject; }
            set
            {

                selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
                if (selectedObject != null)
                {
                    Transparency = Visibility.Hidden;
                    Textboxid = selectedObject.TrackId.ToString();
                    Textboxname = selectedObject.Title;
                    Textboxlength = selectedObject.TrackLength.ToString();
                    Ratingbox = selectedObject.Rating;
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
                        context.Tracks.Remove(selectedObject);
                        context.SaveChanges();
                        Display = context.Tracks.ToList();
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
                    if ( TimeSpan.TryParse(Textboxlength, out TimeSpan vremya) && textboxname.Length <= 20)
                    {
                        if (SelectedObject != null)
                        {
                            Track tmp = context.Tracks.Find(selectedObject.TrackId);
                            tmp.Title = textboxname;
                            tmp.Rating = Ratingbox;
                            tmp.TrackLength = vremya;
                            try
                            {
                                context.Tracks.Update(tmp);
                                context.SaveChanges();
                                Display = context.Tracks.ToList();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
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
