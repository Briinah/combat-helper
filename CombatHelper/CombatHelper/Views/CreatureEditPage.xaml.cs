using CombatHelper.Data;
using CombatHelper.Models;
using CombatHelper.ViewModels;
using SQLite;
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
    public partial class CreatureEditPage : ContentPage
    {
        private CreatureViewModel creature;

        public CreatureEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            creature = BindingContext as CreatureViewModel;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            creature.Save();
            await Navigation.PopAsync();
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
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

        private async void SearchCreature(object sender, EventArgs e)
        {
            using(RestService service = new RestService())
            {
                var list = await service.GetSearchResults(creature.Name);

                if(list.Count > 0)
                {
                    var actions = list.Select((l) => l.Name).ToArray();
                    var action = await DisplayActionSheet($"We found {list.Count} results. Fill data?", "Cancel", null, actions);

                    if (action == "Cancel")
                        return;

                    var id = list.FirstOrDefault((l) => l.Name == action)?.Id;

                    if(id != null)
                    {
                        var data = await service.GetCreature(id);

                        creature.Name = data.Name;
                        creature.HP = data.HP;
                        creature.AC = data.AC;
                        creature.Strength = data.Strength;
                        creature.Dexterity = data.Dexterity;
                        creature.Constitution = data.Constitution;
                        creature.Intelligence = data.Intelligence;
                        creature.Wisdom = data.Wisdom;
                        creature.Charisma = data.Charisma;
                    }

                }

                else
                {
                    await DisplayAlert("Nothing found", "No search result for " + creature.Name, "Cancel");
                }
            }
        }
    }
}