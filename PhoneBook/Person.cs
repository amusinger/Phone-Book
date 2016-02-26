using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    class Person : IComparable<Person>
    {
        public string name;
        public string surname;
        public string mobilePhone;
        public string homePhone;
        public string address;
        public string email;

        public Person(string _name, string _surname, string _mobile, string _homePhone, string _email, string _address)
        {
            this.name = _name;
            this.surname = _surname;
            this.mobilePhone = _mobile;
            this.homePhone = _homePhone;
            this.email = _email;
            this.address = _address;

        }
        public Person() { }

        public int CompareTo(Person p)
        {
            return this.surname.CompareTo(p.surname);
        }
   }

}
