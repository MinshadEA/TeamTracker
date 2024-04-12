using TeamTracker.ViewModels;

namespace TeamTracker.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
	{
		InitializeComponent();
		this.BindingContext = new LoginViewModel();
    }
}
