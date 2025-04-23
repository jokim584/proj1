using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.ViewsModels;

public partial class CheckOutView : ContentPage
{
	public CheckOutView()
	{
		InitializeComponent();
        BindingContext = new CheckOutViewModel();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CheckOutViewModel)?.RefreshProductList();
    }
}