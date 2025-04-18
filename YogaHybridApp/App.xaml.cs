using YogaHybridApp.Views;

namespace YogaHybridApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new AllClassPage());

        }


    }
}