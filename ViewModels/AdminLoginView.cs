using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp2
{
    
    class AdminLoginView : BaseViewModel
    {
        private string name;

        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(nameof(Name));}
        }

        private string password;

        public string Password
        {
            get => password;
            set { password = value;OnPropertyChanged(nameof(Password));}
        }

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ?? (previous = new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); })); }
        }

        private RelayCommand login;

        public RelayCommand Login
        {
            get
            {
                return login ?? (login = new RelayCommand((o) =>
                {
                    if (name == "Amogus" && password == "adminpassword") Navigation.ToAdminMenu();
                }));
            }
        }

    }
}
