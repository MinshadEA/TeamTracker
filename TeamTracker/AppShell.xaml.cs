using TeamTracker.Views;

namespace TeamTracker;

public partial class AppShell : Shell
{
    private string version;
    public string Version
    {
        get { return version; }
        set
        {
            version = value;
            OnPropertyChanged("Version");
        }
    }
    public AppShell()
	{
		InitializeComponent();
        Version="Version "+ AppInfo.Current.VersionString;
        this.BindingContext = this;
	}

    async void MenuItem_Clicked(System.Object sender, System.EventArgs e)
    {
        bool answer=await DisplayAlert("Logout", "Are you sure,you want to logout", "Logout", "Cancel");
        if(answer)
        {
            Preferences.Clear();
            Application.Current.MainPage = new LoginPage();
        }
    }
}

