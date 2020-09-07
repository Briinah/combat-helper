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
    public partial class SpellSlot : ContentView
    {
        public event EventHandler<bool> OnIsUsedChanged;

        public bool IsUsed
        {
            get { return (bool)GetValue(IsUsedProperty); }
            set { SetValue(IsUsedProperty, value); }
        }
        public int Level { get; set; }

        public static readonly BindableProperty IsUsedProperty = BindableProperty.Create("SpellSlots", typeof(bool), typeof(SpellSlot), false, propertyChanged: IsUsedChanged);

        private static void IsUsedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as SpellSlot;
            control.fill.IsVisible = (bool)newValue;

            if(oldValue != newValue)
                control.OnIsUsedChanged.Invoke(control, (bool)newValue);
        }

        public SpellSlot()
        {
            InitializeComponent();
            fill.IsVisible = IsUsed;
        }

        private void OnClicked(object sender, EventArgs e)
        {
            IsUsed = !IsUsed;
        }
    }
}