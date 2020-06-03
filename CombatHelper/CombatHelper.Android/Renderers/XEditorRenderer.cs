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

[assembly: ExportRenderer(typeof(XEditor), typeof(XEditorRenderer))]
namespace CombatHelper.Droid.Renderers
{
    public class XEditorRenderer : EditorRenderer
    {
        public XEditorRenderer(Context context)
            : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                int[][] states = new int[][]
                {
                    new int[] { -Android.Resource.Attribute.StateFocused}, // enabled
                    new int[] {Android.Resource.Attribute.StateFocused} // disabled
                };

                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                gd.SetCornerRadius(10);
                Control.SetBackground(gd);
                Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}