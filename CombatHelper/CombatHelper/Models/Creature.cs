using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Models
{
    public class Creature
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Initiative { get; set; }
    }
}
