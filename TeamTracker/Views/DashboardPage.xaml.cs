using TeamTracker.ViewModels;

namespace TeamTracker.Views;

public partial class DashboardPage : ContentPage
{
    DashboardViewModel _dashboardViewModel;
    public DashboardPage()
	{
		InitializeComponent();
        _dashboardViewModel = new DashboardViewModel();
        this.BindingContext = _dashboardViewModel;
	}
    void SearchBar_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            BindableLayout.SetItemsSource(UsersListBindableView, _dashboardViewModel.UsersList.data);
        }
        else
        {
            var lowerKeyword = e.NewTextValue.ToLower();
            BindableLayout.SetItemsSource(UsersListBindableView, _dashboardViewModel.UsersList.data.Where(x => x.First_name != null && x.First_name.ToLower().Contains(lowerKeyword)
            || x.Last_name != null && x.Last_name.ToLower().Contains(lowerKeyword)
            || x.Email != null && x.Email.ToLower().Contains(lowerKeyword)).ToList());
        }
    }
}
