using System;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TeamTracker.Views;

namespace TeamTracker.ViewModels
{
	public partial class LoginViewModel:BaseViewModel
	{
        #region Properties
        [ObservableProperty]
        private string userName= "eve.holt@reqres.in";

        [ObservableProperty]
        private string password="";

        [ObservableProperty]
        private bool _isTermsAndCondetionChecked=false;

        [ObservableProperty]
        private bool _isEmailValid = false;

        [ObservableProperty]
        private IConnectivity _connectivity;
        #endregion

        public LoginViewModel(IConnectivity connectivity)
		{
            Connectivity = connectivity;
        }

        #region Methods
        [RelayCommand]
        private async void Validate()
        {
            if(Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
                IsLoadingText = "Loging...";
                var popup = new SpinnerPopup(this);
                Application.Current.MainPage.ShowPopup(popup);
                var LoginResponse = await Login(UserName, Password);
                popup.Close();
                if (string.IsNullOrWhiteSpace(LoginResponse?.token))
                {
                    Application.Current.MainPage.DisplayAlert("Login Failed", LoginResponse.error, "ok");
                }
                else
                {
                    Application.Current.MainPage = new AppShell();
                }
            }
            else
            {
                 Application.Current.MainPage.DisplayAlert("No Internet", "No internet found. please try again!", "ok");
            }
            
        }
        #endregion
    }
}

