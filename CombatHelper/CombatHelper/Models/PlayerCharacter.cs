using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Models
{
    [Table("PlayerCharacters")]
    public class PlayerCharacter
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey(typeof(Campaign))]
        public int CampaignID { get; set; }
    }
}
