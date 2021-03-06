﻿using CombatHelper.Data;
using CombatHelper.ViewModels;
using Rg.Plugins.Popup.Services;
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
    public partial class CreatureAddPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        CreatureViewModel creature;
        EncounterViewModel encounter;

        public CreatureAddPopup(EncounterViewModel encounter)
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

        private async void OnInitativeClicked(object sender, EventArgs e)
        {
            await SetInitative();
        }

        private async Task SetInitative()
        {
            var dex = creature.GetModString(creature.Dexterity);
            string response = await DisplayPromptAsync(creature.Name, $"Set initiative, dex ({dex}): ", maxLength: 2, keyboard: Keyboard.Numeric);
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
            if (IsBusy) return;

            IsBusy = true;
            if (creature.Initiative == 0)
            {
                await SetInitative();
            }

            for (int i = 0; i < amountOfCreatures; i++)
            {
                var copy = CreatureViewModel.Copy(creature);
                copy.Number = i + 1;
                encounter.Creatures.Add(copy);
            }

            encounter.Creatures.Sort(CreatureViewModel.CompareInitiative);

            await PopupNavigation.Instance.PopAsync();

            IsBusy = false;
        }

        private async void SearchCreature(object sender, EventArgs e)
        {
            using (RestService service = new RestService())
            {
                IsBusy = true;
                var list = await service.GetSearchResults(creature.Name);
                IsBusy = false;

                if (list.Count > 0)
                {
                    var actions = list.Select((l) => l.Name).ToArray();
                    var action = await DisplayActionSheet($"We found these results. Fill data?", "Cancel", null, actions);

                    if (action == "Cancel")
                        return;

                    var id = list.FirstOrDefault((l) => l.Name == action)?.Id;

                    if (id != null)
                    {
                        var data = await service.GetCreature(id);
                        creature.FillFromData(data);
                    }

                }
                else
                {
                    await DisplayAlert("Nothing found", "No search result for " + creature.Name, "Cancel");
                }
            }
        }

        private void NextEntry(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            var grid = (entry.Parent as Grid).Children;
            var index = grid.IndexOf(entry);
            if (index == -1)
                return;

            var next = grid.ElementAt(index + 2) as Entry; //skip label
            next?.Focus();
            // this way you do not have to remove the 0 before typing a new number
            if (next.Text == "0")
                next.Text = "";
        }
    }
}