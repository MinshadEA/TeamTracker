using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TeamTracker.Helper;
using TeamTracker.Models;

namespace TeamTracker.ViewModels
{
	public partial class BaseViewModel : ObservableObject
    {
        #region Properties
        [ObservableProperty]
        private bool _isLoading;
        [ObservableProperty]
        private string _isLoadingText;
        #endregion


        public BaseViewModel()
		{
		}

        /// <summary>
        /// Team Tracker Login
        /// </summary>
        /// <returns></returns>
        public async Task<LoginResponseModel> Login(string username,string password)
        {
            LoginResponseModel LoginResponse = new LoginResponseModel();
            await Task.Run(() => {
                string postdataForLogin = "{\"username\":\"" + username + "\",\"password\":\"" + password + "\"}";
                HttpHelper LoginAPICall = new HttpHelper(URL.TeamTrackerLoginAPI, postdataForLogin, APIMethodConstants.POST);
                LoginResponse = LoginAPICall.APICallResult<LoginResponseModel>();
            });
            return LoginResponse;
        }

        /// <summary>
        /// Users List
        /// </summary>
        /// <returns></returns>
        public async Task<UsersListModel> GetUsersList()
        {
            UsersListModel UsersListResponse = new UsersListModel();
            await Task.Run(() => {
                HttpHelper usersListAPICall = new HttpHelper(URL.UsersListAPI, "", APIMethodConstants.GET);
                UsersListResponse = usersListAPICall.APICallResult<UsersListModel>();
            });
            return UsersListResponse;
        }

        /// <summary>
        /// User Detail
        /// </summary>
        /// <returns></returns>
        public async Task<UserDataModel> GetUserDetail(string userId)
        {
            UserDataModel UserDetailResponse = new UserDataModel();
            await Task.Run(() => {
                HttpHelper userDetailAPICall = new HttpHelper(URL.UsersListDetailAPI+ userId, "", APIMethodConstants.GET);
                UserDetailResponse = userDetailAPICall.APICallResult<UserDataModel>();
            });
            return UserDetailResponse;
        }
    }
 
}

