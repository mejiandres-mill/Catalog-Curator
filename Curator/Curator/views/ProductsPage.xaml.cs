﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Toasts;

namespace Curator
{
    public partial class ProductsPage : ContentPage
    {
        IToastNotificator notificator;
        CardStackView productCards;

        public ProductsPage()
        {
            InitializeComponent();
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
                Source = "main_on.png",
                BackgroundColor = Color.White,
                HeightRequest = 50,
                WidthRequest = 50,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            bottomMenu.Children.Add(btnProduct);

            view.Children.Add(bottomMenu,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Height - 60; }),
                Constraint.RelativeToParent((parent) => { return parent.Width; })
                );

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            notificator.Notify(ToastNotificationType.Error, "Wiishper", "Error al rechazar el producto", TimeSpan.FromSeconds(1));
            ((ProductViewModel)BindingContext).ProductList = await App.Manager.GetProducts(Constants.PENDING);
            if (((ProductViewModel)BindingContext).ProductList == null || ((ProductViewModel)BindingContext).ProductList.Count() <= 0)
            {
                await notificator.Notify(ToastNotificationType.Error, "Wiishper", "Ooops, no encontramos ningún producto", TimeSpan.FromSeconds(2));

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
            string result = await App.Manager.AcceptProducts(Constants.REJECT_PROD, idproduct);
            if (result.Equals("FAIL"))
                notificator.Notify(ToastNotificationType.Error, "Curator", "Error al rechazar producto " + idproduct, TimeSpan.FromSeconds(2));
        }
    }
}