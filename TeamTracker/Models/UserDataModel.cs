using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TeamTracker.Models
{
    public partial class Users : ObservableObject
    {
        public int id { get; set; }
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string first_name;
        [ObservableProperty]
        private string last_name;
        [ObservableProperty]
        private string avatar;
    }
    public class UserDataModel
	{
        public Users data { get; set; }
        public Support support { get; set; }
    }
}

