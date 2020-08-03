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
    public partial class EncounterCreatureListItem : ContentView
    {
        public event EventHandler OnHealthClicked;
        public static readonly BindableProperty OnHealthClickedProperty = BindableProperty.Create("OnHealthClicked", typeof(EventHandler), typeof(EncounterCreatureListItem));

        public EncounterCreatureListItem()
        {
            InitializeComponent();
        }

        private void HealthButton_Clicked(object sender, EventArgs e)
        {
            OnHealthClicked.Invoke(sender, e);
        }
    }
}