#region Assembly Toasts.Forms.Plugin.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// C:\Users\Desarrollador3\Source\Repos\wiishper\packages\Toasts.Forms.Plugin.2.0.4\lib\portable-net45+wp8+win8+wpa81+netcore451+MonoAndroid10+MonoTouch10+Xamarin.iOS10\Toasts.Forms.Plugin.Abstractions.dll
#endregion

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toasts
{
    public interface IToastNotificator
    {
        void HideAll();
        Task<bool> Notify(ToastNotificationType type, string title, string description, TimeSpan duration, object context = null, bool clickable = true);
        Task NotifySticky(ToastNotificationType type, string title, string description, object context = null, bool clickable = true, CancellationToken cancellationToken = default(CancellationToken), bool modal = false);
       
    }
}