namespace YogaHybridApp.Views.Course;

using System.Windows.Input;
using YogaHybridApp.Objects;
using YogaHybridApp.Views.Class;

public partial class CourseDetailPage : ContentPage
{
    public Course Course { get; set; }
    public ICommand NavigateToAllClassesCommand { get; }


    public CourseDetailPage(Course course)
    {
        InitializeComponent();
        Course = course;
        NavigateToAllClassesCommand = new Command(async () => await NavigateToAllClasses(Course));    
        BindingContext = this;
    }

    private async Task NavigateToAllClasses(Course course)
    {
        await Navigation.PushAsync(new AllClassPage(course));
    }
}