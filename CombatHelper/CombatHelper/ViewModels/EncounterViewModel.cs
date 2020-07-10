using CombatHelper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CombatHelper.ViewModels
{
    public class EncounterViewModel : BaseViewModel, IEquatable<EncounterViewModel>
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetValue(ref name, value);
            }
        }
        public ObservableCollection<CreatureViewModel> Creatures { get; private set; }
        public int CampaignId { get; set; }

        private int round = 1;
        public int Round
        {
            get { return round; }
            set { SetValue(ref round, value); }
        }

        private bool dataLoaded;
        private bool pcsAdded;

        public EncounterViewModel() 
        {
            Creatures = new ObservableCollection<CreatureViewModel>();
            dataLoaded = false;
        }

        public EncounterViewModel(Encounter encounter) 
        {
            Id = encounter.ID;
            Name = encounter.Name;
            CampaignId = encounter.CampaignID;
            Creatures = new ObservableCollection<CreatureViewModel>();
            dataLoaded = false;
        }

        public async Task LoadData()
        {
            if (!dataLoaded)
            {
                var creatures = await App.Database.Creatures.Get<bool>((c) => c.EncounterID == Id, null);
                foreach (var c in creatures)
                {
                    var vm = new CreatureViewModel(c);
                    SetNumber(vm);
                    Creatures.Add(vm);
                }
            }

            dataLoaded = true;
        }

        private void SetNumber(CreatureViewModel creature)
        {
            var count = Creatures.Count((c) => c.Name == creature.Name);
            creature.Number = count + 1;
        }

        public async Task ReloadData()
        {
            Creatures.Clear();
            dataLoaded = false;
            pcsAdded = false;
            await LoadData();
        }

        public async void AddPlayers()
        {
            if (!pcsAdded)
            {
                // add players to encounter if they are not added yet
                List<PlayerCharacter> players = await App.Database.Players.Get<bool>((pc) => pc.CampaignID == this.CampaignId, null);
                foreach (var pc in players)
                {
                    Creatures.Add(new CreatureViewModel(pc));
                }

                pcsAdded = true;
            }
        }

        public Encounter ToModel()
        {
            var encounter = new Encounter()
            {
                ID = this.Id,
                CampaignID = this.CampaignId,
                Name = this.Name
            };

            encounter.Creatures = new List<Creature>();

            foreach (var c in this.Creatures)
                encounter.Creatures.Add(c.ToModel());

            return encounter;
        }

        public async Task Save()
        {
            var encounter = ToModel();

            if (encounter.ID == 0)
            {
                // get id of encounter from db
                await App.Database.Encounters.Insert(encounter);
                this.Id = encounter.ID;
            }

            SaveCreatures();
            await App.Database.Encounters.UpdateWithChildren(encounter);
        }

        public async void Delete()
        {
            var encounter = ToModel();

            if (encounter.ID != 0)
            {
                RemoveCreatures();
                await App.Database.Encounters.Delete(encounter);
            }
        }

        private async void SaveCreatures()
        {
            foreach (var creature in Creatures.ToList()) // create a copy of the list, so it is not modified while looping
            {
                creature.EncounterId = Id;
                await creature.Save();
            }
        }

        private void RemoveCreatures()
        {
            foreach (var creature in Creatures)
            {
                creature.Delete();
            }
        }

        public async Task<bool> HasUnsavedChanges()
        {
            if (Id == 0)
                return true;

            var data = await App.Database.Encounters.GetWithChildren(Id);

            var dataVM = new EncounterViewModel(data);
            await dataVM.LoadData();

            return !this.Equals(dataVM);
        }

        public bool Equals(EncounterViewModel other)
        {
            if (other == null)
                return false;

            if (!string.Equals(Name, other.Name))
                return false;

            if (Creatures.Count != other.Creatures.Count)
                return false;

            for (int i = 0; i < Creatures.Count; i++)
            {
                if (!Creatures[i].Equals(other.Creatures[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
