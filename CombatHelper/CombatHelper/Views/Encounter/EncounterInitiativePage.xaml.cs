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
        private bool groupByName = false;

        private bool rolled = false;

        public EncounterInitiativePage(bool groupByName)
        {
            InitializeComponent();

            this.groupByName = groupByName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            encounter = BindingContext as EncounterViewModel;
            encounter.AddPlayers();
            creatureList.ItemsSource = encounter.Creatures;

            if(!rolled)
                RollInitiative();
        }

        private void RollInitiative()
        {
            var random = new Random();

            // creatures with the same name are grouped in the same initative
            var namedict = new Dictionary<string, int>();

            foreach (var c in encounter.Creatures)
            {
                if (!c.IsPC)
                {
                    if (groupByName)
                    {
                        if (namedict.ContainsKey(c.Name))
                            c.Initiative = namedict[c.Name];
                        else
                        {
                            c.Initiative = random.Next(20) + c.Dexterity;
                            namedict.Add(c.Name, c.Initiative);
                        }
                    }
                    else
                        c.Initiative = random.Next(20) + c.Dexterity;
                }
            }

            encounter.Creatures.Sort(CreatureViewModel.CompareInitiative);

            rolled = true;
        }

        private async void SetInitiative(object sender, EventArgs e)
        {
            var creature = (CreatureViewModel)((Button)sender).BindingContext;
            var dex = creature.GetModString(creature.Dexterity);
            string response = await DisplayPromptAsync(creature.Name, $"Set initiative, dex ({dex}): ", maxLength: 2, keyboard: Keyboard.Numeric);
            if (!string.IsNullOrEmpty(response))
            {
                creature.Initiative = int.Parse(response);
                if (creature.Initiative < 1)
                    creature.Initiative = 1;
            }

            // sort list on initiative
            encounter.Creatures.Sort(CreatureViewModel.CompareInitiative);
        }

        private async void StartEncounter(object sender, EventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            Navigation.InsertPageBefore(new EncounterRunPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
            IsBusy = false;
        }
    }
}