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
    public partial class CampaignDetailPage : ContentPage
    {
        public CampaignDetailPage(bool removePreviousPage = false)
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var campaign = (Campaign)BindingContext;
            campaign = await App.Database.Campaigns.GetWithChildren(campaign.ID);
            playerList.ItemsSource = campaign.Players;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new CampaignEditPage
            {
                BindingContext = this.BindingContext
            }, this);

            await Navigation.PopAsync();
        }
    }
}