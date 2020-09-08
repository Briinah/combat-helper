using CombatHelper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpellSlotInput : ContentView
    {
        private readonly int[] totalSlots = { 4, 3, 3, 3, 3, 2, 2, 1, 1 };

        public int[] SpellSlotNumber
        {
            get { return (int[])GetValue(SpellSlotNumberProperty); }
            set { SetValue(SpellSlotNumberProperty, value); }
        }

        public static readonly BindableProperty SpellSlotNumberProperty = BindableProperty.Create("SpellSlots", typeof(int[]), typeof(SpellSlot), propertyChanged: OnSpellSlotsChanged);

        private static void OnSpellSlotsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as SpellSlotInput;
            control.slotContainer.Children.Clear();
            if (newValue != null)
            {
                control.PopulateSlots();
            }

        }

        public void PopulateSlots()
        {
            for (int i = 0; i < totalSlots.Length; i++)
            {
                var stack = new StackLayout()
                {
                    Children =
                    {
                        new Label
                        {
                            Text = (i + 1).ToString(),
                            HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, false)
                        }
                    },
                    Padding = new Thickness(5, 0)
                };

                for (int j = 0; j < totalSlots[i]; j++)
                {
                    var spellslot = new SpellSlot();
                    spellslot.Level = i + 1;

                    if (SpellSlotNumber[i] > j)
                        spellslot.IsUsed = true;

                    spellslot.OnIsUsedChanged += SetSpelslot;

                    stack.Children.Add(spellslot);
                }

                slotContainer.Children.Add(stack);
            }
        }

        private void SetSpelslot(object source, bool value)
        {
            var spellslot = source as SpellSlot;

            if (value)
                SpellSlotNumber[spellslot.Level - 1]++;
            else
                SpellSlotNumber[spellslot.Level - 1]--;

            Console.WriteLine($"slots lvl {spellslot.Level}: {SpellSlotNumber[spellslot.Level - 1]}");
        }

        public SpellSlotInput()
        {
            InitializeComponent();
        }


    }
}