using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace CombatHelper.Droid.Renderers
{
    public class Open5eWebViewClient : WebViewClient
    {
        public override void OnPageFinished(WebView view, string url)
        {
            var js = @"javascript:
                var menu = document.getElementsByClassName('mobile-header')[0].style.display='none';
                console.log(menu.parentNode);";

            view.LoadUrl(js);
            
            base.OnPageFinished(view, url);
        }
    }
}