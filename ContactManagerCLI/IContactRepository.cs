using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace ContactManagerCLI
{
    public interface IContactRepository
    {
        public Dictionary<int, Contact> Load();
        public void Save(Dictionary<int, Contact> mydictionary);
    }
}