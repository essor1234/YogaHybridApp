namespace YogaHybridApp.Views.Class;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Firebase.Database;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;

public partial class AllClassPage : ContentPage
{
    private readonly DatabaseConnect _database;
    private readonly Course _course;
    public ObservableCollection<ClassInstanceViewModel> ClassInstances { get; set; }

    public AllClassPage(Course course)
    {
        InitializeComponent();
        _database = new DatabaseConnect();
        _course = course;
        ClassInstances = new ObservableCollection<ClassInstanceViewModel>();
        BindingContext = this;
        LoadClassInstances();
    }

    private async void LoadClassInstances()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine($"Loading classes for CourseId: {_course.CourseId}");
            var classInstances = await _database.GetClassInstancesByCourseIdAsync(_course.CourseId);
            ClassInstances.Clear();
            if (classInstances == null || !classInstances.Any())
            {
                await DisplayAlert("Info", "No classes found for this course.", "OK");
                return;
            }

            foreach (var classInstance in classInstances)
            {
                var teacher = await _database.GetTeacherByIdAsync(classInstance.TeacherId);
                var viewModel = new ClassInstanceViewModel
                {
                    ClassInstanceId = classInstance.ClassInstanceId,
                    CourseId = classInstance.CourseId,
                    Date = classInstance.DateTime,
                    TeacherName = teacher?.Name ?? "Unknown Teacher",
                    Comment = classInstance.Comment
                };
                System.Diagnostics.Debug.WriteLine($"ClassInstance: ID={viewModel.ClassInstanceId}, Date={viewModel.Date:MM/dd/yyyy HH:mm}, Teacher={viewModel.TeacherName}");
                ClassInstances.Add(viewModel);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading classes: {ex}");
            await DisplayAlert("Error", $"Failed to load classes: {ex.Message}", "OK");
        }
    }
}

public class ClassInstanceViewModel
{
    public string ClassInstanceId { get; set; }
    public string CourseId { get; set; }
    public DateTime Date { get; set; }
    public string TeacherName { get; set; }
    public string Comment { get; set; }
}