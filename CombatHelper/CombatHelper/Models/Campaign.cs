using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Models
{
    public class Campaign
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        [TextBlob("playersBlob")]
        public List<PlayerCharacter> Players { get; set; }
        public string playersBlob { get; set; }
        [TextBlob("encountersBlob")]
        public List<Encounter> Encounters { get; set; }
        public string encountersBlob { get; set; }
    }
}
