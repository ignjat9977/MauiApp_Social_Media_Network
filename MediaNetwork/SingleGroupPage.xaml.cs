using MediaNetwork.Business.Dto;

namespace MediaNetwork;

public partial class SingleGroupPage : ContentPage
{
	public SingleGroupPage(GroupDto dto)
	{
		InitializeComponent();

		Name.Text = dto.Name;
		Description.Text = dto.Description;
		Id.Text = dto.Id.ToString();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new HomePage();
    }
}