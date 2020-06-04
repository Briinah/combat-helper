using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Models
{
    public class Encounter
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        [OneToMany]
        public List<Creature> Creatures { get; set; }
        [ForeignKey(typeof(Campaign))]
        public int CampaignID { get; set; }
    }
}
