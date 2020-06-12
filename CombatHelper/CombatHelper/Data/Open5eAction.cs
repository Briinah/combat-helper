using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Data
{
    [Serializable]
    public class Open5eAction
    {
        public int attack_bonus { get; set; }
        public int damage_bonus { get; set; }
        public string desc { get; set; }
        public string damage_dice { get; set; }
        public string name { get; set; }
    }
}
