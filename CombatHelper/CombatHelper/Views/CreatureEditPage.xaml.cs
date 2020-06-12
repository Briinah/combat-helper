﻿using CombatHelper.Models;
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
    public partial class CreatureEditPage : ContentPage
    {
        private CreatureViewModel creature;
        public CreatureEditPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            creature = BindingContext as CreatureViewModel;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            creature.Save();
            await Navigation.PopAsync();
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void NextEntry(object sender, EventArgs e)
        {
            var entry = sender as Entry;
            var grid = (entry.Parent as Grid).Children;
            var index = grid.IndexOf(entry);
            if (index == -1)
                return;

            var next = grid.ElementAt(index + 2) as Entry; //skip label
            next?.Focus();
            // this way you do not have to remove the 0 before typing a new number
            if (next.Text == "0")
                next.Text = "";
        }
    }
}