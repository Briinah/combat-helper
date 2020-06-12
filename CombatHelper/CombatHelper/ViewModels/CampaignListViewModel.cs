using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CombatHelper.ViewModels
{
    public class CampaignListViewModel
    {
        public async Task<List<CampaignViewModel>> GetCampaignList()
        {
            var list = await App.Database.Campaigns.Get();

            var result = new List<CampaignViewModel>();

            foreach (var c in list)
                result.Add(new CampaignViewModel(c));

            return result;
        }
    }
}
