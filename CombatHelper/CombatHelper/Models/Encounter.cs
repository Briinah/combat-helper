using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.Models
{
    public class Encounter
    {
        public int ID { get; set; }
        public List<Creature> Creatures { get; set; }
    }
}
