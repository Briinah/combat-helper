using CombatHelper.ViewModels;
using CombatHelper.Views.Encounter;
using Microsoft.AppCenter.Analytics;
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
    public partial class EncounterRunPage : ContentPage
    {
        private EncounterViewModel encounter;

        private int turnIndex = 0;

        public EncounterRunPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            encounter = BindingContext as EncounterViewModel;
            creatureList.ItemsSource = encounter.Creatures;

            // check if a creature has a turn
            var turn = encounter.Creatures.FirstOrDefault((c) => c.HasTurn);
            if (turn == null)
                encounter.Creatures[turnIndex].HasTurn = true;
            else
                turnIndex = encounter.Creatures.IndexOf(turn);

            App.OnSleeping += App_OnSleeping;
        }

        private void App_OnSleeping(object sender, EventArgs e)
        {
            App.ResourceManager.SaveEncounter(encounter);

            Analytics.TrackEvent("Save encounter on sleep");
        }

        protected override void OnDisappearing()
        {
            App.OnSleeping -= App_OnSleeping;
            App.ResourceManager.SaveEncounter(encounter);

            Analytics.TrackEvent("Save encounter on dissapearing");

            base.OnDisappearing();
        }

        private void NextTurn(object sender, EventArgs e)
        {
            encounter.Creatures[turnIndex].HasTurn = false;
            if (turnIndex >= encounter.Creatures.Count - 1)
            {
                turnIndex = 0;
                encounter.Round++;
            }
            else
            {
                turnIndex++;
            }

            encounter.Creatures[turnIndex].HasTurn = true;
        }

        private async void EditHealth(object sender, EventArgs e)
        {
            var creature = ((Button)sender).BindingContext as CreatureViewModel;
            await PopupNavigation.Instance.PushAsync(new HealthPopup
            {
                BindingContext = creature
            });
        }

        private async void AddCreature(object sender, EventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;
            await PopupNavigation.Instance.PushAsync(new CreatureAddPopup(encounter)
            {
                BindingContext = new CreatureViewModel()
            });
            IsBusy = false;
        }

        private async void ShowInfoView(object sender, ItemTappedEventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;
            var creature = e.Item as CreatureViewModel;

            await PopupNavigation.Instance.PushAsync(new EncounterInfoPopup
            {
                BindingContext = creature
            });
            IsBusy = false;
        }

        private async void OnSetConditionsClicked(object sender, EventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;
            var creature = ((Button)sender).BindingContext as CreatureViewModel;
            await PopupNavigation.Instance.PushAsync(new ConditionSelectPopup
            {
                BindingContext = creature
            });
            IsBusy = false;
        }
    }
}