using Microsoft.Maui.Storage; // Add
using System.Windows.Input;
using YogaHybridApp.Database;
using YogaHybridApp.Objects;
using YogaHybridApp.Views.Account;

namespace YogaHybridApp.Views.Class;

public partial class ClassDetailsPage : ContentPage
{
    private readonly DatabaseConnect _database;
    public ClassInstanceViewModel ClassInstance { get; set; }
    public ICommand AddToCartCommand { get; }

    public ClassDetailsPage(ClassInstanceViewModel classInstance)
    {
        InitializeComponent();
        _database = new DatabaseConnect();
        ClassInstance = classInstance;
        AddToCartCommand = new Command(async () => await AddToCartAndNavigate());
        BindingContext = this;
    }

    private async Task AddToCartAndNavigate()
    {
        try
        {
            if (!Preferences.ContainsKey("CurrentUserId")) // Use Preferences
            {
                await DisplayAlert("Error", "Please sign in to add a class.", "OK");
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

            await Navigation.PushAsync(new ShoppingCartPage(ClassInstance, ClassInstance.Course, user));
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error in AddToCartAndNavigate: {ex}");
            await DisplayAlert("Error", $"Failed to add to cart: {ex.Message}", "OK");
        }
    }
}