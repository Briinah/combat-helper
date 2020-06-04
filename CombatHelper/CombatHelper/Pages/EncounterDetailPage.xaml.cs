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
    public partial class EncounterDetailPage : ContentPage
    {
        Encounter encounter;

        public EncounterDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            encounter = (Encounter)BindingContext;
            Title = encounter.Name;
        }
        private async void OnEditClicked(object sender, EventArgs e)
        {
            Navigation.InsertPageBefore(new EncounterEditPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
        }
    }
}