using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Quisco.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Quisco.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateQuestion : Page
    {
        private Quiz quiz;
        private Question question;
        private QuizParams quizParams;
        private int questionToHandle;
        private string selectedAnswerNumber;
        private ObservableCollection<Question> questionsObservableCollection = new ObservableCollection<Question>();

        //Binding variables
        private string quizName;
        private string questionNumberText;

        private string answer1BorderBrush;
        private string answer2BorderBrush;
        private string answer3BorderBrush;
        private string answer4BorderBrush;

        public QuizViewModel ViewModel { get; }

        public CreateQuestion()
        {
            ViewModel = new QuizViewModel();
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            quizParams = (QuizParams)e.Parameter; // get parameter from last view
            quiz = quizParams.Quiz;
            questionToHandle = quizParams.QuestionToHandle;
            if (quiz != null)
            {
                BuildViews();
                
                FillInputs();
            }

            answer1BorderBrush = "Red";
            answer2BorderBrush = "Red";
            answer3BorderBrush = "Red";
            answer4BorderBrush = "Red";
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            RefreshAnswerInputs();
        }

        private void FillInputs()
        {
            question = quiz.Questions.ElementAtOrDefault(questionToHandle - 1);

            // question to be handled has already been created
            if (question != null)
            {
                QuestionTextInput.Text = question.QuestionText;

                var i = 0;
                TextBox answerTextBox = null;
                foreach (var answerGridChild in AnswerGrid.Children)
                {
                    if (answerGridChild.GetType() == typeof(TextBox))
                    {
                        answerTextBox = (TextBox)answerGridChild;
                        if (question.Answers.ElementAtOrDefault(i) != null)
                        {
                            var answer = question.Answers.ElementAtOrDefault(i);
                            if (!string.IsNullOrEmpty(answer.AnswerText))   
                                answerTextBox.Text = answer.AnswerText;
                        }
                        i++;
                    }
                }
            }
        }

        private  void AddQuestion(object sender, RoutedEventArgs e)
        {
            bool newQuestion;
            // if creating a new question, the value will be null
            question = quiz.Questions.ElementAtOrDefault(questionToHandle - 1);
            if (question == null)
            {
                question = new Question();
                newQuestion = true;
            }
            else newQuestion = false;

            if (InputsAreValid())
            {
                question.QuestionText = QuestionTextInput.Text;
                question.QuestionNumber = questionToHandle;
                question.Answers.Clear();
                TextBox answerTextBox = null;
                foreach (var answerGridChild in AnswerGrid.Children)
                {
                    if (answerGridChild.GetType() == typeof(TextBox))
                    {
                        answerTextBox = (TextBox)answerGridChild;
                        if (answerTextBox.IsEnabled && answerTextBox.Text.Length > 0)
                            question.Answers.Add(new Answer(answerTextBox.Text, question));
                    }
                }
            }
            else
            {
                return;
            }

            if (newQuestion) quiz.Questions.Add(question);
            else quiz.Questions[questionToHandle - 1] = question;

            quizParams.QuestionToHandle++;
            Frame.Navigate(typeof(CreateQuestion), quizParams);
        }


        private bool InputsAreValid()
        {
            bool valid;
            valid = QuestionTextIsValid() && AnswersAreValid() && AnswerIsSelected();
            if (!valid)
            {
                if (!QuestionTextIsValid()) ShowErrorMessageAsync("Error: Question text needs at least 5 characters.");
                else if (!AnswersAreValid()) ShowErrorMessageAsync("Error: At least 2 answers must be entered.");
                else ShowErrorMessageAsync("Error: Please select a correct answer.");
            }

            return valid;
        }

        private async void ShowErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }
        private async void ShowErrorMessageAsync1()
        {
            MessageDialog dialog = new MessageDialog("yooo");
            await dialog.ShowAsync();
        }

        private bool QuestionTextIsValid()
        {
            return QuestionTextInput.Text.Length > 5;
        }

        private bool AnswersAreValid()
        {
            if (Answer2Input.Text.Length < 1) return false;
            return true;
        }

        private bool AnswerIsSelected()
        {
            if (RadioButton1.IsChecked == true) return true;
            if (RadioButton2.IsChecked == true) return true;
            if (RadioButton3.IsChecked == true) return true;
            if (RadioButton4.IsChecked == true) return true;
            return false;
        }


        private void BuildViews()
        {
            quizName = quiz.QuizName;
            questionNumberText = "Question " + questionToHandle;
            foreach (Question q in quiz.Questions)  questionsObservableCollection.Add(q);
        }

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var clickedQuestion = (Question)e.ClickedItem;
            quizParams.QuestionToHandle = clickedQuestion.QuestionNumber;

            Frame.Navigate(typeof(CreateQuestion), quizParams);
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            
            Frame.Navigate(typeof(CreateQuizCategory), quizParams);
        }


        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            RefreshTextBoxColors();
        }

        private int GetSelectedAnswer()
        {
            if (RadioButton1.IsChecked == true) return 1;
            if (RadioButton2.IsChecked == true) return 2;
            if (RadioButton3.IsChecked == true) return 3;
            if (RadioButton4.IsChecked == true) return 4;
            return 0;
        }

        private void RefreshTextBoxColors()
        {
            int selectedRadioButton = GetSelectedAnswer();
            switch (selectedRadioButton)
            {
                case 1:
                    answer1BorderBrush = "Green";
                    break;
                case 2:
                    answer2BorderBrush = "Green";
                    break;
                case 3:
                    answer3BorderBrush = "Green";
                    break;
                case 4:
                    answer4BorderBrush = "Green";
                    break;
            }
        }

        public void RefreshAnswerInputs()
        {
            OnAnswer1Changed(null,null);
            OnAnswer2Changed(null,null);
            OnAnswer3Changed(null,null);
            OnAnswer4Changed(null,null);
        }

        public void OnAnswer1Changed(object sender, RoutedEventArgs e)
        {
            // ANSWER 1
            if (Answer1Input.Text.Length > 0)
            {
                Answer2Input.IsEnabled = true;
                RadioButton1.IsEnabled = true;
            }
            else
            {
                RadioButton1.IsChecked = false;
                Answer2Input.IsEnabled = false;
            }
            OnAnswer2Changed(null,null);
        }

        public void OnAnswer2Changed(object sender, RoutedEventArgs e)
        {
            // ANSWER 2
            if (Answer2Input.Text.Length > 0 && Answer2Input.IsEnabled)
            {
                Answer3Input.IsEnabled = true;
                RadioButton2.IsEnabled = true;
            }
            else
            {
                RadioButton2.IsChecked = false;
                RadioButton2.IsEnabled = false;
                Answer3Input.IsEnabled = false;
            }
            OnAnswer3Changed(null,null);
        }

        public void OnAnswer3Changed(object sender, RoutedEventArgs e)
        {
            // ANSWER 3
            if (Answer3Input.Text.Length > 0 && Answer3Input.IsEnabled)
            {
                Answer4Input.IsEnabled = true;
                RadioButton3.IsEnabled = true;
            }
            else
            {
                RadioButton3.IsChecked = false;
                RadioButton3.IsEnabled = false;
                Answer4Input.IsEnabled = false;
            }
            OnAnswer4Changed(null,null);

        }

        public void OnAnswer4Changed(object sender, RoutedEventArgs e)
        {
            // ANSWER 4
            if (Answer4Input.Text.Length > 0 && Answer4Input.IsEnabled)
            {
                RadioButton4.IsEnabled = true;
            }
            else
            {
                RadioButton4.IsChecked = false;
                RadioButton4.IsEnabled = false;
            }
        }
    }
}
