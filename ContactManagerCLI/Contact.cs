using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerCLI
{
    public class Contact
    {
        public int Id {get; init;}
        public string Name {get; set;}
        public string Phone {get; set;}
        public string Email {get; set;}
        public DateTime CreationDate {get; init;}
        public Contact(int id,
                       string name,
                       string phone, 
                       string email,
                       DateTime creationDate)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            CreationDate = creationDate;
        }
    }
}