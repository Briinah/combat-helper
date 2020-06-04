using CombatHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CombatHelper.ViewModels
{
    public class CreatureViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int EncounterId { get; set; }

        public CreatureViewModel() { }

        public CreatureViewModel(Creature creature)
        {
            Id = creature.ID;
            EncounterId = creature.EncounterID;
            Name = creature.Name;
            HP = creature.HP;
            Initiative = 0;
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
            set { SetValue(ref hp, value); }
        }

        private int initiative;
        public int Initiative 
        { 
            get { return initiative; }
            set { SetValue(ref initiative, value); }
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
