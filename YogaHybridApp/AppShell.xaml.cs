using YogaHybridApp.Views;
using YogaHybridApp.Views.Account;
using YogaHybridApp.Views.Class;


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
			Routing.RegisterRoute(nameof(ShopingCartPage), typeof(ShopingCartPage));
			Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
			Routing.RegisterRoute(nameof(SignUpPage), typeof(SignUpPage));
		}
    }
}
