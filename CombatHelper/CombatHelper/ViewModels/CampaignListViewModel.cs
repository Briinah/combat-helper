using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace CombatHelper.ViewModels
{
    public class CampaignListViewModel
    {
        public async Task<ObservableCollection<CampaignViewModel>> GetCampaignList()
        {
            var list = await App.Database.Campaigns.Get();

            var result = new ObservableCollection<CampaignViewModel>();

            foreach (var c in list)
            {
                var vm = new CampaignViewModel(c);
                await vm.LoadPlayerData();
                result.Add(vm);
            }

            return result;
        }
    }
}
