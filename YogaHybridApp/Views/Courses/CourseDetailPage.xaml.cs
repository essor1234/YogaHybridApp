namespace YogaHybridApp.Views.Course; // Note: Fixed namespace from Course to Courses

using System.Windows.Input;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;
using YogaHybridApp.Views.Class;


public partial class CourseDetailPage : ContentPage
{
    public Course Course { get; set; }
    public ICommand NavigateToAllClassesCommand { get; }
    private readonly DatabaseConnect _database; // Change to private readonly

    public CourseDetailPage(Course course)
    {
        InitializeComponent();
        Course = course;
        _database = new DatabaseConnect(); // Initialize DatabaseConnect
        NavigateToAllClassesCommand = new Command(async () => await NavigateToAllClasses(Course));
        BindingContext = this;
    }

    private async Task NavigateToAllClasses(Course course)
    {
        await Navigation.PushAsync(new AllClassPage(course, _database));
    }
}