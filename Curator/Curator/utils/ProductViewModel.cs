using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace Curator
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        List<Product> products = new List<Product>();

        public List<Product> ProductList
        {
            get
            {
                return products;
            }
            set
            {
                if (products == value)
                {
                    products = value;
                    OnProperityChanged();
                }
            }
        }

        private void OnProperityChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            field = value;
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
