namespace YogaHybridApp.Views.Class;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;


public partial class AllClassPage : ContentPage
{
    private readonly DatabaseConnect _database;
    private readonly Course _course;
    public ObservableCollection<ClassInstanceViewModel> ClassInstances { get; set; }
    public ICommand NavigateToClassDetailsCommand { get; }

    public AllClassPage(Course course, DatabaseConnect database)
    {
        InitializeComponent();
        _database = database;
        _course = course;
        ClassInstances = new ObservableCollection<ClassInstanceViewModel>();
        NavigateToClassDetailsCommand = new Command<ClassInstanceViewModel>(async (vm) => await NavigateToClassDetails(vm));
        BindingContext = this;
        LoadClassInstances();
    }

    private async void LoadClassInstances()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"Loading classes for CourseId: {_course?.CourseId}");
            if (_course == null)
            {
                System.Diagnostics.Debug.WriteLine("Course is null");
                await DisplayAlert("Info", "No course selected.", "OK");
                return;
            }

            if (_database == null)
            {
                System.Diagnostics.Debug.WriteLine("Database is null");
                await DisplayAlert("Error", "Database connection not initialized.", "OK");
                return;
            }

            var classInstances = await _database.GetClassInstancesByCourseIdAsync(_course.CourseId);
            System.Diagnostics.Debug.WriteLine($"Fetched {classInstances?.Count ?? 0} class instances for CourseId: {_course.CourseId}");
            ClassInstances.Clear();
            if (classInstances == null || !classInstances.Any())
            {
                System.Diagnostics.Debug.WriteLine("No class instances found");
                await DisplayAlert("Info", "No classes found for this course.", "OK");
                return;
            }

            foreach (var classInstance in classInstances)
            {
                var teacher = await _database.GetTeacherByIdAsync(classInstance.TeacherId);
                System.Diagnostics.Debug.WriteLine($"Adding class instance: {classInstance.ClassInstanceId}, Teacher: {teacher?.Name}");
                var viewModel = new ClassInstanceViewModel
                {
                    ClassInstanceId = classInstance.ClassInstanceId,
                    CourseId = classInstance.CourseId,
                    Date = classInstance.DateTime,
                    TeacherName = teacher?.Name ?? "Unknown Teacher",
                    Comment = classInstance.Comment,
                    Teacher = teacher,
                    Course = _course
                };
                ClassInstances.Add(viewModel);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading classes: {ex}");
            await DisplayAlert("Error", $"Failed to load classes: {ex.Message}", "OK");
        }
    }

    private async Task NavigateToClassDetails(ClassInstanceViewModel viewModel)
    {
        await Navigation.PushAsync(new ClassDetailsPage(viewModel));
    }
}


