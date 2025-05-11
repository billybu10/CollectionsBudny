namespace CollectionsBudny.Pages;

public partial class CollectionPage : ContentPage
{
	public CollectionPage()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }
}