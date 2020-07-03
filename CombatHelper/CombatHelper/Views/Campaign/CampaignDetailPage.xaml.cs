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
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new CampaignEditPage()
            {
                BindingContext = this.BindingContext
            }, this);

            await Navigation.PopAsync();
        }
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EncounterDetailPage
                {
                    BindingContext = e.SelectedItem as EncounterViewModel
                });
            }
        }
    }
}