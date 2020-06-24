using CombatHelper.ViewModels;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class HealthPopup : PopupPage
    {
        private CreatureViewModel creature;
        private bool healing;

        public HealthPopup()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            creature = BindingContext as CreatureViewModel;
            amount.Focus();
        }

        private void SetHealing(object sender, CheckedChangedEventArgs e)
        {
            healing = e.Value;
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnOK(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(amount.Text))
            {
                if (healing)
                    creature.HP += int.Parse(amount.Text);
                else
                    creature.HP -= int.Parse(amount.Text);

                if (creature.HP < 0)
                    creature.HP = 0;
            }

            await PopupNavigation.Instance.PopAsync();
        }
    }
}