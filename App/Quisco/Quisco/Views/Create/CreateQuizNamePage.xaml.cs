using System;
using Windows.UI.Xaml;
using Quisco.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Quisco.Model;

namespace Quisco.Views
{
    public sealed partial class CreateQuizNamePage : Page
    {
        private Quiz quiz;
        public CreateQuizNameViewModel ViewModel { get; } = new CreateQuizNameViewModel();
        public string QuizName = "";

        public CreateQuizNamePage()
        {
            QuizName = "";
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            quiz = (Quiz)e.Parameter; // get parameter
            if(quiz.QuizName != null) QuizName = quiz.QuizName;
        }

        private void ClickedBack(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }


        private void ClickedNext(object sender, RoutedEventArgs e)
        {
            string quizName = CreateQuizName.Text;
            this.quiz.QuizName = quizName;

            this.Frame.Navigate(typeof(CreateQuizCategory), quiz);

        }

        

    }
}
