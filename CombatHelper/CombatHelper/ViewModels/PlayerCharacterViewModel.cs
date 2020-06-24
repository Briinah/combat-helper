using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task Save()
        {
            var pcModel = ToModel();
            if (pcModel.ID == 0)
            {
                await App.Database.Players.Insert(pcModel);
                Id = pcModel.ID;
            }
            await App.Database.Players.Update(pcModel);
        }

        public async void Delete()
        {
            var pc = ToModel();

            if (pc.ID != 0)
                await App.Database.Players.Delete(pc);
        }
    }
}
