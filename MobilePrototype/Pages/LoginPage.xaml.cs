namespace MobilePrototype.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ErrorMessage.Text = "Please enter both username and password.";
            ErrorMessage.IsVisible = true;
        }
        else
        {
            ErrorMessage.IsVisible = false;
            // Here you would typically validate the username and password with your backend service
            if (ValidateCredentials(username, password))
            {
                Shell.Current.GoToAsync("MainPage");
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password.";
                ErrorMessage.IsVisible = true;
            }
        }
    }

    private bool ValidateCredentials(string username, string password)
    {
        // Add your actual validation logic here
        return username == "test" && password == "password";
    }
}