using CrossingKitty.DataAccess;
using CrossingKitty.Models;

namespace CrossingKitty;

public partial class RegistrationPage : ContentPage
{
    private readonly IUserRepository _repository;
    public RegistrationPage(IUserRepository repository)
	{
		InitializeComponent();
        _repository = repository;
    }
    public async void OnRegisterBtnClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text;
        var pass = PasswordEntry.Text;

        if (username == null || pass == null)
        {
            await DisplayAlert("Error", "All fields are required!", "Ok");
        }

        await _repository.Register(username, pass);
        await DisplayAlert("Registration succesfull!","You heve been registered succesfully!","Ok");

    }
}