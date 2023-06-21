
namespace MediaNetwork.Business.Components;

public partial class Friend : ContentView
{
    public static readonly BindableProperty FirstNameProperty =
    BindableProperty.Create(nameof(FirstName), typeof(string), typeof(Friend), "");
    public string FirstName
    {
        get => (string)GetValue(FirstNameProperty);
        set => SetValue(FirstNameProperty, value);
    }
    public static readonly BindableProperty LastNameProperty =
    BindableProperty.Create(nameof(LastName), typeof(string), typeof(Friend), "");
    public string LastName
    {
        get => (string)GetValue(LastNameProperty);
        set => SetValue(LastNameProperty, value);
    }
    public static readonly BindableProperty ImageUrlProperty =
    BindableProperty.Create(nameof(ImageUrl), typeof(string), typeof(Friend), "");
    public string ImageUrl
    {
        get => (string)GetValue(ImageUrlProperty);
        set => SetValue(ImageUrlProperty, value);
    }
    public Friend()
	{
		InitializeComponent();
	}
}