using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;
using System.Linq;
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

        HttpClient _httpClient = new HttpClient();

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
            }
        }

        private async void Clicked_Next(object sender, RoutedEventArgs e)
        {
            string questionText = "";
            try
            {
                question = new Question(QuestionTextInput.Text);
            }
            catch (NullReferenceException)
            {
                //TODO: Handle error: no category selected
            }
            //            quiz.Questions.Add(question);

            var json = JsonConvert.SerializeObject(quiz);

            var result = await _httpClient.PostAsync(quizzesBaseUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));
            result = await _httpClient.GetAsync(quizzesBaseUri);


            json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);

            var dialog = new MessageDialog(quizList.Last() + " was added to Db. Sadly this is all the app can do for now.", "Quiz added");
            await dialog.ShowAsync();
            this.Frame.Navigate(typeof(MainPage));

        }

    }
}
