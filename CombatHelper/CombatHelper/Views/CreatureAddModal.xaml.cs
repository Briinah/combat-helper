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
    public partial class CreatureAddModal : ContentPage
    {
        CreatureViewModel creature;
        EncounterViewModel encounter;

        public CreatureAddModal(EncounterViewModel encounter)
        {
            this.encounter = encounter;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            creature = BindingContext as CreatureViewModel;
        }

        private int amountOfCreatures = 1;

        private async void SetInitiative(object sender, EventArgs e)
        {
            string response = await DisplayPromptAsync(creature.Name, "Set initiative: ", maxLength: 2, keyboard: Keyboard.Numeric);
            if (!string.IsNullOrEmpty(response))
            {
                creature.Initiative = int.Parse(response);
            }
        }

        private void SubtractAmount(object sender, EventArgs e)
        {
            if (amountOfCreatures <= 1)
                amountOfCreatures = 1;
            else
                amountOfCreatures--;

            Amount.Text = amountOfCreatures.ToString();
        }

        private void AddAmount(object sender, EventArgs e)
        {
            amountOfCreatures++;

            Amount.Text = amountOfCreatures.ToString();
        }

        private async void AddCreatures(object sender, EventArgs e)
        {
            // todo: add creatures
            for(int i = 0; i < amountOfCreatures; i++)
            {
                var copy = CreatureViewModel.Copy(creature);
                copy.Name = copy.Name + " " + (i + 1);
                encounter.Creatures.Add(copy);
            }

            encounter.Creatures.Sort(CreatureViewModel.CompareInitiative);

            await Navigation.PopModalAsync(true);
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}