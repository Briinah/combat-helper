using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EncounterDetailPage : ContentPage
    {
        Encounter encounter;

        public EncounterDetailPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            encounter = (Encounter)BindingContext;
            encounter = await App.Database.Encounters.GetWithChildren(encounter.ID);
            creatureList.ItemsSource = encounter.Creatures;
            Title = encounter.Name;
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new EncounterEditPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
        }

        private void StartEncounter(object sender, EventArgs e)
        {

        }
    }
}