using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Data
{
    [Serializable]
    public class Open5eMonster
    {
        // json is lowercase
        public string slug { get; set; }
        public string name { get; set; }
        public string size { get; set; }
        public int armor_class { get; set; }
        public int hit_points { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int charisma { get; set; }
        public Open5eSpeed speed { get; set; }
        public Open5eSkills skills { get; set; }
        public Open5eAction[] actions { get; set; }
        public Open5eAction[] special_abilities { get; set; }
        public Open5eAction[] legendary_actions { get; set; }
        public string[] spell_list { get; set; }

    }

    [Serializable]
    public class Open5eSpeed
    { 
        public int walk { get; set; }
        public int fly { get; set; }
        public int swim { get; set; }
    }

    [Serializable]
    public class Open5eSkills
    {
        public int perception { get; set; }
        public int stealth { get; set; }
        public int athletics { get; set; }
        public int intimidation { get; set; }
    }

}
