using Windows.UI.Xaml;
using Quisco.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Quisco.Model;

namespace Quisco.Views
{
    public sealed partial class CreateQuizNamePage : Page
    {
        public CreateQuizNameViewModel ViewModel { get; set; }
        private QuizParams quizParams;

        public CreateQuizNamePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            quizParams = (QuizParams)e.Parameter; // get parameter
            ViewModel = new CreateQuizNameViewModel(quizParams);

            Frame rootFrame = Window.Current.Content as Frame;
        }
    }
}
