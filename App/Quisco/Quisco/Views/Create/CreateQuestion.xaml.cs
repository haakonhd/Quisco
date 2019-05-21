using System;
using System.Collections;
using System.Collections.Generic;
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
        static Uri quizzesBaseUri = new Uri("http://localhost:53710/api/Quizzes");

        HttpClient httpClient = new HttpClient();

        public CreateQuestion()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            quiz = (Quiz)e.Parameter; // get parameter from last view
            if (quiz != null)
            {
                QuizNameHeader.Text = quiz.QuizName;
                QuestionNumberTextBlock.Text = "Question " + quiz.QuestionToBeHandled;
                FillInputs();
            }
        }

        private void FillInputs()
        {
            question = quiz.Questions.ElementAtOrDefault(quiz.QuestionToBeHandled - 1);
            // question to be handled has already been created
            if (question != null)
            {
                QuestionTextInput.Text = question.QuestionText;

                var i = 0;
                foreach (TextBox answerTextBox in AnswerGrid.Children)
                {
                    var answer = question.Answers.ElementAtOrDefault(i);
                    if (answer != null) answerTextBox.Text = answer;
                    i++;
                }
            }
        }

        private void AddQuestion(object sender, RoutedEventArgs e)
        {
            bool newQuestion;
            // if creating a new question, the value will be null
            question = quiz.Questions.ElementAtOrDefault(quiz.QuestionToBeHandled - 1);
            if (question == null)
            {
                question = new Question();
                newQuestion = true;
            }
            else
                newQuestion = false;

            if (InputsAreValid())
            {
                question.QuestionText = QuestionTextInput.Text;

                question.Answers.Clear();
                foreach (TextBox answerTextBox in AnswerGrid.Children)
                    question.Answers.Add(answerTextBox.Text);
            }
            else
            {
                //TODO: handle error: inputs not valid
            }

            if (newQuestion) quiz.Questions.Add(question);
            else quiz.Questions[quiz.QuestionToBeHandled - 1] = question;
            int i = 0;
        }


        private bool InputsAreValid()
        {
            //TODO: improve this whole function
            if (QuestionTextInput.Text.Length < 5) return false;
            if (Answer1Input.Text.Length < 1) return false;
            if (Answer2Input.Text.Length < 1) return false;
            if (Answer4Input.Text.Length < 1 && Answer3Input.Text.Length > 1) return false;

            return true;
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            
            this.Frame.Navigate(typeof(CreateQuizCategory), quiz);
        }

        private void QuizComplete(object sender, RoutedEventArgs e)
        {

            /*
            Save quiz in db
            var json = JsonConvert.SerializeObject(quiz);

            var result = await httpClient.PostAsync(quizzesBaseUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            result = await httpClient.GetAsync(quizzesBaseUri);


            json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);

            var dialog = new MessageDialog(quizList.Last() + " was added to Db. Sadly this is all the app can do for now.", "Quiz added");
            await dialog.ShowAsync();
            this.Frame.Navigate(typeof(MainPage));
             */
        }

        private void AddQuestionToQuiz(Question question, string questionInputText)
        {
            if (question == null)
            {
                questionInputText = QuestionTextInput.Text;
                question = new Question(questionInputText);
                quiz.Questions.Add(question);
                //TODO: error if no input
            }
        }

    }
}
