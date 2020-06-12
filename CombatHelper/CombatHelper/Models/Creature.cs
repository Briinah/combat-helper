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
        public bool Friendly { get; set; }
        public int HP { get; set; }
        public int AC { get; set; }

        [ForeignKey(typeof(Encounter))]
        public int EncounterID { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Intelligence { get; set; }
        public int Charisma { get; set; }
        public string Info { get; set; }
    }
}
