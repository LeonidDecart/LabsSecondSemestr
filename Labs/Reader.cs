using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reader
{
    class ReaderTicket
    {
        public int Id { get; }
        public ReaderTicket(int id)
        {
            Id = id;
        }
    }
    public class FullName
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public FullName(string firstName, string lastName, string middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }
    }

    class Reader
    {
        public int Id { get; }
        public FullName Name { get; }
        public ReaderTicket Ticket { get; }
        public Reader(int id, FullName name, ReaderTicket ticket)
        {
            Id = id;
            Name = name;
            Ticket = ticket;
        }
    }
}
