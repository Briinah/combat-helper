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
    public partial class CampaignDetailPage : ContentPage
    {
        private CampaignViewModel campaign;

        public CampaignDetailPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            campaign = BindingContext as CampaignViewModel;
            await campaign.LoadData();
            Title = campaign.Name;

            encounterList.ItemsSource = campaign.Encounters;
        }

        private async void NewEncounter(object sender, EventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            var encounter = new EncounterViewModel()
            {
                CampaignId = campaign.Id
            };

            var name = await DisplayPromptAsync("New Encounter", "Name: ");

            if (!string.IsNullOrEmpty(name))
            {
                encounter.Name = name;
                await encounter.Save();

                await Navigation.PushAsync(new EncounterEditPage()
                {
                    BindingContext = encounter
                });
            }

            IsBusy = false;
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            Navigation.InsertPageBefore(new CampaignEditPage()
            {
                BindingContext = this.BindingContext
            }, this);

            await Navigation.PopAsync();

            IsBusy = false;
        }
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EncounterDetailPage
                {
                    BindingContext = e.SelectedItem as EncounterViewModel
                });
            }
            IsBusy = false;
        }
    }
}