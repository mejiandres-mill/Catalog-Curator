using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using FFImageLoading.Forms;

namespace Curator
{
    public class CardView : ContentView
    {
        public Label Name { get; set; }
        public CachedImage Photo { get; set; }
        public Label Price { get; set; }
        public Label Store { get; set; }

        public CardView()
        {
            RelativeLayout view = new RelativeLayout();

            BoxView boxView1 = new BoxView { Color = Color.White, InputTransparent = true };

            view.Children.Add(boxView1,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => {return parent.Width; }),
                Constraint.RelativeToParent((parent) => {return parent.Height;})
                );
            StackLayout nameLayout = new StackLayout { InputTransparent = true, BackgroundColor = Color.White, Orientation = StackOrientation.Vertical };

            Name = new Label
            {
                TextColor = Color.FromHex("26CAD3"),
                BackgroundColor = Color.White,
                FontSize = 18,
                InputTransparent = true,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand

            };

            nameLayout.Children.Add(Name);

            view.Children.Add(nameLayout,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Width;  }),
                Constraint.Constant(46));

            BoxView NameBox = new BoxView { BackgroundColor = Color.FromHex("1026CAD3") };

            view.Children.Add(NameBox,
                Constraint.RelativeToParent((parent) => { return parent.Width * 0.1; }),
                Constraint.RelativeToView(nameLayout, (parent, sibling) => { return sibling.Height; }),
                Constraint.RelativeToParent((parent) => { return parent.Width * 0.8; }),
                Constraint.Constant(2));

            Photo = new CachedImage { InputTransparent = true, Aspect = Aspect.AspectFit, LoadingPlaceholder = "image_loading.png", ErrorPlaceholder = "image_error.png" };

            view.Children.Add(Photo,
                Constraint.Constant(0),
                Constraint.RelativeToView(nameLayout, (parent, sibling) => { return sibling.Height + 2; }),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height - 99;  })
            );

            StackLayout bottomLayout = new StackLayout { Spacing = 0, BackgroundColor = Color.White, Orientation = StackOrientation.Vertical, InputTransparent = true, Padding = new Thickness(0, 10) };

            Price = new Label
            {
                TextColor = Color.FromHex("4E4E4E"),
                FontSize = 16,
                InputTransparent = true,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 5, 0),
                BackgroundColor = Color.White,
                FontAttributes = FontAttributes.Bold
            };

            Store = new Label
            {
                TextColor = Color.FromHex("4E4E4E"),
                BackgroundColor = Color.White,
                InputTransparent = true,
                FontSize = 10,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Fill

            };

            bottomLayout.Children.Add(Price);

            StackLayout locationStack = new StackLayout { Spacing = 0, BackgroundColor = Color.White, Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.CenterAndExpand , HorizontalOptions = LayoutOptions.CenterAndExpand};

            Image Location = new Image { Source = "location.png", HorizontalOptions = LayoutOptions.Fill };

            locationStack.Children.Add(Location);
            locationStack.Children.Add(Store);

            StackLayout yetAnotherStack = new StackLayout { Spacing = 0, BackgroundColor = Color.White, Orientation = StackOrientation.Horizontal, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.CenterAndExpand };
            Image location = new Image { Source = "location.png", HorizontalOptions = LayoutOptions.Fill, Margin = new Thickness(0), BackgroundColor = Color.White };
            yetAnotherStack.Children.Add(Location);
            yetAnotherStack.Children.Add(Store);

            bottomLayout.Children.Add(yetAnotherStack);

            view.Children.Add(bottomLayout,
                Constraint.Constant(0),
                Constraint.RelativeToParent((parent) => { return parent.Height - 51; }),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.Constant(51));

            BoxView BottomBox = new BoxView { BackgroundColor = Color.FromHex("1026CAD3") };

            view.Children.Add(BottomBox,  Constraint.RelativeToParent((parent) => { return parent.Width * 0.1; }),
                Constraint.RelativeToParent((parent) => { return parent.Height - 53; }),
                Constraint.RelativeToParent((parent) => { return parent.Width * 0.8; }),
                Constraint.Constant(2));

            Content = view;
        }
    }
}
