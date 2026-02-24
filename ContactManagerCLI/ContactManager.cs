using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ContactManagerCLI
{
    public class ContactManager
    {
        private readonly IContactRepository _repository;
        private Dictionary<int, Contact> _contacts;
        //private Dictionary<string, List<int>> _nameIndex;//TODO: implement this later for instant search by name
        private int id;
        public ContactManager(IContactRepository repo)
        {
            _repository = repo;
        }

        public void Load()
        {
            _contacts = _repository.Load();

            id = _contacts.Any() ? _contacts.Keys.Max() + 1 : 1; // get last assigned id to increment it for new contacts
        }

        public void AddContact(string name, string phone, string email)
        {
            Contact newContact = new Contact(id++,name, phone, email,DateTime.Now);
            _contacts[newContact.Id] = newContact;
        }
        public void EditContact(int id, int field, string newField) // 1-name, 2-phone, 3-email
        {
            var contact = _contacts[id];
            if (field == 1)
            {
                contact.Name = newField;
            }
            else if (field == 2)
            {
                contact.Phone = newField;
            }
            else
            {
                contact.Email = newField;
            }
        }
        public bool DeleteContact(int id)
        {
            if (!_contacts.ContainsKey(id))
            {
                return false; // Nothing to delete
            }
            return _contacts.Remove(id);
        }
        public Contact? ViewContact(int id)
        {
            if (_contacts.TryGetValue(id, out var contact))
            {
                return contact;
            }
            return null;
        }
        public Dictionary<int, Contact> ListAllContacts()
        {
            return _contacts;
        }
        public Contact? Search(int id) 
        {
            return _contacts.TryGetValue(id, out var contact) ? contact : null;
        }
        //Filters
        public List<Contact>? Filter(string name = null, string phone = null, string email = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            List<Contact> matches = new List<Contact>();
            foreach (Contact contact in _contacts.Values)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (!contact.Name.Contains(name)) continue;
                } 
                if (!string.IsNullOrEmpty(phone))
                {
                    if (!contact.Phone.Contains(phone)) continue;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    if (!contact.Email.Contains(email)) continue;
                }
                if(fromDate.HasValue)
                {
                    if (contact.CreationDate < fromDate.Value)
                    {
                        continue; // too old, skip current contact
                    }
                }
                if(toDate.HasValue)
                {
                    if (contact.CreationDate > toDate.Value)
                    {
                        continue; // too new, skip current contact
                    }
                }

                matches.Add(contact); // add the survived contact from all contacts
            }
            return matches;
        }
    }
}