using CombatHelper.ViewModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CombatHelper.Helpers
{
    public class ResourceManager
    {
        private readonly string path;
        private const string encounterDirectory = "Encounters";

        public ResourceManager(string path)
        {
            this.path = path;

            if(!Directory.Exists(Path.Combine(path, encounterDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(path, encounterDirectory));
            }
        }

        public bool EncounterExists(int id)
        {
            return File.Exists(Path.Combine(path, encounterDirectory, GetEncounterFileName(id)));
        }

        public EncounterViewModel GetEncounter(int id)
        {
            var filePath = Path.Combine(path, encounterDirectory, GetEncounterFileName(id));

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    var json = reader.ReadToEnd();

                    var encounter = JsonConvert.DeserializeObject<EncounterViewModel>(json);

                    return encounter;
                }
            }
            catch(IOException e)
            {
                Crashes.TrackError(e);
                return null;
            }
        }

        public bool SaveEncounter(EncounterViewModel encounter)
        {
            string json = encounter.ToJson();

            var fileName = GetEncounterFileName(encounter.Id);
            var filePath = Path.Combine(path, encounterDirectory, fileName);
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                try
                {
                    writer.Write(json);
                }
                catch (IOException e)
                {
                    Crashes.TrackError(e);
                    return false;
                }

            }

            return true;
        }

        public bool RemoveEncounter(EncounterViewModel encounter)
        {
            var filePath = Path.Combine(path, encounterDirectory, GetEncounterFileName(encounter.Id));

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (IOException e)
                {
                    Crashes.TrackError(e);
                    return false;
                }
            }

            return true;
        }

        private string GetEncounterFileName(int id)
        {
            return "encounter_" + id + ".json";
        }
    }
}
