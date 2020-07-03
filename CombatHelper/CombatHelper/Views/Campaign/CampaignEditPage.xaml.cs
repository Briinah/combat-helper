using CombatHelper.ViewModels;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CampaignEditPage : ContentPage
    {
        private CampaignViewModel campaign;

        public CampaignEditPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            campaign = BindingContext as CampaignViewModel;

            Title = "New Campaign";
            if (campaign.Id != 0)
            {
                await campaign.LoadData();
                Title = $"Edit: {campaign.Name}";
            }

            playerList.ItemsSource = campaign.Players;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            await campaign.Save();
            Navigation.InsertPageBefore(new CampaignDetailPage()
            {
                BindingContext = campaign
            }, this);

            await Navigation.PopAsync();
        }



        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (await OnAlertYesNoClicked(campaign.Name))
            {
                await campaign.Delete();

                await Navigation.PopToRootAsync();
            }
        }

        private async Task<bool> OnAlertYesNoClicked(string campaignName)
        {
            bool answer = await (DisplayAlert("Delete Campaign", $"Are you sure you want to delete {campaignName}?", "Yes", "No"));

            return answer;
        }

        private void OnPlayerAdded(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(newPlayer.Text))
            {
                // insert in front of the list
                campaign.Players.Insert(0, new PlayerCharacterViewModel() { Name = newPlayer.Text });
                newPlayer.Text = "";
            }
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.InsertPageBefore(new CampaignDetailPage()
            {
                BindingContext = campaign
            }, this);

            Navigation.PopAsync();
            return true;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        private void RemovePlayer(object sender, EventArgs e)
        {
            PlayerCharacterViewModel pc = ((ImageButton)sender).BindingContext as PlayerCharacterViewModel;
            campaign.Players.Remove(pc);
        }

        private void SetButtonColor(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(e.NewTextValue))
            {
                playerButton.BackgroundColor = Color.LightGray;
            }
            else
            {
                playerButton.BackgroundColor = Color.Accent;
            }
        }
    }
}