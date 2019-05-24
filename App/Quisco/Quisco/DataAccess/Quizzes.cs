using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;
using Newtonsoft.Json;
using Quisco.Model;

namespace Quisco.DataAccess
{
    public class Quizzes
    {
        readonly HttpClient httpClient = new HttpClient();
        static readonly Uri quizzesBaseUri = new Uri("http://localhost:53710/api/Quizzes");

        public async Task<bool> addQuizToDbAsync(Quiz quiz)
        {
            var json = JsonConvert.SerializeObject(quiz, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            var result = await httpClient.PostAsync(quizzesBaseUri, new HttpStringContent(json, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json"));

            return result.IsSuccessStatusCode;
        }

        /*
         *
        public async void Task<Quiz[]> GetQuizzes()
        {

            var result = await httpClient.GetAsync(quizzesBaseUri, );
            json = await result.Content.ReadAsStringAsync();
            var quizList = JsonConvert.DeserializeObject<Quiz[]>(json);

            var dialog = new MessageDialog(quizList.Last() + " was added to Db", "Quiz added");
            await dialog.ShowAsync();

        }
         */
    }
}
