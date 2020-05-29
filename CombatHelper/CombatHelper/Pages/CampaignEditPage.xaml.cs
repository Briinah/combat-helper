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
    public partial class CampaignEditPage : ContentPage
    {
        private ObservableCollection<PlayerCharacter> observablePCList;
        public CampaignEditPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var campaign = (Campaign)BindingContext;
            campaign = await App.Database.Campaigns.GetWithChildren(campaign.ID);
            if (campaign.Players == null)
                campaign.Players = new List<PlayerCharacter>();
            observablePCList = new ObservableCollection<PlayerCharacter>(campaign.Players);
            playerList.ItemsSource = observablePCList;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var campaign = (Campaign)BindingContext;
            campaign.Players = observablePCList.ToList();

            if (campaign.ID == 0)
            {
                // get id of campaign from db
                await App.Database.Campaigns.Insert(campaign);
            }

            SavePlayers(campaign);
            // update campaign with players
            await App.Database.Campaigns.UpdateWithChildren(campaign);

            Navigation.InsertPageBefore(new CampaignDetailPage()
            {
                BindingContext = campaign
            }, this);

            await Navigation.PopAsync();
        }

        private async void SavePlayers(Campaign campaign)
        {
            foreach (var pc in campaign.Players)
            {
                pc.CampaignID = campaign.ID;

                if (pc.ID == 0)
                    await App.Database.Players.Insert(pc);
                else
                    await App.Database.Players.Update(pc);
            }
        }

        private async void RemovePlayers(Campaign campaign)
        {
            foreach(var pc in campaign.Players)
            {
                if (pc.ID != 0)
                    await App.Database.Players.Delete(pc);
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var campaign = (Campaign)BindingContext;
            campaign = await App.Database.Campaigns.GetWithChildren(campaign.ID);

            if (await OnAlertYesNoClicked(campaign.Name))
            {
                RemovePlayers(campaign);

                if(campaign.ID != 0)
                    await App.Database.Campaigns.Delete(campaign);

                await Navigation.PopAsync();
            }
        }

        private async Task<bool> OnAlertYesNoClicked(string campaignName)
        {
            bool answer = await (DisplayAlert("Delete Campaign", $"Are you sure you want to delete {campaignName}?", "Yes", "No"));

            return answer;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void OnPlayerAdded(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newPlayer.Text))
            {
                observablePCList.Add(new PlayerCharacter() { Name = newPlayer.Text });
                newPlayer.Text = "";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.InsertPageBefore(new CampaignDetailPage()
            {
                BindingContext = this.BindingContext
            }, this);

            Navigation.PopAsync();
            return true;
        }
    }
}