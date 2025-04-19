using Microsoft.Maui.Storage; // Add
using YogaHybridApp.Database;
using YogaHybridApp.Views.Class;
using YogaHybridApp.Views.Courses;

namespace YogaHybridApp.Views.Account;

public partial class SignInPage : ContentPage
{
    public DatabaseConnect _databaseConnect;

    public SignInPage()
    {
        InitializeComponent();
        _databaseConnect = new DatabaseConnect();
    }

    private async void OnSignInClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim();

        if (string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Please enter your email.", "OK");
            return;
        }

        if (!IsValidEmail(email))
        {
            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
            return;
        }

        var user = await _databaseConnect.GetUserByEmailAsync(email);
        if (user == null)
        {
            await DisplayAlert("Error", "No account found with this email.", "OK");
            return;
        }

        Preferences.Set("CurrentUserId", user.UserId); // Use Preferences
        Preferences.Set("CurrentUserName", user.Name);

        await DisplayAlert("Success", $"Welcome back, {user.Name}!", "OK");
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnSignUpTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}