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
    public partial class AttributeInput : ContentView
    {
        public event EventHandler OnNextEntry;
        public static readonly BindableProperty OnNextEntryProperty = BindableProperty.Create("OnNextEntry", typeof(EventHandler), typeof(AttributeInput));

        public AttributeInput()
        {
            InitializeComponent();
        }

        private void NextEntry_Completed(object sender, EventArgs e)
        {
            OnNextEntry.Invoke(sender, e);
        }
    }
}