using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class ClientMenuView:BaseViewModel
    {
        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ?? (previous = new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); })); }
        }

        private RelayCommand toTrack;

        public RelayCommand ToTrack
        {
            get { return toTrack ??= (new RelayCommand((o) => { Navigation.ToClientTrack(); })); }
        }

        private RelayCommand toGroup;

        public RelayCommand ToGroup
        {
            get { return toGroup ??= new RelayCommand((o) => {Navigation.ToClientGroup(); }); }
        }

        private RelayCommand toTrackPerformer;

        public RelayCommand ToTrackPerformer
        {
            get { return toTrackPerformer ??= new RelayCommand((o) => {Navigation.ToClientPerformerTrack(); }); }
        }

        private RelayCommand toVisualization;

        public RelayCommand ToVisualization
        {
            get { return toVisualization ??= new RelayCommand((o) => { Navigation.ToVisualization(); }); }
        }

    }
}
