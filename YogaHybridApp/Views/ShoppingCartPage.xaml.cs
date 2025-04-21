using System.Windows.Input;
namespace YogaHybridApp.Views.Class;

using YogaHybridApp.Database;
using YogaHybridApp.Objects;


public partial class ShoppingCartPage : ContentPage
{
    private readonly DatabaseConnect _database;
    public ClassInstanceViewModel ClassInstance { get; set; }
    public Course Course { get; set; }
    public User User { get; set; }
    public ICommand CancelCommand { get; }
    public ICommand AcceptCommand { get; }

    public ShoppingCartPage(ClassInstanceViewModel classInstance, Course course, User user)
    {
        InitializeComponent();
        _database = new DatabaseConnect();
        ClassInstance = classInstance;
        Course = course;
        User = user;
        CancelCommand = new Command(async () => await Cancel());
        AcceptCommand = new Command(async () => await Accept());
        BindingContext = this;
    }

    private async Task Cancel()
    {
        await Navigation.PushAsync(new AllClassPage(Course, _database));
    }

    private async Task Accept()
    {
        try
        {
            var dbUser = await _database.GetUserByIdAsync(User.UserId);
            if (dbUser == null)
            {
                await DisplayAlert("Error", "User not found in database.", "OK");
                return;
            }

            if (dbUser.AddClass(ClassInstance.ClassInstanceId))
            {
                await _database.UpdateUserClassesAsync(dbUser.UserId, dbUser.ClassesId);
                await DisplayAlert("Success", "Payment Success!", "OK");
                await Navigation.PushAsync(new ClassOrderedPage());
            }
            else
            {
                await DisplayAlert("Error", "Class already ordered or invalid.", "OK");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in Accept: {ex}");
            await DisplayAlert("Error", $"Failed to process order: {ex.Message}", "OK");
        }
    }
}