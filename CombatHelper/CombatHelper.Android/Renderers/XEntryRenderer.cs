using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using CombatHelper.Droid.Renderers;
using CombatHelper.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;

[assembly: ExportRenderer(typeof(XEntry), typeof(XEntryRenderer))]
namespace CombatHelper.Droid.Renderers
{
    public class XEntryRenderer : EntryRenderer
    {
        public XEntryRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                Control.SetBackground(gd);
            }
        }
    }
}