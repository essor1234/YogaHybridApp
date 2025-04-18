using YogaHybridApp.Views;
using YogaHybridApp.Views.Account;
using YogaHybridApp.Views.Courses;

namespace YogaHybridApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new AllClassPage());
            MainPage = new NavigationPage(new SignInPage());


        }


    }
}