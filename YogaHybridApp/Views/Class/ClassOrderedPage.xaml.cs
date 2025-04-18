using Microsoft.Maui.Storage; // Add
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;
using YogaHybridApp.Views.Account;

namespace YogaHybridApp.Views.Class;

public partial class ClassOrderedPage : ContentPage
{
    private readonly DatabaseConnect _database;
    public ObservableCollection<ClassInstanceViewModel> OrderedClasses { get; set; }

    public ClassOrderedPage()
    {
        InitializeComponent();
        _database = new DatabaseConnect();
        OrderedClasses = new ObservableCollection<ClassInstanceViewModel>();
        BindingContext = this;
        LoadOrderedClasses();
    }

    private async void LoadOrderedClasses()
    {
        try
        {
            if (!Preferences.ContainsKey("CurrentUserId")) // Use Preferences
            {
                await DisplayAlert("Error", "Please sign in to view ordered classes.", "OK");
                await Navigation.PushAsync(new SignInPage());
                return;
            }

            string userId = Preferences.Get("CurrentUserId", null);
            var user = await _database.GetUserByIdAsync(userId);
            if (user == null)
            {
                await DisplayAlert("Error", "User not found. Please sign in again.", "OK");
                await Navigation.PushAsync(new SignInPage());
                return;
            }

            if (user.ClassesId == null || !user.ClassesId.Any())
            {
                await DisplayAlert("Info", "You have no ordered classes.", "OK");
                return;
            }

            var classInstances = await _database.GetClassInstancesByIdsAsync(user.ClassesId);
            OrderedClasses.Clear();

            foreach (var classInstance in classInstances)
            {
                var teacher = await _database.GetTeacherByIdAsync(classInstance.TeacherId);
                var course = await _database.GetCourseByIdAsync(classInstance.CourseId);
                var viewModel = new ClassInstanceViewModel
                {
                    ClassInstanceId = classInstance.ClassInstanceId,
                    CourseId = classInstance.CourseId,
                    Date = classInstance.DateTime,
                    TeacherName = teacher?.Name ?? "Unknown Teacher",
                    Comment = classInstance.Comment,
                    Teacher = teacher,
                    Course = course
                };
                OrderedClasses.Add(viewModel);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading ordered classes: {ex}");
            await DisplayAlert("Error", $"Failed to load ordered classes: {ex.Message}", "OK");
        }
    }
}