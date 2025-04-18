namespace YogaHybridApp.Views.Course;

using System.Windows.Input;
using YogaHybridApp.Objects;

public partial class CourseDetailPage : ContentPage
{
    public Course Course { get; set; }
    public ICommand NavigateToAllClassesCommand { get; }


    public CourseDetailPage(Course course)
    {
        InitializeComponent();
        Course = course;
        NavigateToAllClassesCommand = new Command(async () => await NavigateToAllClasses());    
        BindingContext = this;
    }

    private async Task NavigateToAllClasses()
    {
        await Navigation.PushAsync(new AllClassPage());
    }
}