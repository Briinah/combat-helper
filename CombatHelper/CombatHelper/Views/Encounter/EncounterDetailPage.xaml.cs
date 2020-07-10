using CombatHelper.Models;
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
    public partial class EncounterDetailPage : ContentPage
    {
        EncounterViewModel encounter;

        public EncounterDetailPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            encounter = BindingContext as EncounterViewModel;
            await encounter.ReloadData();
            creatureList.ItemsSource = encounter.Creatures;
            Title = encounter.Name;
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            Navigation.InsertPageBefore(new EncounterEditPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
            IsBusy = false;
        }

        private async void RollInitiative(object sender, EventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            await Navigation.PushAsync(new EncounterInitiativePage(groupByName.IsToggled)
            {
                BindingContext = encounter
            });
            IsBusy = false;
        }
    }
}