using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.ViewsModels;

public partial class TaxesManagementView : ContentPage
{
	public TaxesManagementView()
	{
		InitializeComponent();
		BindingContext = new TaxesManagementViewModel();
	}
	
	private void OkClicked(object sender, EventArgs e)
	{
        (BindingContext as TaxesManagementViewModel)?.SaveTax();
        Shell.Current.GoToAsync("//MainPage");
    }

	private void GoBackClicked(object sender, EventArgs e)
	{
        (BindingContext as TaxesManagementViewModel)?.Undo();
        Shell.Current.GoToAsync("//MainPage");
    }
    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as TaxesManagementViewModel).RefreshUX();
    }
}