using CrossingKitty.DataAccess;

namespace CrossingKitty;

public partial class LoginPage : ContentPage
{
    private readonly IUserRepository _repository;
    public LoginPage(IUserRepository repository)
    {
        _repository = repository;
        InitializeComponent();
    }

    public async void OnLoginBtnClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var pass = PasswordEntry.Text;

        if (username == null || pass == null)
        {
            await DisplayAlert("Error", "All fields are required!", "Ok");
        }

        var user = await _repository.Login(username, pass);
        if (user != null)
        {
            await SecureStorage.SetAsync("username", user.Username);
            await DisplayAlert("Success", "You have been logged", "Ok");

            await Navigation.PushAsync(new MainPage());
        }
    }
}