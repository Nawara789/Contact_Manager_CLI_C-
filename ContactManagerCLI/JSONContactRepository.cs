using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ContactManagerCLI
{
    public class JSONContactRepository : IContactRepository
    {
        public readonly string FilePath;
        public JSONContactRepository(string filePath)
        {
            FilePath = filePath;
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "{}");
            }
        }
        public Dictionary<int, Contact> Load()
        {
            Dictionary<int, Contact> contacts;
            string jsonString = File.ReadAllText(FilePath);
            contacts = JsonSerializer.Deserialize<Dictionary<int, Contact>>(jsonString);
            return contacts ?? new Dictionary<int, Contact>();
        }

        public void Save(Dictionary<int, Contact> contacts)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(contacts, options);
            File.WriteAllText(FilePath, jsonString);

        }
    }
}