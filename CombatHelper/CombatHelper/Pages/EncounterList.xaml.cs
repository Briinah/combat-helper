using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EncounterList : ContentPage
    {
        private ObservableCollection<Encounter> observableEncounterList;
        private Campaign campaign;

        public EncounterList()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            campaign = (Campaign)BindingContext;
            campaign = await App.Database.Campaigns.GetWithChildren(campaign.ID);
            Title = campaign.Name;

            if (campaign.Encounters == null)
                campaign.Encounters = new List<Encounter>();
            observableEncounterList = new ObservableCollection<Encounter>(campaign.Encounters);
            encounterList.ItemsSource = observableEncounterList;
        }

        private async void NewEncounter(object sender, EventArgs e)
        {
            var encounter = new Encounter()
            {
                CampaignID = campaign.ID
            };

            await Navigation.PushAsync(new EncounterEditPage()
            {
                BindingContext = encounter
            });
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EncounterDetailPage
                {
                    BindingContext = e.SelectedItem as Encounter
                });
            }
        }
    }
}