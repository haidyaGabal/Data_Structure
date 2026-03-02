using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAdvanced
{
    public class Person : IComparable<Person>
    {
        public int Id { get; set; }

        public int CompareTo(Person? other)
        {
            if(other == null) return 1;
            return this.Id.CompareTo(other.Id);
        }
        // public string Name { get; set; }
        // public string Title { get; set; }
    }
    public class PersonEquality : IEqualityComparer<Person>
    {
        public bool Equals(Person? x, Person? y)
        {

           if (x == null || y == null) { return false; }
           if(x.Id== y.Id) { return true; }
           return false;
        }

        public int GetHashCode([DisallowNull] Person obj)
        {
           return obj.GetHashCode();
        }
    }
}
