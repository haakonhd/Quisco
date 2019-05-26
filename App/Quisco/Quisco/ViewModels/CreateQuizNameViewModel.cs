using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;

namespace Quisco.ViewModels
{
    public class CreateQuizNameViewModel : BindableBase, INotifyPropertyChanged
    {
        private QuizParams quizParams;
        private Quiz quiz;
        private int questionToHandle;

        public CreateQuizNameViewModel(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            quiz = quizParams.Quiz;
            questionToHandle = quizParams.QuestionToHandle;
            if(quiz.QuizName != null)
                QuestionInputText.Text = quiz.QuizName;
        }



        private InputText questionInputText = new InputText();
        public InputText QuestionInputText
        {
            get { return questionInputText; }
            set
            {
                this.SetProperty(ref questionInputText, value);
//                currentQuizText = value;
            }
        }

        private void ClickedBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(CreateQuestion), quizParams);
        }

        public void ClickedNext(object sender, RoutedEventArgs e)
        {
            string quizName = QuestionInputText.Text;
            quizParams.Quiz.QuizName = quizName;
            NavigationService.Navigate(typeof(CreateQuestion), quizParams);

        }
    }
}
