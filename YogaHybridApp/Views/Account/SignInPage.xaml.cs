using YogaHybridApp.Database;

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

        // Basic validation
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

        // Verify user in Firebase
        var user = await _databaseConnect.GetUserByEmailAsync(email);
        if (user == null)
        {
            await DisplayAlert("Error", "No account found with this email.", "OK");
            return;
        }

        //// Store user data if needed (e.g., for app-wide access)
        //Application.Current.Properties["CurrentUserId"] = user.UserId;
        //Application.Current.Properties["CurrentUserName"] = user.Name;

        await DisplayAlert("Success", $"Welcome back, {user.Name}!", "OK");

        // Navigate to the main app page (replace with your actual main page)
        await Navigation.PushAsync(new AllClassPage());
    }

    private async void OnSignUpTapped(object sender, EventArgs e)
    {
        // Navigate to Sign-Up page
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