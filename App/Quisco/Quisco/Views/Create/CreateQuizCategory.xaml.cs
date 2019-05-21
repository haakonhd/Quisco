using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Quisco.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quisco.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateQuizCategory : Page
    {
        private Quiz quiz;

        public string[] QuizCategories =
        {
            "History",
            "Geography",
            "Movies",
            "Celebrities",
            "Music"
        };
        public CreateQuizCategory()
        {
            this.InitializeComponent();
            QuizCategoryComboBox.ItemsSource = QuizCategories;
        }

        private void Clicked_Next(object sender, RoutedEventArgs e)
        {
            string quizCategory = "";
            try
            {
                quizCategory = QuizCategoryComboBox.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                //TODO: Handle error: no category selected
            }

            quiz.QuizCategory = quizCategory;
            //set questionToBeHandled to 1 for the first time.
            //If the user has clicked "back" from CreateQuestion it will have a set value and is not to be edited
            if(quiz.QuestionToBeHandled == 0)
                quiz.QuestionToBeHandled = 1;
            this.Frame.Navigate(typeof(CreateQuestion), quiz);
        }
        private void Clicked_Back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CreateQuizNamePage), quiz);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            quiz = (Quiz)e.Parameter; // get parameter
        }

    }
}
