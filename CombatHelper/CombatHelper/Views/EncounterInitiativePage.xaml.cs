using CombatHelper.Models;
using CombatHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EncounterInitiativePage : ContentPage
    {
        private EncounterViewModel encounter;
        public EncounterInitiativePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            encounter = BindingContext as EncounterViewModel;
            encounter.AddPlayers();
            creatureList.ItemsSource = encounter.Creatures;
        }

        private async void SetInitiative(object sender, EventArgs e)
        {
            var creature = (CreatureViewModel)((Button)sender).BindingContext;
            string response = await DisplayPromptAsync(creature.Name, "Set initiative: ", maxLength: 2, keyboard: Keyboard.Numeric);
            if (!string.IsNullOrEmpty(response))
            {
                creature.Initiative = int.Parse(response);
            }

            // sort list on initiative
            encounter.Creatures.Sort(CompareInitiative);
        }

        private int CompareInitiative(CreatureViewModel a, CreatureViewModel b)
        {
            return b.Initiative.CompareTo(a.Initiative);
        }

        private async void StartEncounter(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EncounterRunPage()
            {
                BindingContext = encounter
            });
        }
    }
}