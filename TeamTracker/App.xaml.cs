using System.Globalization;
using TeamTracker.LanguageResources;
using TeamTracker.Views;

namespace TeamTracker;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
        MainPage = new LoginPage();

    }
}

