using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace WpfApp2
{
    class PerformerView:BaseViewModel
    {
        private CourseProj2Context context;

        private List<Performer> contents;

        private List<Performer> display;

        private DefaultDialogService fileform = new DefaultDialogService();


        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ??= new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); }); }
        }

        public PerformerView()
        {
            context = new CourseProj2Context();
            contents = context.Performers.ToList();
            display = contents;
        }

        public List<Performer> Display
        {
            get { return display; }
            set { display = value; OnPropertyChanged(nameof(Display)); }
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
           
            get { return openfile ??= new RelayCommand((o) =>
            {
                fileform.OpenFileDialog();
            });}
        }


        private RelayCommand insertPerformer;

        public RelayCommand InsertPerformer
        {
            get
            {
                return insertPerformer ?? (insertPerformer = new RelayCommand((o) =>
                {
                    if (fileform.FilePath == null)
                        fileform.FilePath = "D:\\User\\Pictures\\placeholder-image-gray-16x9-1.png";
                    if (int.TryParse(textboxid, out int number) && fileform.FilePath.Length <= 50 && textboxname.Length <= 20)
                    {
                        Performer tmp = new Performer();
                        tmp.PerformerId = Math.Abs(number);
                        tmp.PerformerName = textboxname;
                        tmp.Logo = fileform.FilePath;
                        try
                        {
                            if (context.Performers.Find(number) == null)
                            {
                                context.Performers.Add(tmp);
                                context.SaveChanges();
                                Display = context.Performers.ToList();
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
        private Visibility transparency;

        public Visibility Transparency
        {
            get { return transparency; }
            set { transparency = value; OnPropertyChanged(nameof(Transparency)); }
        }

        private Performer selectedObject;

        public Performer SelectedObject
        {
            get { return selectedObject; }
            set
            {

                selectedObject = value;
                OnPropertyChanged(nameof(SelectedObject));
                if (selectedObject != null)
                {
                    Transparency = Visibility.Hidden;
                    Textboxid = selectedObject.PerformerId.ToString();
                    Textboxname = selectedObject.PerformerName;
                }
            } }
        private RelayCommand delete;

        public RelayCommand Delete
        {
            get
            {
                return delete ??= new RelayCommand((o =>
                {
                    try
                    {
                        context.Performers.Remove(selectedObject);
                        context.SaveChanges();
                        Display = context.Performers.ToList();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                        fileform.FilePath = null;
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
                    if (fileform.FilePath == null)
                        fileform.FilePath = "D:\\User\\Pictures\\placeholder-image-gray-16x9-1.png";
                    if (fileform.FilePath.Length <= 50 && textboxname.Length <= 20)
                    {
                        if (SelectedObject != null)
                        {
                            Performer tmp = context.Performers.Find(selectedObject.PerformerId);
                            tmp.PerformerName = textboxname;
                            tmp.Logo = fileform.FilePath;
                            try
                            {
                                context.Performers.Update(tmp);
                                context.SaveChanges();
                                Display = context.Performers.ToList();
                            }
                            catch(Exception e)
                            {
                                fileform.FilePath = null;
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
