using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EncounterEditPage : ContentPage
    {
        private Encounter encounter;
        private ObservableCollection<Creature> observableCreatureList;

        public EncounterEditPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            encounter = (Encounter)BindingContext;
            Title = "New Encounter";
            if (encounter.ID != 0)
            {
                encounter = await App.Database.Encounters.GetWithChildren(encounter.ID);
                Title = $"Edit: {encounter.Name}";
            }

            if (encounter.Creatures == null)
                encounter.Creatures = new List<Creature>();
            observableCreatureList = new ObservableCollection<Creature>(encounter.Creatures);
            creatureList.ItemsSource = observableCreatureList;
        }

        protected override bool OnBackButtonPressed()
        {
            var encounter = (Encounter)BindingContext;
            if (encounter.ID != 0)
            {
                Navigation.InsertPageBefore(new EncounterDetailPage()
                {
                    BindingContext = encounter
                }, this);
            }

            Navigation.PopAsync();
            return true;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (encounter.ID == 0)
            {
                // get id of encounter from db
                await App.Database.Encounters.Insert(encounter);
            }

            SaveCreatures(encounter);
            await App.Database.Encounters.UpdateWithChildren(encounter);

            Navigation.InsertPageBefore(new EncounterDetailPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (await OnAlertYesNoClicked(encounter.Name))
            {
                if (encounter.ID != 0)
                {
                    RemoveCreatures(encounter);
                    await App.Database.Encounters.Delete(encounter);
                }

                await Navigation.PopAsync();
            }
        }

        private async void SaveCreatures(Encounter encounter)
        {
            foreach (var creature in encounter.Creatures)
            {
                creature.EncounterID = encounter.ID;

                if (creature.ID == 0)
                    await App.Database.Creatures.Insert(creature);
                else
                    await App.Database.Creatures.Update(creature);
            }
        }

        private async void RemoveCreatures(Encounter encounter)
        {
            foreach (var creature in encounter.Creatures)
            {
                if (creature.ID != 0)
                    await App.Database.Creatures.Delete(creature);
            }
        }

        private void RemoveCreature(object sender, EventArgs e)
        {
            Creature creature = (Creature)((ImageButton)sender).BindingContext;
            observableCreatureList.Remove(creature);
        }

        private async Task<bool> OnAlertYesNoClicked(string encounterName)
        {
            bool answer = await (DisplayAlert("Delete Encounter", $"Are you sure you want to delete {encounterName}?", "Yes", "No"));

            return answer;
        }

        private async void AddCreature(object sender, EventArgs e)
        {
            var creature = new Creature()
            { 
                EncounterID = encounter.ID 
            };

            await Navigation.PushAsync(new CreatureEditPage()
            {
                BindingContext = creature
            });
        }
    }
}