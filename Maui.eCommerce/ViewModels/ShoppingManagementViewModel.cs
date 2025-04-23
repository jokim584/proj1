using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using proj1.Models;

namespace Maui.eCommerce.ViewModels
{
    public class ShoppingManagementViewModel : INotifyPropertyChanged//I is for header
    {
        private ProductServiceProxy _invSvc = ProductServiceProxy.Current;
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        public ItemViewModel? SelectedItem { get; set; }

        private bool Sort { get; set; }
        public ItemViewModel? SelectedCartItem { get; set; }
        public void NameSort()
        {
            Sort = true;
            RefreshUX();
        }
        public void PSort()
        {
            Sort = false;
            RefreshUX();
        }

        public ObservableCollection<ItemViewModel?> Inventory
        {
            get
            {
                /*return new ObservableCollection<ItemViewModel?>(_invSvc.Products.Where(i=>i?.Quantity>0).Select(m => new ItemViewModel(m))
                    );*/
                ObservableCollection<ItemViewModel?> filteredList;
                if (Sort) 
                    filteredList = new ObservableCollection<ItemViewModel?>(_invSvc.Products.Where(i => i?.Quantity > 0).OrderBy(i => i?.Product?.Name).Select(m => new ItemViewModel(m))
                   );
                else 
                    filteredList = new ObservableCollection<ItemViewModel?>(_invSvc.Products.Where(i => i?.Quantity > 0).OrderBy(i => i?.Product?.Price).Select(m => new ItemViewModel(m))
                     );

                return filteredList;
            }
        }

        public ObservableCollection<ItemViewModel?> ShoppingCart
        {
            get
            {
                /*return new ObservableCollection<ItemViewModel?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0).Select(m=>new ItemViewModel(m))
                    );*/
                ObservableCollection<ItemViewModel?> filteredList;
                if (Sort)
                    filteredList = new ObservableCollection<ItemViewModel?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0).OrderBy(i => i?.Product?.Name).Select(m => new ItemViewModel(m))
                   );
                else
                    filteredList = new ObservableCollection<ItemViewModel?>(_cartSvc.CartItems.Where(i => i?.Quantity > 0).OrderBy(i => i?.Product?.Price).Select(m => new ItemViewModel(m))
                     );

                return filteredList;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            if(propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(sender:this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(Inventory));
            NotifyPropertyChanged(nameof(ShoppingCart));
        }
        public void PurchaseItem()
        {
            if (SelectedItem != null)
            {
               var shouldRefresh = SelectedItem.Model.Quantity>= 1;
               var updatedItem = _cartSvc.AddOrUpdate(SelectedItem.Model);

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }

            }
        }

        public void ReturnItem()
        {
            if (SelectedCartItem != null)
            {
                var shouldRefresh = SelectedCartItem.Model.Quantity >= 1;

                var updatedItem = _cartSvc.ReturnItem(SelectedCartItem.Model);

                if (updatedItem != null && shouldRefresh)
                {
                    NotifyPropertyChanged(nameof(Inventory));
                    NotifyPropertyChanged(nameof(ShoppingCart));
                }

            }
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Inventory));
        }
    }
}
