using System;

using Quisco.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Quisco.Views
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public MainPage()
        {
            InitializeComponent();
        }

        private void Clicked_Create(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateQuizNamePage));
        }
        private void Clicked_Take(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
