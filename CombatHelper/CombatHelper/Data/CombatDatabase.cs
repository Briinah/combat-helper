using CombatHelper.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CombatHelper.Data
{
    public class CombatDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public Repository<PlayerCharacter> Players { get; set; }
        public Repository<Campaign> Campaigns { get; set; }
        //public Repository<Encounter> Encounters { get; set; }

        public CombatDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath); 
            _database.CreateTableAsync<Campaign>().Wait();
            _database.CreateTableAsync<PlayerCharacter>().Wait();
            //_database.CreateTableAsync<Encounter>().Wait();

            // create repositories
            Players = new Repository<PlayerCharacter>(_database);
            Campaigns = new Repository<Campaign>(_database);
            //Encounters = new Repository<Encounter>(_database);
        }
    }
}
