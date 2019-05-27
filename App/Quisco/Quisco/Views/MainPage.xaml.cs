using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Quisco.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;
using Quisco.Views.Take;

namespace Quisco.Views
{
    public sealed partial class MainPage : Page
    {
        private Quiz quiz;

        public MainViewModel ViewModel { get; } = new MainViewModel();

        static Uri quizzesBaseUri = new Uri("http://localhost:53710/api/Quizzes");

        HttpClient _httpClient = new HttpClient();

        private ObservableCollection<Quiz> quizzesObservableCollection = new ObservableCollection<Quiz>();

        public MainPage()
        {
            InitializeComponent();

        }

        private void CreateQuiz(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Quiz quiz = new Quiz();
            QuizParams quizParams = new QuizParams(quiz, 1);

            this.Frame.Navigate(typeof(CreateQuizNamePage), quizParams);
        }

        private void TakeQuiz(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TakeBotOrHuman));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillQuizList();
        }

        public async Task<Quiz[]> GetQuizList()
        {
            var result = await _httpClient.GetAsync(quizzesBaseUri);
            var json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);
            return quizList;
        }

        public async void FillQuizList()
        {
            //TODO: errorhandling no internet
            var quizList = await GetQuizList().ConfigureAwait(true);
            // inserts quizzes to the listView with the newest first
            foreach(var quiz in quizList)
            {
                quizzesObservableCollection.Add(quiz);
            }
        }

        public void ClickItemList(object sender, ItemClickEventArgs e)
        {
            var clickedQuestion = (Quiz)e.ClickedItem;

//            Frame.Navigate(typeof(CreateQuestion), quizParams);
        }
    }
}
