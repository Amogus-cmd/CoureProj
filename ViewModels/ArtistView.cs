using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    class ArtistView: BaseViewModel
    {
        private CourseProj2Context context;

        private List<Artist> contents;

        private List<Artist> display;

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ?? (previous = new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); })); }
        }

        public ArtistView()
        {
         context = new CourseProj2Context();

         contents = context.Artists.ToList();
         display = contents;

        }

        public List<Artist> Display
        {
            get { return display;}
            set { display = value; OnPropertyChanged(nameof(Display));}
        }


        private string textboxid;
        public string Textboxid
        {
            get { return textboxid; }
            set { textboxid = value;OnPropertyChanged(nameof(textboxid));}
        }

        private string textboxname;

        public string Textboxname
        {
            get { return textboxname; }
            set { textboxname = value;OnPropertyChanged(nameof(textboxname));}
        }

        private DateTime? datetimebox = DateTime.Now;

        public DateTime? Datetimebox
        {
            get { return datetimebox; }
            set { datetimebox = value;OnPropertyChanged(nameof(datetimebox));}
        }

        private string textboxcountry;

        public string Textboxcountry
        {
            get { return textboxcountry; }
            set { textboxcountry = value;OnPropertyChanged(nameof(textboxcountry)); }
        }

        private RelayCommand insertArtist;

        public RelayCommand InsertArtist
        {
            get { return insertArtist ??= new RelayCommand((o) =>
            {
                int number;
                if (int.TryParse(textboxid, out number) && datetimebox <= DateTime.Now && textboxcountry.Length <=20 && textboxname.Length <= 20)
                {
                    Artist tmp = new Artist();
                    tmp.ArtistId = Math.Abs(number);
                    tmp.ArtistName = textboxname;
                    tmp.DateOfBirth = datetimebox;
                    tmp.Country = textboxcountry;
                    try
                    {
                        if(context.Artists.Find(number) == null){
                            context.Artists.Add(tmp);
                            context.SaveChanges();
                            Display = context.Artists.ToList();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);}
                    
                }
            }); }
        }

        private Visibility transparency;

        public  Visibility Transparency
        {
            get { return transparency; }
            set { transparency = value;OnPropertyChanged(nameof(Transparency)); }
        } 

        private Artist selectedObject;

        public Artist SelectedObject
        {
            get { return selectedObject; }
            set
            {
                
                selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
                if (selectedObject != null)
                {
                    Transparency = Visibility.Hidden;
                    Textboxid = selectedObject.ArtistId.ToString();
                    Textboxname = selectedObject.ArtistName;
                    Textboxcountry = selectedObject.Country;
                    Datetimebox = selectedObject.DateOfBirth;
                }
            }

        }

        private RelayCommand delete;

        public RelayCommand Delete
        {
            get
            { return delete ??= new RelayCommand((o =>
            {
                try
                {
                    context.Artists.Remove(selectedObject);
                    context.SaveChanges();
                    Display = context.Artists.ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);} 
                
                SelectedObject = null;
                Transparency = Visibility.Visible;
            })); } }


        private RelayCommand update;

        public RelayCommand Update
        {
            get { return update ??= new RelayCommand((o =>
            {
                if (datetimebox <= DateTime.Now && textboxcountry.Length <= 20 && textboxname.Length <= 20)
                {
                    if (SelectedObject != null)
                    {
                        Artist tmp = context.Artists.Find(selectedObject.ArtistId);
                        tmp.ArtistName = textboxname;
                        tmp.Country = textboxcountry;
                        tmp.DateOfBirth = datetimebox;
                        try
                        {
                            context.Artists.Update(tmp);
                            context.SaveChanges();
                            Display = context.Artists.ToList();
                        }
                        catch
                        {
                        }

                        Transparency = Visibility.Visible;
                        SelectedObject = null;
                       
                    }
                }
            })); }
        }
    }
}
