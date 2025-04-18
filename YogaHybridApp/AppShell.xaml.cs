using YogaHybridApp.Views;
using YogaHybridApp.Views.Account;
using YogaHybridApp.Views.Class;
using YogaHybridApp.Views.Courses;


namespace YogaHybridApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

			// Define the Routes
			Routing.RegisterRoute(nameof(AllClassPage), typeof(AllClassPage));
			Routing.RegisterRoute(nameof(ClassDetailsPage), typeof(ClassDetailsPage));
			Routing.RegisterRoute(nameof(ClassOrderedPage), typeof(ClassOrderedPage));
			Routing.RegisterRoute(nameof(ShoppingCartPage), typeof(ShoppingCartPage));
			Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
			Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
		}


    }
}
