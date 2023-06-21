using MediaNetwork.Business;

namespace MediaNetwork;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainPage();
	}
    protected async override void OnResume()
    {
        //Pristupiti objetku korisnika
        var user = await SecureStorage.Default.GetUser();

        if (user == null)
        {
            return;
        }

        if (user.ShouldBeLoggedOut)
        {
            await Logout();
        }
        base.OnResume();
    }

    protected async override void OnStart()
    {
        var user = await SecureStorage.Default.GetUser();

        if (user == null)
        {

            MainPage = new MainPage();
            return;
        }

        if (user.ShouldBeLoggedOut)
        {
            await Logout();
        }
        else
        {
            //MainPage = new HomePage();
            MainPage = new MainPage();
        }

        base.OnStart();
    }

    private async Task Logout()
    {
        this.MainPage = new MainPage();
        await SecureStorage.Default.SetAsync("user", "");
    }
}
