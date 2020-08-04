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
        public string Slug { get; set; }
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
        public int? StrengthSave { get; set; }
        public int? DexteritySave { get; set; }
        public int? ConstitutionSave { get; set; }
        public int? WisdomSave { get; set; }
        public int? IntelligenceSave { get; set; }
        public int? CharismaSave { get; set; }
        public string Info { get; set; }
        public int Walk { get; set; }
        public int Swim { get; set; }
        public int Fly { get; set; }
        public int Climb { get; set; }
    }

}
