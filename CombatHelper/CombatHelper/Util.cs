using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper
{
    class Util
    {
        public static int GetModifier(int stat)
        {
            return (int)Math.Floor((stat - 10) / 2.0);
        }
    }
}
