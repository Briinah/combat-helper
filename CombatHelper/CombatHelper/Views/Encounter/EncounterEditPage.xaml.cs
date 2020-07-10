using CombatHelper.Helpers;
using CombatHelper.ViewModels;
using System;
using System.Linq;
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

            encounter.Creatures.CollectionChanged += Creatures_CollectionChanged;
            creatureList.ItemsSource = encounter.Creatures;
        }

        protected override void OnDisappearing()
        {
            encounter.Creatures.CollectionChanged -= Creatures_CollectionChanged;
        }

        private void Creatures_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    SetNumber(item as CreatureViewModel);
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    var creature = item as CreatureViewModel;
                    var sameNameList = encounter.Creatures.Where((c) => c.Name == creature.Name).ToArray();
                    for (int i = 0; i < sameNameList.Length; i++)
                    {
                        sameNameList[i].Number = i + 1;
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (IsBusy) return false;

            IsBusy = true;
            if (encounter.Id != 0)
            {
                Navigation.InsertPageBefore(new EncounterDetailPage()
                {
                    BindingContext = encounter
                }, this);
            }

            Navigation.PopAsync();
            IsBusy = false;
            return true;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            OnBackButtonPressed();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            await encounter.Save();

            Navigation.InsertPageBefore(new EncounterDetailPage()
            {
                BindingContext = encounter
            }, this);

            await Navigation.PopAsync();
            IsBusy = false;
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (IsBusy) return;
            IsBusy = true;
            if (await OnAlertYesNoClicked(encounter.Name))
            {
                encounter.Delete();
                await Navigation.PopAsync();
            }
            IsBusy = false;
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
            if (IsBusy) return;

            IsBusy = true;
            if (await encounter.HasUnsavedChanges())
            {
                if (await SaveChangesDialog())
                {
                    await encounter.Save();
                    return;
                }
                else
                    return;
            }

            var creature = new CreatureViewModel()
            {
                EncounterId = encounter.Id
            };

            await Navigation.PushAsync(new CreatureEditPage()
            {
                BindingContext = creature
            });
            IsBusy = false;
        }

        private async void EditCreature(object sender, ItemTappedEventArgs e)
        {
            if (IsBusy) return;

            IsBusy = true;
            if (await encounter.HasUnsavedChanges())
            {
                if (await SaveChangesDialog())
                {
                    await encounter.Save();
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
            IsBusy = false;
        }

        private async Task<bool> SaveChangesDialog()
        {
            bool answer = await (DisplayAlert("Unsaved changes", "Do you want to save changes?", "Yes", "No"));

            return answer;
        }

        private void CopyCreature(object sender, EventArgs e)
        {
            var creature = ((ImageButton)sender).BindingContext as CreatureViewModel;
            var copy = CreatureViewModel.Copy(creature);
            encounter.Creatures.Add(copy);
        }

        private void SetNumber(CreatureViewModel creature)
        {
            var count = encounter.Creatures.Count((c) => c.Name == creature.Name);
            creature.Number = count;
        }
    }
}