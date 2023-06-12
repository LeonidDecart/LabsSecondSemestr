namespace LabAllowance
{
    class Database
    {
        public List<Book> Books { get; }
        public List<Reader> Readers { get; }
        public List<Log> Logs { get; }
        public Database()
        {
            Books = new List<Book>();
            Readers = new List<Reader>();
            Logs = new List<Log>();
        }
    }
}
