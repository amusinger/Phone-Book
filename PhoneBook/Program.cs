using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PhoneBook
{

    class Program
    {
        public static List<Person> contacts = new List<Person>();
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("en-US");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Please enter everything in English\n");
            Console.ResetColor();

            bool isRunning = true;
            int option;

            while (isRunning)
            {
                displayMenu();
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 0:
                        viewAll();
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nCreate new contact");
                        Console.ResetColor();
                        addPerson();
                        break;
                    case 2:
                        deletePerson();
                        break;
                    case 3:
                        findPerson();
                        break;
                    case 4:
                        updatePerson();
                        break;
                    case 5:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thank you. Bye!");
                        Console.ResetColor();
                        Console.WriteLine("Press any key");
                        isRunning = false;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Oops, something went wrong");
                        Console.ResetColor();
                        break;
                }
            }
        }

        private static void updatePerson()
        {
        linkUpdate:
            Console.WriteLine("Enter name to update person");
            string name = Console.ReadLine();
            int i = 0;

            if (contacts.Exists(s => s.name == name))
            {
                foreach (Person p in contacts)
                {
                    if (p.name == name)
                    {
                        i++;
                        Console.WriteLine("Found{0}: \nName: {1} \nSurname: {2}\nMobile phone: {3}\nHome phone: {4}\nEmail: {5}\nAddress: {6}", i, p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                    linkSureUpdate:
                        Console.WriteLine("Are you sure you want to update a contact [yes][no]");
                        string temp = Console.ReadLine();
                        if (temp == "yes")
                        {
                            Person personToDelete = new Person();
                            personToDelete = p;
                            contacts.Remove(personToDelete);
                            addPerson();
                            break;
                        }
                        else if (temp == "no")
                        {
                            Console.WriteLine("As you wish");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Only [yes] or [no]");
                            Console.ResetColor();
                            goto linkSureUpdate;
                        }
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Couldn't find such contact");
                Console.ResetColor();
                goto linkUpdate;
            }
        }

        private static void findPerson()
        {
        linkFind:
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Enter name to find a contact");
            Console.ResetColor();
            string name = Console.ReadLine();

            if (contacts.Exists(s => s.name == name))
            {
                foreach (Person p in contacts)
                {
                    if (p.name == name)
                    {
                        Console.WriteLine("Found: \nName: {0} \nSurname: {1}\nMobile phone: {2}\nHome phone: {3}\nEmail: {4}\nAddress: {5}\n", p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, no such contact");
                Console.ResetColor();
                goto linkFind;
            }
            Console.WriteLine("");

        }

        private static void deletePerson()
        {
        linkDelete:
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Enter name to delete person");
            Console.ResetColor();
            string name = Console.ReadLine();
            int i = 0;

            if (contacts.Exists(s => s.name == name))
            {
                foreach (Person p in contacts)
                {
                    if (p.name == name)
                    {
                        i++;
                        Console.WriteLine("Found{0}: \nName: {1} \nSurname: {2}\nMobile phone: {3}\nHome phone: {4}\nEmail: {5}\nAddress: {6}", i, p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                    linkSureDelete:
                        Console.WriteLine("Are you sure you want to delete a contact [yes][no]");
                        string temp = Console.ReadLine();
                        if (temp == "yes")
                        {
                            Person personToDelete = new Person();
                            personToDelete = p;
                            contacts.Remove(personToDelete);
                            Console.WriteLine("Deleted!\n");
                            break;
                        }
                        else if (temp == "no")
                        {
                            Console.WriteLine("As you wish");
                        }
                        else
                        {
                            Console.WriteLine("Only [yes] or [no]");
                            goto linkSureDelete;
                        }
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Couldn't find such contact");
                Console.ResetColor();
                goto linkDelete;
            }
        }

        private static void addPerson()
        {
            string name, surname, phone, homePhone, email, address, temp, pattern;
        linkName:
            Console.Write("Name: ");
            name = Console.ReadLine();
            if (name == "")
            {
                goto linkName;
            }

            Console.Write("Surname: ");
            surname = Console.ReadLine();
        linkMobile:
            Console.Write("Mobile phone: +7 ");
            pattern = @"^\d{10}$";
            temp = Console.ReadLine();
            if (Regex.IsMatch(temp, pattern))
            {
                phone = temp;
            }
            else
            {
                goto linkMobile;
            }

            Console.Write("Home number: ");
            homePhone = Console.ReadLine();
        linkMail:
            Console.Write("Email: ");
            pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            temp = Console.ReadLine();
            if (Regex.IsMatch(temp, pattern))
            {
                email = temp;
            }
            else
            {
                goto linkMail;
            }
            Console.Write("Address: ");
            address = Console.ReadLine();
            Person p = new Person(name, surname, phone, homePhone, email, address);
            contacts.Add(p);
        }

        private static void viewAll()
        {
            Console.WriteLine("How do you want to sort?" +
                                "\n1: by name \n2: by email \n3: by surname" +
                                "\n4: by mobile phone \n5: by home number");
        linkSort:
            try
            {
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        int i = 0;
                        List<Person> SortedByName = contacts.OrderBy(o => o.name).ToList();
                        using (StreamWriter writer = new StreamWriter("C:\\Users\\User\\Desktop\\kbtu2\\.Net\\project Phone Book\\PhoneBook\\Contacts.txt", true))
                        {
                            foreach (Person p in SortedByName)
                            {
                                i++;
                                Console.WriteLine("Contact #{0}: \nName: {1} \nSurname: {2}\nMobile phone: +7{3}\nHome phone: {4}\nEmail: {5}\nAddress: {6}\n", i, p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                                writer.WriteLine("Contact: \nName: {0} \nSurname: {1}\nMobile phone: +7{2}\nHome phone: {3}\nEmail: {4}\nAddress: {5}\n", p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                            }
                        }
                        break;
                    case 2:
                        i = 0;
                        List<Person> SortedByMail = contacts.OrderBy(o => o.email).ToList();
                        foreach (Person p in SortedByMail)
                        {
                            i++;
                            Console.WriteLine("\nContact #{0}: \nName: {1} \nSurname: {2}\nMobile phone: +7{3}\nHome phone: {4}\nEmail: {5}\nAddress: {6}\n", i, p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                        }
                        break;

                    case 3:
                        ContactSort pc = new ContactSort();
                        pc.ComparisonMethod = ContactSort.ComparisonType.surname;
                        contacts.Sort(pc);
                        i = 0;
                        foreach (Person p in contacts)
                        {
                            i++;
                            Console.WriteLine("\nContact #{0}: \nName: {1} \nSurname: {2}\nMobile phone: +7{3}\nHome phone: {4}\nEmail: {5}\nAddress: {6}\n", i, p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                        }
                        break;

                    case 4:
                        ContactSort phone = new ContactSort();
                        phone.ComparisonMethod = ContactSort.ComparisonType.mobile;

                        i = 0;
                        foreach (Person p in contacts)
                        {
                            i++;
                            Console.WriteLine("\nContact #{0}: \nName: {1} \nSurname: {2}\nMobile phone: +7{3}\nHome phone: {4}\nEmail: {5}\nAddress: {6}\n", i, p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                        }
                        break;

                    case 5:
                        ContactSort home = new ContactSort();
                        home.ComparisonMethod = ContactSort.ComparisonType.home;

                        i = 0;
                        foreach (Person p in contacts)
                        {
                            i++;
                            Console.WriteLine("\nContact #{0}: \nName: {1} \nSurname: {2}\nMobile phone: +7{3}\nHome phone: {4}\nEmail: {5}\nAddress: {6}\n", i, p.name, p.surname, p.mobilePhone, p.homePhone, p.email, p.address);
                        }
                        break;

                    default:
                        break;
                }

            }
            catch (FormatException fe)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have to choose a number [1,2,3,4,5]");
                Console.ResetColor();
                goto linkSort;
            }
        }

        private static void displayMenu()
        {
            Console.WriteLine("\nChoose Option");
            Console.WriteLine("[0] View the list");
            Console.WriteLine("[1] Create contact");
            Console.WriteLine("[2] Delete contact");
            Console.WriteLine("[3] Search for a contact");
            Console.WriteLine("[4] Update contact");
            Console.WriteLine("[5] Exit\n");
        }


    }
}