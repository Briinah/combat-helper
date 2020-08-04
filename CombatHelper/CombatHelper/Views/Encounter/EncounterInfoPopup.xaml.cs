using CombatHelper.ViewModels;
using CombatHelper.Views.Encounter;
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
        private CreatureViewModel creature;

        public EncounterInfoPopup()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            creature = BindingContext as CreatureViewModel;
            if (creature.HasTurn)
            {
                sourceTextView.IsVisible = true;
                quickView.IsVisible = false;
            }
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

        private async void ShowConditionSelect(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new ConditionSelectPopup
            {
                BindingContext = creature
            });
        }
    }
}