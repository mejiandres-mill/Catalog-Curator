using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;
using System.Diagnostics;

namespace Curator
{
    public partial class ProductsPage : ContentPage
    {
        IToastNotificator notificator;
        CardStackView productCards;
        int state;

        public ProductsPage(int status)
        {
            InitializeComponent();
            state = status;
            BindingContext = new ProductViewModel();
            productCards = new CardStackView();
            productCards.SetBinding(CardStackView.ItemSourceProperty, "ProductList");
            productCards.SwipedLeft += SwipedLeft;
            productCards.SwipedRight += SwipedRight;
            RelativeLayout view = new RelativeLayout();
            view.BackgroundColor = Color.FromHex("F2F2F2");
            NavigationPage.SetHasNavigationBar(this, false);
            notificator = DependencyService.Get<IToastNotificator>();

            view.Children.Add(productCards,
                Constraint.Constant(30),
                Constraint.Constant(60),
                Constraint.RelativeToParent((parent) => { return parent.Width - 60; }),
                Constraint.RelativeToParent((parent) => { return parent.Height - 160; })
            );
            this.LayoutChanged += (object sender, System.EventArgs e) =>
            {
                productCards.CardMoveDistance = (int)(this.Width * 0.60f);
            };
            BoxView upperSeparator = new BoxView
            {
                BackgroundColor = Color.FromHex("86CACD")
            };

            view.Children.Add(upperSeparator,
                Constraint.Constant(0),
                Constraint.Constant(50),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.Constant(2));
            StackLayout upperBar = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.FromHex("5D5D5D")
            };

            Image logo = new Image
            {
                Source = ImageSource.FromFile("wordlogo.png"),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HeightRequest = 50
            };
            upperBar.Children.Add(logo);
            view.Children.Add(upperBar,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.Constant(50));

            StackLayout bottomMenu = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                HeightRequest = 60
            };

            Image btnProduct = new Image
            {
                Source = status == Constants.PENDING ? "main_on.png": "main_off.png",
                BackgroundColor = Color.White,
                HeightRequest = 50,
                WidthRequest = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            TapGestureRecognizer tapper_menu = new TapGestureRecognizer();
            tapper_menu.Tapped += OnProducts;
            btnProduct.GestureRecognizers.Add(tapper_menu);
            
            Image btnProductsAccepted = new Image
            {
                Source = status == Constants.OK ? "like_on.png": "like.png",
                BackgroundColor = Color.White,
                HeightRequest = 25,
                WidthRequest = 25,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            TapGestureRecognizer tapper_accepted = new TapGestureRecognizer();
            tapper_accepted.Tapped += OnAcceptedProduct;
            btnProductsAccepted.GestureRecognizers.Add(tapper_accepted);

            Image btnProductsRejected = new Image
            {
                Source = status == Constants.REJECTING ? "dislike_on.png": "dislike.png",
                BackgroundColor = Color.White,
                HeightRequest = 25,
                WidthRequest = 25,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            TapGestureRecognizer tapper_rejected = new TapGestureRecognizer();
            tapper_rejected.Tapped += OnRejectedProduct;
            btnProductsRejected.GestureRecognizers.Add(tapper_rejected);

            bottomMenu.Children.Add(btnProductsRejected);
            bottomMenu.Children.Add(btnProduct);
            bottomMenu.Children.Add(btnProductsAccepted);

            view.Children.Add(bottomMenu,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Height - 60; }),
                Constraint.RelativeToParent((parent) => { return parent.Width; })
                );

            Content = view;

        }

        private async void OnProducts(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new ProductsPage(Constants.PENDING), this);
            await Navigation.PopAsync();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            notificator.Notify(ToastNotificationType.Info, "Wiishper", "Leyendo productos...", TimeSpan.FromSeconds(1));
            List<Product> prods = await App.Manager.GetProducts(state);
             
            if (prods == null || prods.Count() <= 0)
            {
                await notificator.Notify(ToastNotificationType.Error, "Wiishper", "Ooops, no encontramos ningún producto", TimeSpan.FromSeconds(2));

            }
            else
            {
                ((ProductViewModel)BindingContext).ProductList = prods;
            }
        }

        async void SwipedRight(int index)
        {
            int idproduct = productCards.product.idproducts;
            string result = await App.Manager.AcceptProducts(Constants.ACCEPT_PROD, idproduct);
            if (result.Equals("FAIL"))
                notificator.Notify(ToastNotificationType.Error, "Curator", "Error al aceptar producto " + idproduct, TimeSpan.FromSeconds(2));
            
        }

        async void SwipedLeft(int index)
        {
            int idproduct = productCards.product.idproducts;
            string result = await App.Manager.RejectProducts(Constants.REJECT_PROD, idproduct);
            if (result.Equals("FAIL"))
                notificator.Notify(ToastNotificationType.Error, "Curator", "Error al rechazar producto " + idproduct, TimeSpan.FromSeconds(2));
        }

        private async void OnAcceptedProduct(object sender, EventArgs e)
        {
             Navigation.InsertPageBefore(new ProductsPage(Constants.OK), this);
            await Navigation.PopAsync();
        }
        private async void OnRejectedProduct(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new ProductsPage(Constants.REJECTING), this);
            await Navigation.PopAsync();
        }
    }
}
