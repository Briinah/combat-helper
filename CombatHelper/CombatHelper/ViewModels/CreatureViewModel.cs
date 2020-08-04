using CombatHelper.Helpers;
using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace CombatHelper.ViewModels
{
    public class CreatureViewModel : BaseViewModel, IEquatable<CreatureViewModel>
    {
        public int Id { get; set; }
        public int EncounterId { get; set; }
        public bool IsPC { get; set; }

        public CreatureViewModel()
        {
            Speed = new Speed();
            FillConditions();
        }

        public CreatureViewModel(Creature creature)
        {
            Id = creature.ID;
            EncounterId = creature.EncounterID;
            Name = creature.Name;
            Slug = creature.Slug;
            HP = creature.HP;
            AC = creature.AC;
            Initiative = 0;
            IsPC = false;
            HasTurn = false;
            Strength = creature.Strength;
            Dexterity = creature.Dexterity;
            Constitution = creature.Constitution;
            Intelligence = creature.Intelligence;
            Wisdom = creature.Wisdom;
            Charisma = creature.Charisma;
            StrengthSave = creature.StrengthSave;
            DexteritySave = creature.DexteritySave;
            ConstitutionSave = creature.ConstitutionSave;
            IntelligenceSave = creature.IntelligenceSave;
            WisdomSave = creature.WisdomSave;
            CharismaSave = creature.CharismaSave;
            Info = creature.Info;
            Friendly = creature.Friendly;
            Speed = new Speed
            {
                Walk = creature.Walk,
                Fly = creature.Fly,
                Climb = creature.Climb,
                Swim = creature.Swim
            };
            FillConditions();
        }

        public CreatureViewModel(PlayerCharacter pc)
        {
            Name = pc.Name;
            Initiative = 0;
            HP = 1;
            IsPC = true;
            HasTurn = false;
            Friendly = true;

            Speed = new Speed();
            FillConditions();
        }

        public Creature ToModel()
        {
            var c = new Creature()
            {
                ID = this.Id,
                EncounterID = this.EncounterId,
                HP = this.HP,
                AC = this.AC,
                Name = this.Name,
                Slug = this.Slug,
                Strength = this.Strength,
                Dexterity = this.Dexterity,
                Constitution = this.Constitution,
                Intelligence = this.Intelligence,
                Wisdom = this.Wisdom,
                Charisma = this.Charisma,
                StrengthSave = this.StrengthSave,
                DexteritySave = this.DexteritySave,
                ConstitutionSave = this.ConstitutionSave,
                IntelligenceSave = this.IntelligenceSave,
                WisdomSave = this.WisdomSave,
                CharismaSave = this.CharismaSave,
                Info = this.Info,
                Friendly = this.Friendly,
                Walk = this.Speed.Walk,
                Fly = this.Speed.Fly,
                Climb = this.Speed.Climb,
                Swim = this.Speed.Swim
            };

            return c;
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetValue(ref name, value);
                OnPropertyChanged("DisplayName");
            }
        }

        // if multiple similar creatures are in the same encounter
        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                SetValue(ref number, value);
                OnPropertyChanged("DisplayName");
            }
        }

        public string DisplayName
        {
            get
            {
                if (number > 1)
                    return Name + $" ({Number})";
                else
                    return Name;
            }
        }
        private string slug;
        public string Slug
        {
            get { return slug; }
            set
            {
                SetValue(ref slug, value);
                OnPropertyChanged("HasSourceText");
                OnPropertyChanged("WebUrl");
            }
        }

        public string WebUrl
        {
            get { return "https://open5e.com/monsters/" + Slug; }
        }

        public bool HasSourceText
        {
            get { return !string.IsNullOrEmpty(Slug); }
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

        private int ac;
        public int AC
        {
            get { return ac; }
            set { SetValue(ref ac, value); }
        }

        private Speed speed;
        public Speed Speed
        {
            get { return speed; }
            set { SetValue(ref speed, value); }
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

        public string GetModString(int modifier)
        {
            if (modifier >= 0)
                return "+" + modifier;
            else
                return modifier.ToString();
        }

        #region attributes
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

        private int? strengthSave;
        public int? StrengthSave
        {
            get { return strengthSave; }
            set
            {
                SetValue(ref strengthSave, value);
                OnPropertyChanged("HasSaves");
            }
        }
        private int? dexteritySave;
        public int? DexteritySave
        {
            get { return dexteritySave; }
            set
            {
                SetValue(ref dexteritySave, value);
                OnPropertyChanged("HasSaves");
            }
        }
        private int? constitutionSave;
        public int? ConstitutionSave
        {
            get { return constitutionSave; }
            set
            {
                SetValue(ref constitutionSave, value);
                OnPropertyChanged("HasSaves");
            }
        }
        private int? intelligenceSave;
        public int? IntelligenceSave
        {
            get { return intelligenceSave; }
            set
            {
                SetValue(ref intelligenceSave, value);
                OnPropertyChanged("HasSaves");
            }
        }
        private int? wisdomSave;
        public int? WisdomSave
        {
            get { return wisdomSave; }
            set
            {
                SetValue(ref wisdomSave, value);
                OnPropertyChanged("HasSaves");
            }
        }
        private int? charismaSave;
        public int? CharismaSave
        {
            get { return charismaSave; }
            set
            {
                SetValue(ref charismaSave, value);
                OnPropertyChanged("HasSaves");
            }
        }
        #endregion

        public bool HasSaves
        {
            get
            {
                return
                    StrengthSave != null ||
                    DexteritySave != null ||
                    ConstitutionSave != null ||
                    IntelligenceSave != null ||
                    WisdomSave != null ||
                    CharismaSave != null;
            }
        }
        private string info;
        public string Info
        {
            get { return info; }
            set { SetValue(ref info, value); }
        }

        public bool IsDead
        {
            get { return HP <= 0; }
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
            get { return HasTurn; }
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

        public ObservableCollection<object> Conditions; // required to be object by collectionview

        public string ConditionString
        {
            get
            {
                return string.Join(", ", Conditions);
            }
        }

        public bool ShowConditionString
        {
            get
            {
                return ConditionString.Length > 0;
            }
        }

        private void FillConditions()
        {
            Conditions = new ObservableCollection<object>();
            Conditions.CollectionChanged += Conditions_CollectionChanged;
        }

        private bool conditionRefRestored = false;

        /// <summary>
        /// Use after reloading a creature via json (resourcemanager). The references to the mechanics list are lost and cause the collectionview to not preselect the values. 
        /// This fixes that. 
        /// </summary>
        /// <param name="valuesChanged"></param>
        /// <returns></returns>
        public ObservableCollection<object> GetConditionReference(out bool valuesChanged)
        {
            if (conditionRefRestored)
            {
                valuesChanged = false;
                return Conditions;
            }

            var indices = new List<int>();

            foreach (var c in Conditions)
            {
                indices.Add(Mechanics.Conditions.IndexOf(c));
            }

            var references = new ObservableCollection<object>();

            foreach (var i in indices)
            {
                references.Add(Mechanics.Conditions[i]);
            }

            conditionRefRestored = true;
            valuesChanged = true;
            return references;
        }

        private void Conditions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("ConditionString");
            OnPropertyChanged("ShowConditionString");
        }

        public async Task Save()
        {
            var creature = ToModel();
            if (creature.ID == 0)
            {
                // get id of encounter from db
                await App.Database.Creatures.Insert(creature);
                Id = creature.ID;
            }

            await App.Database.Creatures.Update(creature);
        }

        public async void Delete()
        {
            var creature = ToModel();

            if (creature.ID != 0)
                await App.Database.Creatures.Delete(creature);
        }

        public bool Equals(CreatureViewModel other)
        {
            if (other == null)
                return false;

            return string.Equals(Name, other.Name) &&
                   string.Equals(Slug, other.Slug) &&
                   Speed.Equals(other.Speed) &&
                   Number == other.Number &&
                   HP == other.HP &&
                   AC == other.AC &&
                   Friendly == other.Friendly &&
                   string.Equals(Info, other.Info) &&
                   Strength == other.Strength &&
                   Dexterity == other.Dexterity &&
                   Constitution == other.Constitution &&
                   Intelligence == other.Intelligence &&
                   Wisdom == other.Wisdom &&
                   Charisma == other.Charisma;

        }

        public static int CompareInitiative(CreatureViewModel a, CreatureViewModel b)
        {
            // compare b to a, because the highest comes first
            var compareInitiative = b.Initiative.CompareTo(a.Initiative);
            if (compareInitiative == 0)
                return b.Dexterity.CompareTo(a.Dexterity);
            return compareInitiative;
        }

        public static CreatureViewModel Copy(CreatureViewModel vm)
        {
            var copy = new CreatureViewModel()
            {
                Name = vm.Name,
                Slug = vm.Slug,
                HP = vm.HP,
                AC = vm.AC,
                EncounterId = vm.EncounterId,
                Strength = vm.Strength,
                Dexterity = vm.Dexterity,
                Constitution = vm.Constitution,
                Intelligence = vm.Intelligence,
                Wisdom = vm.Wisdom,
                Charisma = vm.Charisma,
                StrengthSave = vm.StrengthSave,
                DexteritySave = vm.DexteritySave,
                ConstitutionSave = vm.ConstitutionSave,
                IntelligenceSave = vm.IntelligenceSave,
                WisdomSave = vm.WisdomSave,
                CharismaSave = vm.CharismaSave,
                Info = vm.Info,
                Friendly = vm.Friendly,
                Initiative = vm.Initiative,
                Speed = new Speed
                {
                    Walk = vm.Speed.Walk,
                    Fly = vm.Speed.Fly,
                    Climb = vm.Speed.Climb,
                    Swim = vm.Speed.Swim
                }
            };

            return copy;
        }

        public void FillFromData(Creature model)
        {
            Name = model.Name;
            Slug = model.Slug;
            HP = model.HP;
            AC = model.AC;
            Strength = model.Strength;
            Dexterity = model.Dexterity;
            Constitution = model.Constitution;
            Intelligence = model.Intelligence;
            Wisdom = model.Wisdom;
            Charisma = model.Charisma;
            StrengthSave = model.StrengthSave;
            DexteritySave = model.DexteritySave;
            ConstitutionSave = model.ConstitutionSave;
            IntelligenceSave = model.IntelligenceSave;
            WisdomSave = model.WisdomSave;
            CharismaSave = model.CharismaSave;
            Speed = new Speed
            {
                Fly = model.Fly,
                Walk = model.Walk,
                Swim = model.Swim,
                Climb = model.Climb
            };
        }
    }

    public class Speed : BaseViewModel, IEquatable<Speed>
    {
        private int walk;
        public int Walk
        {
            get { return walk; }
            set { SetValue(ref walk, value); }
        }

        private int fly;
        public int Fly
        {
            get { return fly; }
            set { SetValue(ref fly, value); }
        }

        private int swim;
        public int Swim
        {
            get { return swim; }
            set { SetValue(ref swim, value); }
        }

        private int climb;
        public int Climb
        {
            get { return climb; }
            set { SetValue(ref climb, value); }
        }

        public bool Equals(Speed other)
        {
            return Walk.Equals(other.Walk) && Fly.Equals(other.Fly) && Swim.Equals(other.Swim) && Climb.Equals(other.Climb);
        }
    }

}
