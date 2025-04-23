using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Library.eCommerce.Services;

namespace Maui.eCommerce.ViewModels
{
    class TaxesManagementViewModel:INotifyPropertyChanged
    {
        private ShoppingCartService _cartSvc = ShoppingCartService.Current;
        private decimal _tempTaxed;
        
        public TaxesManagementViewModel()
        {
            _tempTaxed = _cartSvc.Tax;
        }

        public decimal Taxed
        {
            get => _cartSvc.Tax;
            set
            {
                if(_cartSvc.Tax!=value)
                {
                    _cartSvc.Tax = value;
                    RefreshUX();
                }
            }
        }

        public decimal TempTaxed
        {
            get => _tempTaxed;
            set
            {
                _tempTaxed = value;
            }
        }

        public void SaveTax()
        {
            Taxed = _tempTaxed;
        }

        public void Undo()
        {
            _tempTaxed = Taxed;
        }
        public void RefreshUX()
        {
            NotifyPropertyChanged(nameof(TempTaxed));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
                throw new ArgumentNullException(nameof(propertyName));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
