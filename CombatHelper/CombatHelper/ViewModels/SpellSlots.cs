using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CombatHelper.ViewModels
{
    public class SpellSlots
    {
        public int[] SlotNumber { get; private set; }

        public ObservableCollection<int> UsedSlots { get; private set; }

        public SpellSlots(string slots) : this(ParseFromString(slots))
        { }

        public SpellSlots(int[] slots = null)
        {
            if (slots != null && slots.Length <= 9)
                SlotNumber = slots;
            else
                SlotNumber = new int[9];

            UsedSlots = new ObservableCollection<int>();
            for (int i = 0; i < SlotNumber.Length; i++)
                UsedSlots.Add(0);
        }

        private static int[] ParseFromString(string slots)
        {
            string[] separate = slots.Split(',');
            try
            {
                int[] numbers = separate.Select(s => int.Parse(s)).ToArray();
                return numbers;
            }
            catch (Exception e)
            {
                Console.WriteLine("slot string has incorrect format " + slots);
                Crashes.TrackError(e);
            }

            return null;
        }

        public string GetSlotNumberAsString()
        {
            return string.Join(",", SlotNumber);
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
