
using YogaHybridApp.Database;

namespace YogaHybridApp.Views.Account;

public partial class SignUpPage : ContentPage
{
	private DatabaseConnect _databaseConnect;
	public SignUpPage()
	{
        InitializeComponent();
		_databaseConnect = new DatabaseConnect();

	}
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text?.Trim();
        string email = EmailEntry.Text?.Trim();

        // Basic validation
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email))
        {
            await DisplayAlert("Error", "Please enter both name and email.", "OK");
            return;
        }

        if (!IsValidEmail(email))
        {
            await DisplayAlert("Error", "Please enter a valid email address.", "OK");
            return;
        }

        // Create user in Firebase
        bool success = await _databaseConnect.CreateUserAsync(name, email);
        if (!success)
        {
            await DisplayAlert("Error", "Email already exists or an error occurred.", "OK");
            return;
        }

        await DisplayAlert("Success", $"Account created for {name}!", "OK");
        await Navigation.PushAsync(new SignInPage());
    }

    private async void OnSignInTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignInPage());
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