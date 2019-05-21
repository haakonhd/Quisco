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
        private QuizParams quizParams;
        private string quizName;

        public CreateQuizNamePage()
        {
            quizName = "";
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            quizParams = (QuizParams)e.Parameter; // get parameter
            if(quizParams?.Quiz.QuizName != null) quizName = quizParams.Quiz.QuizName;
        }

        private void ClickedBack(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }


        private void ClickedNext(object sender, RoutedEventArgs e)
        {
            string quizName = CreateQuizName.Text;
            quizParams.Quiz.QuizName = quizName;

            this.Frame.Navigate(typeof(CreateQuizCategory), quizParams);

        }

        

    }
}
