using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Toasts;

using Xamarin.Forms;

namespace Curator
{
    public partial class AcceptedProducts : ContentPage
    {
        private IToastNotificator notificator;
        private List<Product> products;

        public AcceptedProducts(int state)
        {
            InitializeComponent();
            /*  TapGestureRecognizer tapper_products = new TapGestureRecognizer();
              tapper_products.Tapped += OnProducts;
              btnProducts.GestureRecognizers.Add(tapper_products);

              NavigationPage.SetHasNavigationBar(this, false);
              notificator = DependencyService.Get<IToastNotificator>();


          }
          private async OnProducts(object sender, EventArgs e)
          {
              Navigation.InsertPageBefore(new prod)
          }*/
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}
