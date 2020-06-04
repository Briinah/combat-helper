using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Models
{
    public class Creature
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Initiative { get; set; }
        public int HP { get; set; }
        [ForeignKey(typeof(Encounter))]
        public int EncounterID { get; set; }
    }
}
