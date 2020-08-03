using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Interfaces.Animations;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EncounterInfoPopup : PopupPage
    {
        public EncounterInfoPopup()
        {
            InitializeComponent();
        }

        private async void ClosePopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            sourceTextView.IsVisible = false;
            quickView.IsVisible = true;
        }

        private void ShowSource(object sender, EventArgs e)
        {
            sourceTextView.IsVisible = true;
            quickView.IsVisible = false;
        }
    }
}