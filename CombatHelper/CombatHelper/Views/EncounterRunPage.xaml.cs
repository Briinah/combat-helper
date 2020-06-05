using CombatHelper.ViewModels;
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

            encounter.Creatures[turnIndex].HasTurn = true;
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

        private void EditHealth(object sender, EventArgs e)
        {

        }
    }
}