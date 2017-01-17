using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Curator
{
    public class App : Application
    {
        public static RESTManager Manager { get; set; }

        public App()
        {
            Manager = new RESTManager(new RestService());
            MainPage = new NavigationPage(new ProductsPage(Constants.PENDING));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
