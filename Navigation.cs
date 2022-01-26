using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Navigation
    {
        public static event Action StateChanged;
        public static Stack<BaseViewModel> previousViewmModel = new Stack<BaseViewModel>();
        private static BaseViewModel currentViewModel = new User_AdminView();

        public static BaseViewModel CurrentViewModel
        {
            get => currentViewModel;
        }
        private static void Navigate(BaseViewModel viewModel)
        {
            previousViewmModel.Push(currentViewModel);
            currentViewModel?.Dispose();
            currentViewModel = viewModel;
            StateChanged?.Invoke();
        }
        public static void ToDBAdmin()
        {
            Navigate(new AdminLoginView());
        }

        public static void ToAdminMenu()
        {
            Navigate(new AdminMenuView());
        }

        public static void ToArtist()
        {
            Navigate(new ArtistView());
        }

        public static void ToGroup()
        {
            Navigate(new GroupView());
        }

        public static void ToPerformer()
        {
            Navigate(new PerformerView());
        }

        public static void ToTrack()
        {
            Navigate(new TrackView());
        }

        public static void ToPerformerTrack()
        {
            Navigate(new PerformerTrackView());
        }

        public static void ToAlbum()
        {
            Navigate(new AlbumView());
        }

        public static void ToAlbumTrack()
        {
            Navigate(new AlbumTrackView());
        }

        public static void ToClient()
        {
            Navigate(new ClientMenuView());
        }

        public static void ToClientTrack()
        {
            Navigate(new ClientTrackView());
        }

        public static void ToClientGroup()
        {
            Navigate(new ClientGroupView());
        }

        public static void ToClientPerformerTrack()
        {
            Navigate(new ClientTrackPerformerView());
        }

        public static void ToVisualization()
        {
            Navigate(new VisualizationView());
        }


        public static BaseViewModel ToPreviuosViewModel()
        {
            currentViewModel?.Dispose();
            currentViewModel = previousViewmModel.Pop();
            StateChanged?.Invoke();
            return currentViewModel;
        }
    }
}
