using CombatHelper.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Helpers
{
    public static class Mechanics
    {
        public static string[] Conditions =
        {
            "Blinded",
            "Charmed",
            "Deafened",
            "Frightened",
            "Grappled",
            "Incapacitated",
            "Invisible",
            "Paralyzed",
            "Petrified",
            "Poisoned",
            "Prone",
            "Restrained",
            "Stunned",
            "Unconscious"
        };

        public static int GetModifier(int stat)
        {
            return (int)Math.Floor((stat - 10) / 2.0);
        }

        public static string GetAttributeString(this int attribute)
        {
            var mod = GetModifier(attribute);
            var sign = (mod >= 0) ? "+" : ""; 
            return $"{attribute} ({sign}{mod})";
        }

        public static int GetSavingTrow(int attribute, int? save)
        {
            if (save == null)
                return GetModifier(attribute);
            else
                return save.Value;
        }
    }
}
