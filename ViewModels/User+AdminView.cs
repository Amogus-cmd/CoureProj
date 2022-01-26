using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class User_AdminView: BaseViewModel
    {
        private RelayCommand toAdmin;


        public RelayCommand ToAdmin
        {
            get { return toAdmin ?? (toAdmin = new RelayCommand((o) => { Navigation.ToDBAdmin(); })); }

        }


        private RelayCommand toClient;

        public RelayCommand ToClient
        {
            get { return toClient ??= new RelayCommand((o) => { Navigation.ToClient(); }); }
        }

    }
}
