namespace YogaHybridApp.Views.Courses;

using System.Collections.ObjectModel;
using System.Windows.Input;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;
using YogaHybridApp.Views.Course;

public partial class AllCoursePage : ContentPage
{
    private readonly DatabaseConnect _databaseConnect;
    public ObservableCollection<Course> Courses { get; set; }

    public ICommand NavigateToDetailCommand { get; }

    public AllCoursePage()
    {
        InitializeComponent();
        _databaseConnect = new DatabaseConnect();
        Courses = new ObservableCollection<Course>();
        NavigateToDetailCommand = new Command<Course>(async (course) => await NavigateToDetail(course));
        BindingContext = this;
        LoadCourses();
    }

    private async void LoadCourses()
    {
        var courses = await _databaseConnect.GetAllCoursesAsync();
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            Courses.Clear();
            foreach (var course in courses)
            {
                Courses.Add(course);
            }
        });
    }

    private async Task NavigateToDetail(Course course)
    {
        await Navigation.PushAsync(new CourseDetailPage(course));
    }
}