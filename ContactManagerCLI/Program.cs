using System;

namespace ContactManagerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new JSONContactRepository("contacts.json");
            var manager = new ContactManager(repo);

            manager.Load();

            bool running = true;
            while (running)
            {
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. Edit Contact");
                Console.WriteLine("3. Delete Contact");
                Console.WriteLine("4. View Contact");
                Console.WriteLine("5. List Contacts");
                Console.WriteLine("6. Search by Id");
                Console.WriteLine("7. Filter");
                Console.WriteLine("8. Save");
                Console.WriteLine("9. Exit");

                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Phone: ");
                        string phone = Console.ReadLine();
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        manager.AddContact(name, phone, email);
                        break;
                    case "2":
                        Console.Write("Id to edit: ");
                        int editId = int.Parse(Console.ReadLine());
                        Console.Write("Field to edit (1-Name, 2-Phone, 3-Email): ");
                        int field = int.Parse(Console.ReadLine());
                        Console.Write("New value: ");
                        string newValue = Console.ReadLine();
                        manager.EditContact(editId, field, newValue);
                        Console.WriteLine("Contact edited!");
                        break;
                    case "3":
                        Console.Write("Id to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());
                        if (manager.DeleteContact(deleteId))
                            Console.WriteLine("Contact deleted!");
                        else
                            Console.WriteLine("Contact not found.");
                        break;

                    case "4":
                        Console.Write("Id to view: ");
                        int viewId = int.Parse(Console.ReadLine());
                        var contact = manager.ViewContact(viewId);
                        Console.WriteLine($"{contact.Id} | {contact.Name} | {contact.Phone} | {contact.Email} | {contact.CreationDate}");
                        break;

                    case "5":
                        var allContacts = manager.ListAllContacts();
                        foreach (var c in allContacts.Values)
                        {
                            Console.WriteLine($"{c.Id} | {c.Name} | {c.Phone} | {c.Email} | {c.CreationDate}");
                        }
                        break;

                    case "6":
                        Console.Write("Id to search: ");
                        int searchId = int.Parse(Console.ReadLine());
                        var found = manager.Search(searchId);
                        if (found != null)
                            Console.WriteLine($"{found.Id} | {found.Name} | {found.Phone} | {found.Email} | {found.CreationDate}");
                        else
                            Console.WriteLine("Contact not found.");
                        break;

                    case "7":
                        Console.Write("Filter by Name (leave empty for none): ");
                        string fName = Console.ReadLine();
                        Console.Write("Filter by Phone (leave empty for none): ");
                        string fPhone = Console.ReadLine();
                        Console.Write("Filter by Email (leave empty for none): ");
                        string fEmail = Console.ReadLine();
                        // Dates optional: leave empty
                        Console.Write("Filter from date (yyyy-mm-dd or leave empty): ");
                        DateTime? fromDate = DateTime.TryParse(Console.ReadLine(), out var fd) ? fd : null;
                        Console.Write("Filter to date (yyyy-mm-dd or leave empty): ");
                        DateTime? toDate = DateTime.TryParse(Console.ReadLine(), out var td) ? td : null;

                        var filtered = manager.Filter(fName, fPhone, fEmail, fromDate, toDate);
                        if (filtered.Count == 0)
                            Console.WriteLine("No contacts matched the filter.");
                        else
                        {
                            foreach (var c in filtered)
                                Console.WriteLine($"{c.Id} | {c.Name} | {c.Phone} | {c.Email} | {c.CreationDate}");
                        }
                        break;

                    case "8":
                        repo.Save(manager.ListAllContacts());
                        Console.WriteLine("Contacts saved to JSON!");
                        break;

                    case "9":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            Console.WriteLine("Exiting Contact Manager. Goodbye!");
        }
    }
}