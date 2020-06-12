using CombatHelper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CombatHelper.Data
{
    public class RestService : IDisposable
    {
        private HttpClient client;

        private readonly string baseUrl = "https://api.open5e.com/";

        public RestService()
        {
            client = new HttpClient();
        }

        public async Task<List<Result>> GetSearchResults(string query)
        {
            Uri uri = new Uri(baseUrl + "monsters/?search=" + query + "&limit=5");
            var result = new List<Result>();

            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(content);

                foreach(var item in json["results"])
                {
                    var r = new Result(item["slug"].Value, item["name"].Value);
                    result.Add(r);
                }
            }

            return result;
        }

        public async Task<Creature> GetCreature(string id)
        {
            Uri uri = new Uri(baseUrl + "monsters/" + id);
            var response = await client.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                dynamic json = JsonConvert.DeserializeObject(content);

                try
                {
                    var creature = new Creature();
                    creature.Name = json["name"].Value;
                    creature.AC = (int)json["armor_class"].Value;
                    creature.HP = (int)json["hit_points"].Value;
                    creature.Strength = Util.GetModifier((int)json["strength"].Value);
                    creature.Dexterity = Util.GetModifier((int)json["dexterity"].Value);
                    creature.Constitution = Util.GetModifier((int)json["constitution"].Value);
                    creature.Intelligence = Util.GetModifier((int)json["intelligence"].Value);
                    creature.Wisdom = Util.GetModifier((int)json["wisdom"].Value);
                    creature.Charisma = Util.GetModifier((int)json["charisma"].Value);

                    return creature;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return null;
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public class Result
        {
            public string Id { get; private set; }
            public string Name { get; private set; }

            public Result(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}
