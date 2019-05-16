using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Quisco.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;

namespace Quisco.Views
{
    public sealed partial class MainPage : Page
    {
        private Quiz quiz;

        public MainViewModel ViewModel { get; } = new MainViewModel();

        static Uri quizzesBaseUri = new Uri("http://localhost:53710/api/Quizzes");

        HttpClient _httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();

        }

        private void Clicked_Create(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Quiz quiz = new Quiz();
            this.Frame.Navigate(typeof(CreateQuizNamePage), quiz);
        }
        private void Clicked_Take(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
//            FillQuizList();
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
            var quizList = await GetQuizList().ConfigureAwait(true);
            // inserts quizzes to the listView with the newest first
            foreach(var quiz in quizList)
            {
                NewestQuizzesListView.Items?.Insert(0, quiz);
            }
        }
    }
}
