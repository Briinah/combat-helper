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
    public partial class CampaignDetailPage : ContentPage
    {
        CampaignViewModel campaign;

        public CampaignDetailPage(bool removePreviousPage = false)
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            campaign = BindingContext as CampaignViewModel;
            await campaign.LoadData();
            playerList.ItemsSource = campaign.Players;
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new CampaignEditPage()
            {
                BindingContext = this.BindingContext
            }, this);

            await Navigation.PopAsync();
        }

        private async void ManageEncounters(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EncounterList()
            {
                BindingContext = this.BindingContext
            });
        }
    }
}