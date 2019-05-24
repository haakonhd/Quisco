using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Quisco.DataAccess;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;

namespace Quisco.ViewModels
{
    public class QuizViewModel
    {
        private Quiz quiz;

        public ICommand AddQuizCommand { get; set; }
        private Quizzes quizzesDataAccess = new Quizzes();
        public ObservableCollection<Quiz> Quizzes { get; set; } = new ObservableCollection<Quiz>();

        public QuizViewModel()
        {
            AddQuizCommand = new RelayCommand<Quiz>(async quiz =>
            {
                foreach (Question q in quiz.Questions) quiz.QuestionsCollection.Add(q);

                if (await quizzesDataAccess.addQuizToDbAsync(quiz))
                {
                    Quizzes.Add(quiz);
                    NavigationService.Navigate(typeof(MainPage));
                }
            }, QuizIsValid);
        }

        public Task OnNavigatedToAsync(object parameter, NavigationMode mode)
        {
            if (parameter != null)
            {
                quiz = parameter as Quiz;
            }
            return Task.CompletedTask;
        }

        private bool QuizIsValid(Quiz quiz)
        {
            bool isValid = true;
            string errorMessage = "";
            if (quiz == null) return false;
            if (quiz.Questions.Count < 3)
            {
                isValid = false;
                errorMessage = "Error: You need at least 3 questions.";
            }

            if (quiz.Questions.Count > 20)
            {
                isValid = false;
                errorMessage = "You can have maximum 20 questions";
            }
            if(!isValid) ShowErrorMessage(errorMessage);
            return isValid;
        }

        private async void ShowErrorMessage(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
    }
}
