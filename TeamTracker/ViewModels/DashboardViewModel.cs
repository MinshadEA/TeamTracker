using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using TeamTracker.Models;
using TeamTracker.Views;
using static Java.Util.Jar.Attributes;

namespace TeamTracker.ViewModels
{
    [QueryProperty(nameof(SelectedUserDetail), "userdetail")]
    public partial class DashboardViewModel:BaseViewModel
    {
        #region Properties
        [ObservableProperty]
        private UsersListModel usersList = new UsersListModel();

        [ObservableProperty]
        private Users _selectedUserData;

        [ObservableProperty]
        private bool isRefreash;

        [ObservableProperty]
        private IConnectivity _connectivity;

        private string selectedUserDetail;
        public string SelectedUserDetail
        {
            get { return selectedUserDetail; }
            set
            {
                selectedUserDetail = Uri.UnescapeDataString(value);
                OnPropertyChanged("SelectedUserDetail");
                SelectedUserData = JsonConvert.DeserializeObject<Users>(selectedUserDetail);
                UpdateList();
            }
        }
        #endregion

        public DashboardViewModel(IConnectivity connectivity)
		{
            Connectivity = connectivity;
            LoadUsers();
		}

        #region Methods
        [RelayCommand]
        private async void NavigateToDetailPage(Users users)
        {
            Shell.Current.GoToAsync($"UsersDetailPage?SelectedUserId={users.id}");
        }
        [RelayCommand]
        private async void Refreash()
        {
            IsRefreash = true;
            await LoadUsers();
            IsRefreash = false;
        }
        private async Task LoadUsers()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                IsLoadingText = "Loading Users...";
                var popup = new SpinnerPopup(this);
                if (!isRefreash)
                {
                    Application.Current.MainPage.ShowPopup(popup);
                }
                UsersList = await GetUsersList();
                if (!isRefreash)
                {
                    popup.Close();
                }
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("No Internet", "No internet found. please try again!", "ok");
            }
        }

        public void UpdateList()
        {
            try
            {
                foreach (Users users in UsersList.data.ToList())
                {
                    if (users.id.Equals(SelectedUserData.id))
                    {
                        users.Avatar = "";
                        users.Avatar = SelectedUserData.Avatar;
                        users.First_name = SelectedUserData.First_name;
                        users.Last_name = SelectedUserData.Last_name;
                        users.Email = SelectedUserData.Email;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}

