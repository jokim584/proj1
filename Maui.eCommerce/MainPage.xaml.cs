using Maui.eCommerce.ViewModels;

namespace Maui.eCommerce
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(); //class dictated  by binding context
        }

        private void InventoryClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//InventoryManagement"); //current means singleton
        }

        private void ShopClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ShoppingManagement");
        }
        private void BillClicked(object sender, EventArgs e)//needs to print out price and a list of all the items bought
        {
            Shell.Current.GoToAsync("//CheckOut");
        }

        private void TaxesClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//TaxesManagement");
        }
    }

}
