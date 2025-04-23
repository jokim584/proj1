using Library.eCommerce.Services;
using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce.ViewsModels;

public partial class InventoryManagementView : ContentPage
{
	public InventoryManagementView()
	{
		InitializeComponent();
		BindingContext = new InventoryManagementViewModel();//bindingcontext is a property on ContentPage
	}

    private void DeleteClicked(object sender, EventArgs e)
    {
		(BindingContext as InventoryManagementViewModel)?.Delete();
    }
    private void CancelClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//MainPage");
	}

	private void AddClicked(object sender, EventArgs e)
	{
        Shell.Current.GoToAsync("//Product");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		(BindingContext as InventoryManagementViewModel)?.RefreshProductList();
		
    }

    private void EditClicked(object sender, EventArgs e)
    {
        var productId = (BindingContext as InventoryManagementViewModel)?.SelectedProduct?.Id;
        Shell.Current.GoToAsync($"//Product?productId={productId}");
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
    }

    private void SortToggled(object sender, CheckedChangedEventArgs e)
    {
        bool isChecked = e.Value;
        if (isChecked)
        {
            (BindingContext as InventoryManagementViewModel)?.NameSort();
            (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
        }
        else
        {
            (BindingContext as InventoryManagementViewModel)?.PSort();
            (BindingContext as InventoryManagementViewModel)?.RefreshProductList();
        }
        
    }

 

}
