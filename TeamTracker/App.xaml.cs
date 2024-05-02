using System.Globalization;
using TeamTracker.LanguageResources;
using TeamTracker.Views;

namespace TeamTracker;

public partial class App : Application
{
    public static IConnectivity NetConnectivity { get; private set; }
	public App(IConnectivity connectivity)
	{
		InitializeComponent();
        NetConnectivity = connectivity;
        //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar");
        MainPage = new LoginPage();

    }
}

