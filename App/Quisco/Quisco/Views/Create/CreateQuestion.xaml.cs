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
        private string questionNumberText;
        private ObservableCollection<Question> questionsObservableCollection = new ObservableCollection<Question>();
        static Uri quizzesBaseUri = new Uri("http://localhost:53710/api/Quizzes");
        private string quizName;

        HttpClient httpClient = new HttpClient();

        public CreateQuestion()
        {
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
        }

        private void FillInputs()
        {
            question = quiz.Questions.ElementAtOrDefault(questionToHandle - 1);
            // question to be handled has already been created
            if (question != null)
            {
                QuestionTextInput.Text = question.QuestionText;

                var i = 0;
                foreach (TextBox answerTextBox in AnswerGrid.Children)
                {
                    var answer = question.Answers.ElementAtOrDefault(i);
                    if (!string.IsNullOrEmpty(answer)) answerTextBox.Text = answer;
                    i++;
                }
            }
        }

        private void AddQuestion(object sender, RoutedEventArgs e)
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
                foreach (TextBox answerTextBox in AnswerGrid.Children)
                    question.Answers.Add(answerTextBox.Text);
            }
            else
            {
                return;
                //TODO: handle error: inputs not valid
            }

            if (newQuestion) quiz.Questions.Add(question);
            else quiz.Questions[questionToHandle - 1] = question;

            quizParams.QuestionToHandle++;
            Frame.Navigate(typeof(CreateQuestion), quizParams);
        }


        private bool InputsAreValid()
        {
            //TODO: improve this whole function
            if (QuestionTextInput.Text.Length < 5) return false;
            if (Answer1Input.Text.Length < 1) return false;
            if (Answer2Input.Text.Length < 1) return false;
            if (Answer4Input.Text.Length > 1 && Answer3Input.Text.Length < 1) return false;

            return true;
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
            
            this.Frame.Navigate(typeof(CreateQuizCategory), quizParams);
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

    }
}
