using MediaNetwork.Business;
using Newtonsoft.Json;
using MediaNetwork;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;

namespace MediaNetwork;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
        
		InitializeComponent();
	}

    private async void RegisterBtn_Clicked(object sender, EventArgs e)
    {

        Application.Current.MainPage = new RegisterPage();
    }

    private async void LogInBtn_Clicked(object sender, EventArgs e)
    {
        AuthService auth = new AuthService();
        User loginUser = auth.Auth(this.Email.Text, this.Password.Text);

        if(loginUser != null)
        {
            await SecureStorage.Default.SetAsync("jwt", loginUser.Token);
            await SecureStorage.Default.SetAsync("user", JsonConvert.SerializeObject(loginUser));

            Application.Current.MainPage = new HomePage();
        }
        else
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Green,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(10),
                Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
                CharacterSpacing = 0
            };

            string text = "Wrong Email Or Password!";
            string actionButtonText = "Click Here to Dismiss";
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = Snackbar.Make(text, null, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }
        
    }

    //private void OnCounterClicked(object sender, EventArgs e)
    //{
       
    //    count++;

    //    if (count == 1)
    //        CounterBtn.Text = $"Clicked {count} time";
    //    else
    //        CounterBtn.Text = $"Clicked {count} times";

    //    SemanticScreenReader.Announce(CounterBtn.Text);
    //}
}

