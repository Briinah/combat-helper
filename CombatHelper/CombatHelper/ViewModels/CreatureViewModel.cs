using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CombatHelper.ViewModels
{
    public class CreatureViewModel : BaseViewModel
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
        }

        public CreatureViewModel(PlayerCharacter pc)
        {
            Name = pc.Name;
            Initiative = 0;
            HP = 1;
            IsPC = true;
            HasTurn = false;
        }

        public Creature ToModel()
        {
            var c = new Creature()
            {
                ID = this.Id,
                EncounterID = this.EncounterId,
                HP = this.HP,
                Name = this.Name
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

        private int initiative;
        public int Initiative
        {
            get { return initiative; }
            set { SetValue(ref initiative, value); }
        }

        public bool IsDead
        {
            get { return HP == 0; }
        }

        private bool hasTurn = false;
        public bool HasTurn
        {
            get { return hasTurn; }
            set { SetValue(ref hasTurn, value); }
        }

        public Color ButtonColor
        {
            get
            {
                if (IsDead)
                    return Color.Gray;
                else if (IsPC)
                    return Color.LightGreen;
                else
                    return Color.HotPink;
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
    }
}
