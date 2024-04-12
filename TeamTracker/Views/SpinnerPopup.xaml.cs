using CommunityToolkit.Maui.Views;

namespace TeamTracker.Views;

public partial class SpinnerPopup : Popup
{
	public SpinnerPopup(object viemodel)
	{
		InitializeComponent();
		this.BindingContext = viemodel;
	}
}
