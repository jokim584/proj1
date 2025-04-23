using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;
using proj1.Models;
using Library.eCommerce.Models;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public Item? SelectedProduct { get; set; }
        public string Sort { get; set; }

        public void NameSort()
        {
            Sort = "Name";
        }
        public void PSort()
        {
            Sort = "Price";
        }
        public string? Query { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current; //most things are passed by refernce
        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName="")
        {
            if(propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }   
        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }
        public ObservableCollection<Item?> Products
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Product?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                if(Sort == "Name")
                {
                    filteredList = filteredList.OrderBy(p => p?.Product?.Name);
                }
                else
                    filteredList = filteredList.OrderBy(p => p?.Product?.Price);
                return new ObservableCollection<Item?>(filteredList);
            }
        }
        public Item? Delete()
        {
            var item =  _svc.Delete(SelectedProduct?.Id ?? 0);
            NotifyPropertyChanged("Products");
            return item;
        }
    }
}
