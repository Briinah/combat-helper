using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CombatHelper.ViewModels
{
    public class CreatureViewModel : BaseViewModel, IEquatable<CreatureViewModel>
    {
        public int Id { get; set; }
        public int EncounterId { get; set; }
        public bool IsPC { get; private set; }

        public CreatureViewModel() { }

        public CreatureViewModel(Creature creature)
        {
            Id = creature.ID;
            EncounterId = creature.EncounterID;
            Name = creature.Name;
            HP = creature.HP;
            Initiative = 0;
            IsPC = false;
            HasTurn = false;
            Strength = creature.Strength;
            Dexterity = creature.Dexterity;
            Constitution = creature.Constitution;
            Intelligence = creature.Intelligence;
            Wisdom = creature.Wisdom;
            Charisma = creature.Charisma;
            Info = creature.Info;
            Friendly = creature.Friendly;
        }

        public CreatureViewModel(PlayerCharacter pc)
        {
            Name = pc.Name;
            Initiative = 0;
            HP = 1;
            IsPC = true;
            HasTurn = false;
            Friendly = true;
        }

        public Creature ToModel()
        {
            var c = new Creature()
            {
                ID = this.Id,
                EncounterID = this.EncounterId,
                HP = this.HP,
                Name = this.Name,
                Strength = this.Strength,
                Dexterity = this.Dexterity,
                Constitution = this.Constitution,
                Intelligence = this.Intelligence,
                Wisdom = this.Wisdom,
                Charisma = this.Charisma,
                Info = this.Info,
                Friendly = this.Friendly
            };

            return c;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetValue(ref name, value); }
        }

        private int hp;
        public int HP
        {
            get { return hp; }
            set
            {
                SetValue(ref hp, value);
                OnPropertyChanged("IsDead");
                OnPropertyChanged("ButtonColor");
            }
        }


        private bool friendly;
        public bool Friendly
        {
            get { return friendly; }
            set
            {
                SetValue(ref friendly, value);
                OnPropertyChanged("ButtonColor");
            }
        }

        private int initiative;
        public int Initiative
        {
            get { return initiative; }
            set { SetValue(ref initiative, value); }
        }

        private int strength;
        public int Strength
        {
            get { return strength; }
            set { SetValue(ref strength, value); }
        }
        private int dexterity;
        public int Dexterity
        {
            get { return dexterity; }
            set { SetValue(ref dexterity, value); }
        }
        private int constitution;
        public int Constitution
        {
            get { return constitution; }
            set { SetValue(ref constitution, value); }
        }
        private int intelligence;
        public int Intelligence
        {
            get { return intelligence; }
            set { SetValue(ref intelligence, value); }
        }
        private int wisdom;
        public int Wisdom
        {
            get { return wisdom; }
            set { SetValue(ref wisdom, value); }
        }
        private int charisma;
        public int Charisma
        {
            get { return charisma; }
            set { SetValue(ref charisma, value); }
        }

        private string info;
        public string Info
        {
            get { return info; }
            set { SetValue(ref info, value); }
        }

        public bool IsDead
        {
            get { return HP == 0; }
        }

        private bool hasTurn = false;
        public bool HasTurn
        {
            get { return hasTurn; }
            set
            {
                SetValue(ref hasTurn, value);
                OnPropertyChanged("ShowInfo");
            }
        }

        public bool ShowInfo
        {
            get { return HasTurn && !IsPC; }
        }

        public Color ButtonColor
        {
            get
            {
                if (IsDead)
                    return Color.Gray;
                else if (IsPC || Friendly)
                    return ColorConverters.FromHex("#64d9c6");
                else
                    return ColorConverters.FromHex("#d96464");
            }
        }

        public async void Save()
        {
            var creature = ToModel();
            if (creature.ID == 0)
            {
                // get id of encounter from db
                await App.Database.Creatures.Insert(creature);
            }

            await App.Database.Creatures.Update(creature);
        }

        public bool Equals(CreatureViewModel other)
        {
            if (other == null)
                return false;

            if (!string.Equals(Name, other.Name))
                return false;

            if (HP != other.HP)
                return false;

            if (Friendly != other.Friendly)
                return false;

            if (!string.Equals(Info, other.Info))
                return false;

            return Strength == other.Strength &&
                Dexterity == other.Dexterity &&
                Constitution == other.Constitution &&
                Intelligence == other.Intelligence &&
                Wisdom == other.Wisdom &&
                Charisma == other.Charisma;

        }

        public static int CompareInitiative(CreatureViewModel a, CreatureViewModel b)
        {
            return b.Initiative.CompareTo(a.Initiative);
        }

        public static CreatureViewModel Copy(CreatureViewModel vm)
        {
                var copy = new CreatureViewModel()
                {
                    Name = vm.Name,
                    HP = vm.HP,
                    EncounterId = vm.EncounterId,
                    Strength = vm.Strength,
                    Dexterity = vm.Dexterity,
                    Constitution = vm.Constitution,
                    Intelligence = vm.Intelligence,
                    Wisdom = vm.Wisdom,
                    Charisma = vm.Charisma,
                    Info = vm.Info,
                    Friendly = vm.Friendly,
                    Initiative = vm.Initiative
                };

            return copy;
        }
    }
}
