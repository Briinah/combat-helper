using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CombatHelper.Models
{
    public class SpellSlots
    {
        public int[] SlotNumber { get; private set; }

        public ObservableCollection<bool>[] UsedSlots { get; private set; }

        public SpellSlots(int[] slots = null)
        {
            if (slots != null && slots.Length <= 9)
                SlotNumber = slots;
            else
                SlotNumber = new int[9];

            UsedSlots = new ObservableCollection<bool>[SlotNumber.Length];
            for(int i = 0; i < UsedSlots.Length; i++)
            {
                UsedSlots[i] = new ObservableCollection<bool>();

                for (int j = 0; j < SlotNumber[i]; j++)
                    UsedSlots[i].Add(false);
            }
        }

        public void SetLevel(int level, int number)
        {
            if (level < 0 || level >= SlotNumber.Length)
                return;

            SlotNumber[level - 1] = number;
        }

        public int GetLevel(int level)
        {
            if (level < 0 || level >= SlotNumber.Length)
                return 0;

            return SlotNumber[level - 1];
        }
    }
}
