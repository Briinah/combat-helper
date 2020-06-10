using CombatHelper.Models;
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
    public partial class EncounterEditPage : ContentPage
    {
        private EncounterViewModel encounter;

        public EncounterEditPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            encounter = BindingContext as EncounterViewModel;
            Title = "New Encounter";
            if (encounter.Id != 0)
            {
                await encounter.ReloadData();
                Title = $"Edit: {encounter.Name}";
            }

            creatureList.ItemsSource = encounter.Creatures;
        }

        protected override bool OnBackButtonPressed()
        {
            if (encounter.Id != 0)
            {
                Navigation.InsertPageBefore(new EncounterDetailPage()
                {
                    BindingContext = encounter
                }, this);
            }

            Navigation.PopAsync();
            return true;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            encounter.Save();

            Navigation.InsertPageBefore(new EncounterDetailPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (await OnAlertYesNoClicked(encounter.Name))
            {
                encounter.Delete();
                await Navigation.PopAsync();
            }
        }


        private void RemoveCreature(object sender, EventArgs e)
        {
            var creature = (CreatureViewModel)((ImageButton)sender).BindingContext;
            encounter.Creatures.Remove(creature);
        }

        private async Task<bool> OnAlertYesNoClicked(string encounterName)
        {
            bool answer = await (DisplayAlert("Delete Encounter", $"Are you sure you want to delete {encounterName}?", "Yes", "No"));

            return answer;
        }

        private async void AddCreature(object sender, EventArgs e)
        {
            var creature = new CreatureViewModel()
            {
                EncounterId = encounter.Id
            };

            await Navigation.PushAsync(new CreatureEditPage()
            {
                BindingContext = creature
            });
        }

        private async void EditCreature(object sender, ItemTappedEventArgs e)
        {
            if (await encounter.HasUnsavedChanges())
            {
                if (await SaveChangesDialog())
                {
                    encounter.Save();
                    return;
                }
                else
                    return;
            }

            var creature = e.Item as CreatureViewModel;

            await Navigation.PushAsync(new CreatureEditPage()
            {
                BindingContext = creature
            });
        }

        private async Task<bool> SaveChangesDialog()
        {
            bool answer = await (DisplayAlert("Unsaved changes", "Do you want to save changes?", "Yes", "No"));

            return answer;
        }

        private void CopyCreature(object sender, EventArgs e)
        {
            var creature = ((ImageButton)sender).BindingContext as CreatureViewModel;

            var copy = new CreatureViewModel()
            {
                Name = creature.Name + "(copy)",
                HP = creature.HP,
                EncounterId = creature.EncounterId,
                Strength = creature.Strength,
                Dexterity = creature.Dexterity,
                Constitution = creature.Constitution,
                Intelligence = creature.Intelligence,
                Wisdom = creature.Wisdom,
                Charisma = creature.Charisma,
                Info = creature.Info
            };

            encounter.Creatures.Add(copy);
        }
    }
}