using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Header : ContentView
    {
        public string LabelText { get; set; }
        public static readonly BindableProperty LabelTextProperty = BindableProperty.Create("LabelText", typeof(string), typeof(Header), propertyChanged: TextChanged);

        private static void TextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as Header;
            control.HeaderLabel.Text = newValue.ToString();
        }

        public ImageSource IconSource { get; set; }
        public static readonly BindableProperty IconSourceProperty = BindableProperty.Create("IconSource", typeof(ImageSource), typeof(Header), propertyChanged: IconChanged);
        private static void IconChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as Header;
            control.HeaderImage.Source = (ImageSource)newValue;
        }

        public Header()
        {
            BindingContext = this;
            InitializeComponent();
        }
    }
}