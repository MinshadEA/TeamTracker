using System;
using CommunityToolkit.Mvvm.ComponentModel;
using static Microsoft.Maui.Controls.Internals.Profile;

namespace TeamTracker.Models
{
	public class UsersListModel
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Users> data { get; set; }
        public Support support { get; set; }
    }
    public class Support
    {
        public string url { get; set; }
        public string text { get; set; }
    }
}

