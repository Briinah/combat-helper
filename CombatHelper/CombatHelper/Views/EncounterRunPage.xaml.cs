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
    public partial class EncounterRunPage : ContentPage
    {
        private EncounterViewModel encounter;
        public EncounterRunPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            encounter = BindingContext as EncounterViewModel;
            AddPlayers();
            creatureList.ItemsSource = encounter.Creatures;
        }

        private async void AddPlayers()
        {
            //// add players to encounter if they are not added yet
            //List<PlayerCharacter> players = await App.Database.Players.Get<bool>((pc) => pc.CampaignID == encounter.CampaignID, null);
            //foreach (var pc in players)
            //{
                
            //    creatures.Add(new Creature
            //    {
            //        Name = pc.Name
            //    });
            //}
        }

        private async void SetInitiative(object sender, EventArgs e)
        {
            var creature = (CreatureViewModel)((Button)sender).BindingContext;
            string response = await DisplayPromptAsync(creature.Name, "Set initiative: ", maxLength: 2, keyboard: Keyboard.Numeric);
            if (!string.IsNullOrEmpty(response))
            {
                //var c = encounter.Creatures.IndexOf(creature);
                //creatures[c].Initiative = int.Parse(response);
                creature.Initiative = int.Parse(response);
            }
        }
    }
}