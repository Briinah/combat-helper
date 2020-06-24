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
    public partial class CampaignList : ContentPage
    {
        public CampaignList()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var listViewModel = new CampaignListViewModel();
            listView.ItemsSource = await listViewModel.GetCampaignList();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            var campaign = new CampaignViewModel();

            var name = await DisplayPromptAsync("New Campaign", "Name: ");
            if (!string.IsNullOrEmpty(name))
            {
                campaign.Name = name;
                await campaign.Save();

                await Navigation.PushAsync(new CampaignEditPage
                {
                    BindingContext = campaign
                });
            }
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new EncounterList
                {
                    BindingContext = e.SelectedItem as CampaignViewModel
                });
            }
        }
    }
}