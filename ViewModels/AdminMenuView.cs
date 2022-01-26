using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class AdminMenuView: BaseViewModel
    {

        private RelayCommand previous;

        public RelayCommand Previous
        {
            get { return previous ?? (previous = new RelayCommand((o) => { Navigation.ToPreviuosViewModel(); })); }
        }

        private RelayCommand toArtist;

        public RelayCommand ToArtist
        {
            get { return toArtist ?? (toArtist = new RelayCommand((o) => { Navigation.ToArtist(); })); }

        }

        private RelayCommand togroup;

        public RelayCommand ToGroup
        {
            get { return togroup ??= new RelayCommand((o) => { Navigation.ToGroup(); }); }

        }

        private RelayCommand toPerformer;

        public RelayCommand ToPerformer
        {
            get { return toPerformer ??= new RelayCommand((o) => { Navigation.ToPerformer(); }); }
        }

        private RelayCommand totrack;

        public RelayCommand ToTrack
        {
            get { return totrack ??= new RelayCommand((o) => { Navigation.ToTrack(); }); }
        }

        private RelayCommand toperformertrack;

        public RelayCommand ToPerformerTrack
        {
            get { return toperformertrack ??= new RelayCommand((o) => { Navigation.ToPerformerTrack(); }); }
        }

        private RelayCommand toAlbum;

        public RelayCommand ToAlbum
        {
            get { return toAlbum ??= new RelayCommand((o) => { Navigation.ToAlbum(); }); }
        }

        private RelayCommand toAlbumTrack;

        public RelayCommand ToAlbumTrack
        {
            get { return toAlbumTrack ??= new RelayCommand((o) => { Navigation.ToAlbumTrack(); }); }
        }
    }
}
