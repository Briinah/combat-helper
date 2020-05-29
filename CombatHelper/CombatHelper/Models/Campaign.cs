using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Models
{
    [Table("Campaigns")]
    public class Campaign
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        [OneToMany]
        public List<PlayerCharacter> Players { get; set; }
        //[OneToMany]
        //public List<Encounter> Encounters { get; set; }
    }
}
