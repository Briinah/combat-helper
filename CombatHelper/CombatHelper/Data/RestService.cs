﻿using CombatHelper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            Uri uri = new Uri(baseUrl + "monsters/?search=" + query);
            var result = new List<Result>();

            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                IList<JToken> results = json["results"].Children().ToList();

                foreach(var item in results)
                {
                    var monster = item.ToObject<Open5eMonster>();
                    result.Add(new Result(monster.slug, monster.name));
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
                JObject json = JObject.Parse(content);
                var data = json.ToObject<Open5eMonster>();

                var creature = new Creature()
                {
                    Name = data.name,
                    Slug = data.slug,
                    AC = data.armor_class,
                    HP = data.hit_points,
                    Strength = data.strength,
                    Dexterity = data.dexterity,
                    Constitution = data.constitution,
                    Intelligence = data.intelligence,
                    Wisdom = data.wisdom,
                    Charisma = data.charisma,
                    StrengthSave = data.strength_save,
                    DexteritySave = data.dexterity_save,
                    ConstitutionSave = data.constitution_save,
                    IntelligenceSave = data.intelligence_save,
                    WisdomSave = data.wisdom_save,
                    CharismaSave = data.charisma_save,
                    Fly = data.speed.fly,
                    Walk = data.speed.walk,
                    Swim = data.speed.swim,
                    Climb = data.speed.climb
                };

                return creature;
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
