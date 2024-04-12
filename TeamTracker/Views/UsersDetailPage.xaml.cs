using TeamTracker.ViewModels;

namespace TeamTracker.Views;

public partial class UsersDetailPage : ContentPage
{
	public UsersDetailPage()
	{
		InitializeComponent();
		this.BindingContext = new UsersDetailViewModel();

    }
}
