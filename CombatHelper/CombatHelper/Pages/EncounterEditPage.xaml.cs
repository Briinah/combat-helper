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
    public partial class EncounterEditPage : ContentPage
    {
        public EncounterEditPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
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
            var encounter = (Encounter)BindingContext;

            if (encounter.ID == 0)
            {
                // get id of encounter from db
                await App.Database.Encounters.Insert(encounter);
            }

            // do not save creatures here yet
            await App.Database.Encounters.Update(encounter);

            Navigation.InsertPageBefore(new EncounterDetailPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var encounter = (Encounter)BindingContext;

            if (encounter.ID != 0)
            {
                // no creatures yet
                encounter = await App.Database.Encounters.Get(encounter.ID);
                await App.Database.Encounters.Delete(encounter);
            }

            await Navigation.PopAsync();
        }
    }
}