using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    class ContactSort : IComparer<Person>
    {
        public enum ComparisonType
        {
            surname = 3,
            mobile = 4,
            home = 5
        }
        public ComparisonType _comparisonType;
        public ComparisonType ComparisonMethod
        {
            get { return _comparisonType; }
            set { _comparisonType = value; }
        }

        public int Compare(Person x, Person y)
        {
            return x.CompareTo(y);
        }

    }
}
