using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatHelper.ViewModels
{
    public class CampaignViewModel : BaseViewModel
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

        public ObservableCollection<EncounterViewModel> Encounters { get; private set; }

        public ObservableCollection<PlayerCharacterViewModel> Players { get; private set; }

        public CampaignViewModel() 
        {
            Encounters = new ObservableCollection<EncounterViewModel>();
            Players = new ObservableCollection<PlayerCharacterViewModel>();
        }
        public CampaignViewModel(Campaign campaign)
        {
            Id = campaign.ID;
            Name = campaign.Name;
            Encounters = new ObservableCollection<EncounterViewModel>();
            Players = new ObservableCollection<PlayerCharacterViewModel>();
        }

        public Campaign ToModel()
        {
            var campaign = new Campaign
            {
                ID = Id,
                Name = this.Name
            };

            campaign.Encounters = new List<Encounter>();
            foreach (var e in this.Encounters)
                campaign.Encounters.Add(e.ToModel());

            campaign.Players = new List<PlayerCharacter>();
            foreach (var pc in this.Players)
                campaign.Players.Add(pc.ToModel());

            return campaign;
        }

        public async Task LoadData()
        {
            Players.Clear();
            var players = await App.Database.Players.Get<bool>((pc) => pc.CampaignID == Id, null);
            foreach (var pc in players)
                Players.Add(new PlayerCharacterViewModel(pc));

            Encounters.Clear();
            var encounters = await App.Database.Encounters.Get<bool>((e) => e.CampaignID == Id, null);
            foreach (var e in encounters)
                Encounters.Add(new EncounterViewModel(e));
        }

        public async void Save()
        {
            var campaign = ToModel();
            if (campaign.ID == 0)
            {
                // get id of campaign from db
                await App.Database.Campaigns.Insert(campaign);
                this.Id = campaign.ID;
            }

            SavePlayers(campaign);
            // update campaign with players
            await App.Database.Campaigns.UpdateWithChildren(campaign);
        }

        public async void Delete()
        {
            var campaign = ToModel();

            RemovePlayers(campaign);

            if (campaign.ID != 0)
            {
                await App.Database.Campaigns.Delete(campaign);
            }
        }

        private async void SavePlayers(Campaign campaign)
        {
            foreach (var pc in campaign.Players)
            {
                pc.CampaignID = campaign.ID;

                if (pc.ID == 0)
                    await App.Database.Players.Insert(pc);
                else
                    await App.Database.Players.Update(pc);
            }
        }

        private async void RemovePlayers(Campaign campaign)
        {
            foreach (var pc in campaign.Players)
            {
                if (pc.ID != 0)
                    await App.Database.Players.Delete(pc);
            }
        }
    }
}
