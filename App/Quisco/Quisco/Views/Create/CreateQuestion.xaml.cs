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

        public CreateQuestionViewModel ViewModel { get; set; }
        private QuizParams quizParams;

        public CreateQuestion()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            quizParams = (QuizParams)e.Parameter; // get parameter
            Frame rootFrame = Window.Current.Content as Frame;
            quizParams.CurrentFrame = rootFrame;
            ViewModel = new CreateQuestionViewModel(quizParams);
            DataContext = ViewModel;
        }


    }
}
