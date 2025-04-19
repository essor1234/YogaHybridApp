using Microsoft.Maui.Storage;
using System.Windows.Input;
using YogaHybridApp.Views.Class;
using YogaHybridApp.Views.Courses;

namespace YogaHybridApp;

public partial class MainPage : ContentPage
{
    public string WelcomeMessage { get; set; }
    public ICommand NavigateToCoursesCommand { get; }
    public ICommand NavigateToOrderedClassesCommand { get; }
    public ICommand NavigateToSearchClassesCommand { get; }


    public MainPage()
    {
        InitializeComponent();

        // Fetch username from Preferences (set during login, defaults to "Guest")
        string username = Preferences.Get("CurrentUserName", "Guest");
        WelcomeMessage = $"Welcome, {username}";

        // Commands for navigation
        NavigateToCoursesCommand = new Command(async () => await Navigation.PushAsync(new AllCoursePage()));
        NavigateToOrderedClassesCommand = new Command(async () => await Navigation.PushAsync(new ClassOrderedPage()));
        NavigateToSearchClassesCommand = new Command(async () => await Navigation.PushAsync(new SearchClassPage()));

        // Set this page as the binding context
        BindingContext = this;
    }
}