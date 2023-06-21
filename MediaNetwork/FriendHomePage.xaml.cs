using MediaNetwork.Business.Dto;
using Newtonsoft.Json;

namespace MediaNetwork;

public partial class FriendHomePage : TabbedPage
{
	public FriendHomePage(UserDto user)
	{
		InitializeComponent();
	}
}