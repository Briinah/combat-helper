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
    public partial class CampaignList : ContentPage
    {
        public CampaignList()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            listView.ItemsSource = await App.Database.Campaigns.Get();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CampaignEditPage
            {
                BindingContext = new Campaign()
            });
        }

        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new CampaignDetailPage
                {
                    BindingContext = e.SelectedItem as Campaign
                });
            }
        }
    }
}