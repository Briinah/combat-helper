using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.ViewModels
{
    public class PlayerCharacterViewModel : BaseViewModel
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetValue(ref name, value); }
        }

        public int CampaignId { get; set; }

        public PlayerCharacterViewModel() { }
        public PlayerCharacterViewModel(PlayerCharacter pc)
        {
            Id = pc.ID;
            Name = pc.Name;
            CampaignId = pc.CampaignID;
        }

        public PlayerCharacter ToModel()
        {
            var player = new PlayerCharacter
            {
                ID = this.Id,
                Name = this.name,
                CampaignID = this.CampaignId
            };

            return player;
        }

        public async void Save()
        {
            var pc = ToModel();
            if (pc.ID == 0)
            {
                await App.Database.Players.Insert(pc);
                this.Id = pc.ID;
            }

            await App.Database.Players.Update(pc);
        }

        public async void Delete()
        {
            var pc = ToModel();

            if (pc.ID != 0)
                await App.Database.Players.Delete(pc);
        }
    }
}
