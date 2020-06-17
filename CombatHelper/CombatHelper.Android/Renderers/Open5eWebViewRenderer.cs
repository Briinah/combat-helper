using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CombatHelper.Controls;
using CombatHelper.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Open5eWebView), typeof(Open5eWebViewRenderer))]
namespace CombatHelper.Droid.Renderers
{
    class Open5eWebViewRenderer : WebViewRenderer
    {
        public Open5eWebViewRenderer(Context context)
            : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            Control.SetWebViewClient(new Open5eWebViewClient());
        }
    }
}