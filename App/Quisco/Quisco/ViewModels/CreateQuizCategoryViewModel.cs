using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;

namespace Quisco.ViewModels
{
    public class CreateQuizCategoryViewModel : BindableBase

    {
        private QuizParams quizParams;
        private Quiz quiz;

        public CreateQuizCategoryViewModel(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            this.quiz = quizParams.Quiz;

            if (quiz.QuizCategory != null)
                SelectedItem = quiz.QuizCategory;
        }

        public string[] QuizCategories =
        {
            "History",
            "Geography",
            "Movies",
            "Celebrities",
            "Music",
            "Other"
        };

        private Object selectedItem;
        public Object SelectedItem
            {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        private InputText headerText = new InputText();

        public InputText HeaderText
        {
            get { return headerText; }
            set { SetProperty(ref headerText, value); }
        }

        public void ClickedNext(object sender, RoutedEventArgs e)
        {
            if(SelectedItem != null)
                quiz.QuizCategory = SelectedItem.ToString();

            NavigationService.Navigate(typeof(CreateQuestion), quizParams);
        }

        public void ClickedBack(object sender, RoutedEventArgs e)
        {
            if (SelectedItem != null)
                quiz.QuizCategory = SelectedItem.ToString();

            NavigationService.Navigate(typeof(CreateQuizNamePage), quizParams);
        }
    }
}
