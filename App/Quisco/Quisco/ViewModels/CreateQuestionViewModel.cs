using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Quisco.DataAccess;
using Quisco.Helpers;
using Quisco.Model;
using Quisco.Services;
using Quisco.Views;

namespace Quisco.ViewModels
{
    public class CreateQuestionViewModel : BindableBase, INotifyPropertyChanged
    {
        private Quiz quiz;
        private Question question;
        private QuizParams quizParams;
        private int questionToHandle;
        private string selectedAnswerNumber;

        public ICommand AddQuizCommand { get; set; }
        private Quizzes quizzesDataAccess = new Quizzes();
        public ObservableCollection<Quiz> Quizzes { get; set; } = new ObservableCollection<Quiz>();

        public CreateQuestionViewModel(QuizParams quizParams)
        {
            this.quizParams = quizParams;
            /*
            AddQuizCommand = new RelayCommand<Quiz>(async quiz =>
            {
                foreach (Question q in quiz.Questions) quiz.QuestionsCollection.Add(q);

                if (await quizzesDataAccess.addQuizToDbAsync(quiz))
                {
                    Quizzes.Add(quiz);
                    NavigationService.Navigate(typeof(MainPage));
                }
            }, QuizIsValid);
             */
        }

        public async void AddQuiz()
        {
            if (QuizIsValid(quiz))
            {
                foreach (Question q in quiz.Questions) quiz.QuestionsCollection.Add(q);

                if (await quizzesDataAccess.addQuizToDbAsync(quiz).ConfigureAwait(true))
                {
                    Quizzes.Add(quiz);
                    NavigationService.Navigate(typeof(MainPage));
                }
            }
        }

        public void OnLoaded()
        {
            this.quiz = quizParams.Quiz;
            this.questionToHandle = quizParams.QuestionToHandle;
            question = quiz.Questions.ElementAtOrDefault(questionToHandle - 1);

            BuildViews();
        }

        //One way bindings:

        private string quizNameHeader;
        public string QuizNameHeader
        {
            get => quizNameHeader;
            set => SetProperty(ref quizNameHeader, value);
        }

        private string questionNumberText;
        public string QuestionNumberText
        {
            get => questionNumberText;
            set => SetProperty(ref questionNumberText, value);
        }


        private ObservableCollection<Question> questionsObservableCollection = new ObservableCollection<Question>();
        public ObservableCollection<Question> QuestionsObservableCollection
        {
            get => questionsObservableCollection;
        }

        public SolidColorBrush Answer1BorderBrush { get; set; }
        public SolidColorBrush Answer2BorderBrush { get; set; }
        public SolidColorBrush Answer3BorderBrush { get; set; }
        public SolidColorBrush Answer4BorderBrush { get; set; }

        public Quiz CurrentQuiz { get => quiz; }

        //Two way bindings:

        //input texts

        private string questionInputText;
        public string QuestionInputText
        {
            get => questionInputText;
            set => SetProperty(ref questionInputText, value);
        }

        private InputText inputText1 = new InputText();

        public InputText Answer1InputText
        {
            get => inputText1;
            set => SetProperty(ref inputText1, value);
        }

        private InputText inputText2 = new InputText();
        public InputText Answer2InputText
        {
            get => inputText2;
            set => SetProperty(ref inputText2, value);
        }

        private InputText inputText3 = new InputText();
        public InputText Answer3InputText
        {
            get => inputText3;
            set => SetProperty(ref inputText3, value);
        }

        private InputText inputText4 = new InputText();
        public InputText Answer4InputText
        {
            get => inputText4;
            set => SetProperty(ref inputText4, value);
        }

        // radio buttons

        private AnswerRadioButton radioButton1 = new AnswerRadioButton();
        public AnswerRadioButton RadioButton1
        {
            get => radioButton1;
            set => SetProperty(ref radioButton1, value);
        }

        private AnswerRadioButton radioButton2 = new AnswerRadioButton();
        public AnswerRadioButton RadioButton2
        {
            get => radioButton2;
            set => SetProperty(ref radioButton2, value);
        }

        private AnswerRadioButton radioButton3 = new AnswerRadioButton();
        public AnswerRadioButton RadioButton3
        {
            get => radioButton3;
            set => SetProperty(ref radioButton3, value);
        }

        private AnswerRadioButton radioButton4 = new AnswerRadioButton();
        public AnswerRadioButton RadioButton4
        {
            get => radioButton4;
            set => SetProperty(ref radioButton4, value);
        }



        public void ClickedItem(object sender, ItemClickEventArgs e)
        {
            var clickedQuestion = (Question)e.ClickedItem;
            quizParams.QuestionToHandle = clickedQuestion.QuestionNumber;

            OnLoaded();
        }


        private void FillInputs()
        {
            QuestionInputText = "";
            question = quiz.Questions.ElementAtOrDefault(questionToHandle - 1);

            // question to be handled has already been created
            if (question != null)
            {
                QuestionInputText = question.QuestionText;

                Answer1InputText.Text = question.Answers.ElementAtOrDefault(0)?.AnswerText;
                Answer2InputText.Text = question.Answers.ElementAtOrDefault(1)?.AnswerText;
                Answer3InputText.Text = question.Answers.ElementAtOrDefault(2)?.AnswerText;
                Answer4InputText.Text = question.Answers.ElementAtOrDefault(3)?.AnswerText;
                SetCorrectAnswer();
            }
        }

        private void SetCorrectAnswer()
        {
            if (question.Answers.Count < 1) return;
            int correctAnswerNumber = question.CorrectAnswer.AnswerNumber;
            if (correctAnswerNumber == 1 && RadioButton1.IsEnabled) RadioButton1.IsChecked = true;
            if (correctAnswerNumber == 2 && RadioButton1.IsEnabled) RadioButton2.IsChecked = true;
            if (correctAnswerNumber == 3 && RadioButton1.IsEnabled) RadioButton3.IsChecked = true;
            if (correctAnswerNumber == 4 && RadioButton1.IsEnabled) RadioButton4.IsChecked = true;

        }



        public void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(typeof(CreateQuizCategory), quizParams);
        }

        public void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RefreshTextBoxColors();
        }

        private void SetAnswersToQuestion(Question question)
        {
            Answer a1 = new Answer(Answer1InputText.Text, question, 1);
            if (RadioButton1.IsChecked) question.CorrectAnswer = a1;
            question.Answers.Add(a1);

            Answer a2 = new Answer(Answer2InputText.Text, question,2);
            if (RadioButton2.IsChecked) question.CorrectAnswer = a2;
            question.Answers.Add(a2);

            Answer a3 = new Answer(Answer3InputText.Text, question,3);
            if (RadioButton3.IsChecked) question.CorrectAnswer = a3;
            question.Answers.Add(a3);

            Answer a4 = new Answer(Answer4InputText.Text, question,4);
            if (RadioButton4.IsChecked) question.CorrectAnswer = a4;
            question.Answers.Add(a4);
        }

        public void AddQuestion(object sender, RoutedEventArgs e)
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
                question.QuestionText = QuestionInputText;
                question.QuestionNumber = questionToHandle;
                question.Answers.Clear();
                SetAnswersToQuestion(question);
            }
            else
            {
                return; // InputsAreValid() will handle errors
            }

            if (newQuestion) quiz.Questions.Add(question);
            else quiz.Questions[questionToHandle - 1] = question;

            quizParams.QuestionToHandle++;
            OnLoaded();
            //            NavigationService.Navigate(typeof(CreateQuestion), quizParams);
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

        private bool QuestionTextIsValid()
        {
            return QuestionInputText.Length > 5;
        }

        private bool AnswersAreValid()
        {
            if (Answer1InputText.Text.Length < 1) return false;
            if (Answer2InputText.Text.Length < 1) return false;
            if (Answer4InputText.Text.Length > 0 && Answer3InputText.Text.Length < 1) return false;
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
            RefreshAnswerInputs();
            FillInputs();

            QuizNameHeader = quiz.QuizName;
            QuestionNumberText = "Question " + questionToHandle;
            QuestionsObservableCollection.Clear();
            foreach (Question q in quiz.Questions) QuestionsObservableCollection.Add(q);

            Answer1BorderBrush = new SolidColorBrush(Colors.Red);
            Answer2BorderBrush = new SolidColorBrush(Colors.Red);
            Answer3BorderBrush = new SolidColorBrush(Colors.Red);
            Answer4BorderBrush = new SolidColorBrush(Colors.Red);

        }


        private int GetSelectedAnswer()
        {
            if (RadioButton1.IsChecked) return 1;
            if (RadioButton2.IsChecked) return 2;
            if (RadioButton3.IsChecked) return 3;
            if (RadioButton4.IsChecked) return 4;
            return 0;
        }

        private void RefreshTextBoxColors()
        {
            int selectedRadioButton = GetSelectedAnswer();
            switch (selectedRadioButton)
            {
                case 1:
                    Answer1BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
                case 2:
                    Answer2BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
                case 3:
                    Answer3BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
                case 4:
                    Answer4BorderBrush = new SolidColorBrush(Colors.PaleGreen);
                    break;
            }
        }

        public void RefreshAnswerInputs()
        {
            Answer1InputText.Text = "";
            Answer2InputText.Text = "";
            Answer3InputText.Text = "";
            Answer4InputText.Text = "";
            Answer1InputText.IsEnabled = true;
            OnAnswer1Changed(null, null);
        }

        public void OnAnswer1Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 1
            if (Answer1InputText.Text.Length > 0)
            {
                Answer2InputText.IsEnabled = true;
                RadioButton1.IsEnabled = true;
            }
            else
            {
                RadioButton1.IsChecked = false;
                Answer2InputText.IsEnabled = false;
            }
            OnAnswer2Changed(null, null);
        }

        public void OnAnswer2Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 2
            if (Answer2InputText.Text.Length > 0 && Answer2InputText.IsEnabled)
            {
                Answer3InputText.IsEnabled = true;
                RadioButton2.IsEnabled = true;
            }
            else
            {
                RadioButton2.IsChecked = false;
                RadioButton2.IsEnabled = false;
                Answer3InputText.IsEnabled = false;
            }
            OnAnswer3Changed(null, null);
        }

        public void OnAnswer3Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 3
            if (Answer3InputText.Text.Length > 0 && Answer3InputText.IsEnabled)
            {
                Answer4InputText.IsEnabled = true;
                RadioButton3.IsEnabled = true;
            }
            else
            {
                RadioButton3.IsChecked = false;
                RadioButton3.IsEnabled = false;
                Answer4InputText.IsEnabled = false;
            }
            OnAnswer4Changed(null, null);

        }

        public void OnAnswer4Changed(object sender, TextChangedEventArgs e)
        {
            // ANSWER 4
            if (Answer4InputText.Text.Length > 0 && Answer4InputText.IsEnabled)
            {
                RadioButton4.IsEnabled = true;
            }
            else
            {
                RadioButton4.IsChecked = false;
                RadioButton4.IsEnabled = false;
            }
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
            if (!isValid) ShowErrorMessageAsync(errorMessage);
            return isValid;
        }

        private async void ShowErrorMessageAsync(string errorMessage)
        {
            MessageDialog dialog = new MessageDialog(errorMessage);
            await dialog.ShowAsync();
        }

    }
}
