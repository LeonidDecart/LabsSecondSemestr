using book;
using reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace log
{
    class BookLog
    {
        public Reader User { get; }
        public Book Book { get; }
        public DateTime TakenDate { get; }
        public DateTime? ReturnedDate { get; }
        public BookLog(Reader user, Book book, DateTime takenDate, DateTime? returnedDate)
        {
            User = user;
            Book = book;
            TakenDate = takenDate;
            ReturnedDate = returnedDate;
        }
    }
}
