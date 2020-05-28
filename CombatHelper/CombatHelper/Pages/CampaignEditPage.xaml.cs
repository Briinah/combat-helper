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
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var campaign = (Campaign)BindingContext;
            if (campaign.Players == null)
                campaign.Players = new List<PlayerCharacter>();
            observablePCList = new ObservableCollection<PlayerCharacter>(campaign.Players);
            playerList.ItemsSource = observablePCList;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var campaign = (Campaign)BindingContext;
            campaign.Players = observablePCList.ToList();
            // TODO: look at how players are saved with repository pattern
            await App.Database.Campaigns.Insert(campaign);
            await Navigation.PushAsync(new CampaignDetailPage
            {
                BindingContext = campaign
            }) ;
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {

            var campaign = (Campaign)BindingContext;

            if (await OnAlertYesNoClicked(campaign.Name))
            {
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
            if(!string.IsNullOrEmpty(newPlayer.Text))
            {
                observablePCList.Add(new PlayerCharacter() { Name = newPlayer.Text }) ;
                newPlayer.Text = "";
            }
        }
    }
}