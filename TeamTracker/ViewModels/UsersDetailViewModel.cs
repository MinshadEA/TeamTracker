using System;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Java.Lang;
using Newtonsoft.Json;
using TeamTracker.Models;
using TeamTracker.Views;

namespace TeamTracker.ViewModels
{
    public partial class UsersDetailViewModel:BaseViewModel, IQueryAttributable
    {
        #region Properties
        [ObservableProperty]
        private string selectedUserId;
        [ObservableProperty]
        private UserDataModel selectedUserData;
        [ObservableProperty]
        private bool isEditable = false;
        [ObservableProperty]
        private bool isEditIconVisible = false;
        #endregion

        public UsersDetailViewModel()
        {
            IsLoadingText = "Loading...";
            IsEditIconVisible = false;
            LoadUserDetail("2");
        }
        
        #region Methods
        [RelayCommand]
        private async void Update()
        {
            var Userdetail = JsonConvert.SerializeObject(SelectedUserData.data);
            await Shell.Current.GoToAsync($"..?userdetail={Userdetail}");
        }
        [RelayCommand]
        private async void EnableEdit()
        {
            IsEditable = true;
            IsEditIconVisible = false;
        }
        [RelayCommand]
        private async void EditProfileImage()
        {
            bool answer =await Application.Current.MainPage.DisplayAlert("Update Profile Photo", "Select camera or gallery option to update profile photo", "Take Photo", "Pick Photo");
            if(answer)
            {
                OpenCamera();
            }
            else
            {
                OpenGallery();
            }
        }
        private async void OpenCamera()
        {
            try
            {
                await Permissions.RequestAsync<Permissions.Camera>();
                await Permissions.RequestAsync<Permissions.StorageWrite>();
                var Photostatus = await Permissions.CheckStatusAsync<Permissions.Camera>();
                var StorageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                if (Device.RuntimePlatform == Device.Android && int.Parse(DeviceInfo.VersionString) >= 13)
                {
                    StorageStatus = PermissionStatus.Granted;
                }
                if (StorageStatus == PermissionStatus.Granted && Photostatus == PermissionStatus.Granted)
                {
                    if (MediaPicker.Default.IsCaptureSupported)
                    {
                        FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                        if (photo != null)
                        {
                            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                            using Stream sourceStream = await photo.OpenReadAsync();
                            using FileStream localFileStream = File.OpenWrite(localFilePath);
                            await sourceStream.CopyToAsync(localFileStream);
                            SelectedUserData.data.Avatar = localFilePath;
                        }
                    }
                }
                else
                {
                    AppInfo.ShowSettingsUI();
                }

            }
            catch (System.Exception ex)
            {

            }
        }
        private async void OpenGallery()
        {
            try
            {
                await Permissions.RequestAsync<Permissions.StorageWrite>();
                var StorageStatus = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                if (Device.RuntimePlatform == Device.Android && int.Parse(DeviceInfo.VersionString) >= 13)
                {
                    StorageStatus = PermissionStatus.Granted;
                }
                if (StorageStatus == PermissionStatus.Granted)
                {
                    if (MediaPicker.Default.IsCaptureSupported)
                    {
                        FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                        if (photo != null)
                        {
                            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                            using Stream sourceStream = await photo.OpenReadAsync();
                            using FileStream localFileStream = File.OpenWrite(localFilePath);
                            await sourceStream.CopyToAsync(localFileStream);
                            SelectedUserData.data.Avatar = localFilePath;
                        }
                    }
                }
                else
                {
                    AppInfo.ShowSettingsUI();
                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query!=null&&query.Count>0)
            {
                IsEditIconVisible = true;
                SelectedUserId = query["SelectedUserId"] as string;
                await LoadUserDetail(SelectedUserId);
                IsLoadingText = "Updating";
            }
        }
        private async Task LoadUserDetail(string id)
        {
            var popup = new SpinnerPopup(this);
            Application.Current.MainPage.ShowPopup(popup);
            SelectedUserData = await GetUserDetail(id);
            popup.Close();
        }
        #endregion
       
        
    }
}

