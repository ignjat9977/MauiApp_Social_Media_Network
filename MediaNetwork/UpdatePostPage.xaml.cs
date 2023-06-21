using MediaNetwork.Business.Dto;

namespace MediaNetwork;

public partial class UpdatePostPage : ContentPage
{
	public UpdatePostPage(PostDto dto)
	{

        InitializeComponent();

        Title.Text = dto.Title;
        Content.Text = dto.Content;
        Id.Text = $"{dto.Id}";
    }
}