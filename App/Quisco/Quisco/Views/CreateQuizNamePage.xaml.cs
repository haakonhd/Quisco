using System;
using Windows.UI.Xaml;
using Quisco.ViewModels;

using Windows.UI.Xaml.Controls;
using Quisco.Model;

namespace Quisco.Views
{
    public sealed partial class CreateQuizNamePage : Page
    {
        public CreateQuizNameViewModel ViewModel { get; } = new CreateQuizNameViewModel();

        public CreateQuizNamePage()
        {
            InitializeComponent();
        }

        private void Clicked_next(object sender, RoutedEventArgs e)
        {
            string quizName = CreateQuizName.Text;
            Quiz quiz = new Quiz(quizName);

            this.Frame.Navigate(typeof(CreateQuizCategory), quiz);

        }

        

    }
}
