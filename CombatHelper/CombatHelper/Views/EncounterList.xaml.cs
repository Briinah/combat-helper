﻿using CombatHelper.Models;
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
    public partial class EncounterList : ContentPage
    {
        private CampaignViewModel campaign;

        public EncounterList()
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

            await Navigation.PushAsync(new EncounterEditPage()
            {
                BindingContext = encounter
            });
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CampaignEditPage()
            {
                BindingContext = this.BindingContext
            });
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