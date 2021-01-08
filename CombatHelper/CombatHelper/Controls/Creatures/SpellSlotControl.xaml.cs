using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CombatHelper.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpellSlotControl : ContentView
    {
        public int[] TotalSlots
        {
            get { return (int[])GetValue(TotalSlotsProperty); }
            set { SetValue(TotalSlotsProperty, value); }
        }

        public static readonly BindableProperty TotalSlotsProperty = BindableProperty.Create("TotalSlots", typeof(int[]), typeof(SpellSlotControl), new int[0], propertyChanged: OnTotalSlotsChanged);

        private static void OnTotalSlotsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as SpellSlotControl;

            if (control.TotalSlots.All((slot) => slot.Equals(0)))
            {
                control.IsVisible = false;
                return;
            }

            control.slotContainer.Children.Clear();
            if (newValue != null)
            {
                control.PopulateSlots();
            }
        }

        private void PopulateSlots()
        {
            for (int i = 0; i < TotalSlots.Length; i++)
            {
                if (TotalSlots[i] == 0)
                    continue;

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
                    Padding = new Thickness(5, 0),
                    WidthRequest = 25
                };

                for (int j = 0; j < TotalSlots[i]; j++)
                {
                    var spellslot = new SpellSlot();
                    spellslot.Level = i + 1;
                    if (UsedSlots[i] > j)
                        spellslot.IsUsed = true;

                    spellslot.OnIsUsedChanged += SetSpellSlot;

                    stack.Children.Add(spellslot);
                }

                slotContainer.Children.Add(stack);
            }
        }


        public ObservableCollection<int> UsedSlots
        {
            get { return (ObservableCollection<int>)GetValue(UsedSlotsProperty); }
            set { SetValue(UsedSlotsProperty, value); }
        }

        public static readonly BindableProperty UsedSlotsProperty = BindableProperty.Create("UsedSlots", typeof(ObservableCollection<int>), typeof(SpellSlotControl), propertyChanged: UsedSlotsChanged);

        private static void UsedSlotsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as SpellSlotControl;
            if (newValue != null && oldValue != newValue)
            {
                control.UsedSlots.CollectionChanged += control.UsedSlots_CollectionChanged;
            }
        }

        private void SetSpellSlot(object sender, bool value)
        {
            var spellslot = sender as SpellSlot;

            if (value)
                UsedSlots[spellslot.Level - 1]++;
            else
                UsedSlots[spellslot.Level - 1]--;

            Console.WriteLine($"slots lvl {spellslot.Level}: {UsedSlots[spellslot.Level - 1]}");
        }

        public SpellSlotControl()
        {
            InitializeComponent();
        }

        private void UsedSlots_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("collection changed: " + e.NewItems);
            slotContainer.Children.Clear();
            PopulateSlots();
        }
    }
}