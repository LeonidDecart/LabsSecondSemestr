using author;
using book;
using reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    class Database
    {
        public List<BookLot> BookLots { get; }
        public List<Author> Authors { get; }
        public List<Book> Books { get; }
        public List<ReaderTicket> ReaderTickets { get; }
        public List<Reader> Readers { get; }
    }
}
