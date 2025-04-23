using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.ViewsModels;

public partial class ShoppingManagementView : ContentPage
{
	public ShoppingManagementView()
	{
		InitializeComponent();
		BindingContext = new ShoppingManagementViewModel();
	}
	private void AddToCartClicked(object sender, EventArgs e)
	{
		(BindingContext as ShoppingManagementViewModel).PurchaseItem();
	}
	private void RemoveFromCartClicked(object sender, EventArgs e)
	{
        (BindingContext as ShoppingManagementViewModel)?.ReturnItem();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShoppingManagementViewModel)?.RefreshProductList();
    }
	
	private void InLineAddClicked(object sender, EventArgs e)
	{
		(BindingContext as ShoppingManagementViewModel)?.RefreshUX();

    }

	private void InLineDeleteClicked(object sender, EventArgs e)
	{
        (BindingContext as ShoppingManagementViewModel)?.RefreshUX();
    }

	private void SortToggled(object sender, CheckedChangedEventArgs e)
	{
        bool isChecked = e.Value;
        if (isChecked)
		{
			(BindingContext as ShoppingManagementViewModel)?.NameSort();
		}
		else
			(BindingContext as ShoppingManagementViewModel)?.PSort();

    }

	
}
