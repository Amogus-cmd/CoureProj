using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class MainWindowView : BaseViewModel
    {
        private BaseViewModel currentMenuViewModel;
        private void NavigationStateChanged()
        {
            ///OnPropertyChanged(nameof(CurrentViewModel));
            CurrentMenuViewModel = Navigation.CurrentViewModel;
        }
        public BaseViewModel CurrentMenuViewModel
        {
            get => currentMenuViewModel;
            set
            {
                currentMenuViewModel = value;
                OnPropertyChanged(nameof(CurrentMenuViewModel));
            }
        }

        

        public MainWindowView()
        {
            Navigation.StateChanged += NavigationStateChanged;
            currentMenuViewModel = Navigation.CurrentViewModel;
        }
    }
}
