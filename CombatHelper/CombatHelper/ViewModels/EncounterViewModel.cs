using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatHelper.ViewModels
{
    class EncounterViewModel : BaseViewModel
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

        public int Round { get; set; } = 1;

        private bool dataLoaded;
        private bool pcsAdded;

        public EncounterViewModel() { }

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
                    Creatures.Add(new CreatureViewModel(c));
            }

            dataLoaded = true;
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

        public async void Save()
        {
            var encounter = ToModel();

            if (encounter.ID == 0)
            {
                // get id of encounter from db
                await App.Database.Encounters.Insert(encounter);
            }

            SaveCreatures(encounter);
            await App.Database.Encounters.UpdateWithChildren(encounter);
        }

        public async void Delete()
        {
            var encounter = ToModel();

            if (encounter.ID != 0)
            {
                RemoveCreatures(encounter);
                await App.Database.Encounters.Delete(encounter);
            }
        }

        private async void SaveCreatures(Encounter encounter)
        {
            foreach (var creature in encounter.Creatures)
            {
                creature.EncounterID = encounter.ID;

                if (creature.ID == 0)
                    await App.Database.Creatures.Insert(creature);
                else
                    await App.Database.Creatures.Update(creature);
            }
        }

        private async void RemoveCreatures(Encounter encounter)
        {
            foreach (var creature in encounter.Creatures)
            {
                if (creature.ID != 0)
                    await App.Database.Creatures.Delete(creature);
            }
        }
    }
}
