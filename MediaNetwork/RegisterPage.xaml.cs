using MediaNetwork.Business;
using MediaNetwork.Business.Dto;

namespace MediaNetwork;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private async void LogInBtn_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new MainPage();
    }

    private async void RegisterBtn_Clicked(object sender, EventArgs e)
    {
        RegisterService service = new RegisterService();
        var dto = new RegisterDto
        {
            FirstName = this.FirstName.Text,
            LastName = this.LastName.Text,
            Email = this.Email.Text,
            Password = this.Password.Text,
            DateOfBirth = this.DateOfBirth.Date,
            RoleId = 1
        };
        if (service.Register(dto))
        {
            await Navigation.PushAsync(new UserProfilePage());
        }

    }
}