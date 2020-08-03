using CombatHelper.Helpers;
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

namespace CombatHelper.Views.Encounter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConditionSelectPopup : PopupPage
    {
        private CreatureViewModel creature;

        public ConditionSelectPopup()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            creature = BindingContext as CreatureViewModel;

            collectionView.ItemsSource = Mechanics.Conditions;
            collectionView.SelectedItems = creature.Conditions.ToList<object>();
        }
        private async void ClosePopup(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            creature.Conditions.Clear();
            foreach (var c in e.CurrentSelection)
            {
                creature.Conditions.Add(c.ToString());
            }
        }
    }
}