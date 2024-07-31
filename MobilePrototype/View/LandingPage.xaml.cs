using MobilePrototype.ViewModel;


namespace MobilePrototype.View;


public partial class LandingPage : ContentPage
{
	public LandingPage()
	{
        InitializeComponent();
		this.BindingContext = new LandingViewModel();
	}
}