using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Curator
{
    public partial class OpenGLPage : ContentPage
    {
        public OpenGLPage()
        {
            InitializeComponent();
            var view = new OpenGLView { HasRenderLoop = true };
            view.HeightRequest = 300;
            view.WidthRequest = 300;

            view.OnDisplay = r =>
            {
                
            }
        }
    }
}
