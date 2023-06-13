using System;
using LabAllowance;

class Program
{
    public static Database Database;
    static void Main()
    {
        Database = new Database();
        InitializeDatabase();
        PrintBooksInformation();
    }

    public static void PrintBooksInformation()
    {
        Console.WriteLine("{0,-25} {1,-40} {2,-30} {3,-20}","Автор","Название","Читает","Взял");
        for (int i = 0; i < Database.Books.Count; i++)
        {
            PrintBookInformation(Database.Books[i]);   
        }
    }

    public static void PrintBookInformation(Book book)
    {
        foreach (Log log in Database.Logs)
        {
            if (log.Book == book && log.ReturnDate == null)
            {
                Console.WriteLine("{0,-25} {1,-40} {2,-30} {3,-20}", book.Author, book.Name, log.Reader.Name, log.TakeDate);
                return;
            }
        }
        Console.WriteLine("{0,-25} {1,-40}", book.Author, book.Name);
    }

    public static void InitializeDatabase()
    {
        InitializeReaders();
        InitializeBooks();
        InitializeLogs();
    }
    public static void InitializeReaders()
    {
        string[] readerStrings = File.ReadAllLines("../../../../Data/Readers.csv");
        for (int i=0; i<readerStrings.Length; i++)
        {
            string readerString = readerStrings[i];
            string[] readerStringSplitted = readerString.Split(";");
            Type type = typeof(Reader);
            int NumberOfRecords = type.GetProperties().Length;
            if (readerStringSplitted.Length != NumberOfRecords)
                throw new Exception("File Readers.csv | Invalid count of fields | Line - "+(i+1).ToString());
            if (!uint.TryParse(readerStringSplitted[0], out uint readerId))
                throw new Exception("File Readers.csv | Invalid readerId format | Line - " + (i + 1).ToString());
            Database.Readers.Add(new Reader()
            {
                Id = readerId,
                Name = readerStringSplitted[1]
            }
            );
        }
    }

    public static void InitializeBooks()
    {
        string[] bookStrings = File.ReadAllLines("../../../../Data/Books.csv");
        for (int i = 0; i<bookStrings.Length; i++)
        {
            string bookString = bookStrings[i];
            string[] bookStringSplitted = bookString.Split(";");

            Type type = typeof(Book);
            int NumberOfRecords = type.GetProperties().Length;
            if (bookStringSplitted.Length != NumberOfRecords)
                throw new Exception("File Books.csv | Invalid count of fields | Line - " + (i + 1).ToString());
            if (!uint.TryParse(bookStringSplitted[0], out uint bookId))
                throw new Exception("File Books.csv | Invalid bookId format | Line - " + (i + 1).ToString());
            if (!int.TryParse(bookStringSplitted[3], out int bookPublicationYear))
                throw new Exception("File Books.csv | Invalid bookPublicationYear format | Line - " + (i + 1).ToString());
            if (!uint.TryParse(bookStringSplitted[4], out uint bookShelfNumber))
                throw new Exception("File Books.csv | Invalid bookShelfNumber format | Line - " + (i + 1).ToString());
            if (!uint.TryParse(bookStringSplitted[5], out uint bookShelfPlaceNumber))
                throw new Exception("File Books.csv | Invalid bookShelfPlaceNumber format | Line - " + (i + 1).ToString());
            Database.Books.Add(new Book()
            {
                Id = bookId,
                Name = bookStringSplitted[1],
                Author = bookStringSplitted[2],
                PublicationYear = bookPublicationYear,
                ShelfNumber = bookShelfNumber,
                ShelfPlaceNumber = bookShelfPlaceNumber
            }
            );
        }
    }

    public static void InitializeLogs()
    {
        string[] logStrings = File.ReadAllLines("../../../../Data/Logs.csv");
        for (int i= 0;i < logStrings.Length;i++)
        {
            string logString = logStrings[i];
            string[] logStringSplitted = logString.Split(";");

            Type type = typeof(Log);
            int NumberOfRecords = type.GetProperties().Length;
            if (logStringSplitted.Length != NumberOfRecords)
                throw new Exception("File Logs.csv | Invalid count of fields | Line - " + (i + 1).ToString());
            if (!uint.TryParse(logStringSplitted[0], out uint logId))
                throw new Exception("File Logs.csv | Invalid logId format | Line - " + (i + 1).ToString());
            if (!uint.TryParse(logStringSplitted[1], out uint readerId))
                throw new Exception("File Logs.csv | Invalid readerId format | Line - " + (i + 1).ToString());
            if (!uint.TryParse(logStringSplitted[2], out uint bookId))
                throw new Exception("File Logs.csv | Invalid bookId format | Line - " + (i + 1).ToString());
            if (!DateTime.TryParse(logStringSplitted[3], out DateTime logTakeDate))
                throw new Exception("File Logs.csv | Invalid logTakeDate format | Line - " + (i + 1).ToString());
            if (!DateTime.TryParse(logStringSplitted[4], out DateTime logReturnDate) && logStringSplitted[4] != "Not defined")
                throw new Exception("File Logs.csv | Invalid logReturnDate format | Line - " + (i + 1).ToString());

            Log log = new Log
            {
                Id = logId,
                Reader = Database.Readers.FirstOrDefault(reader => reader.Id == readerId)
                    ?? throw new Exception("File Logs.csv | readerId does not exist | Line - " + (i + 1).ToString()),
                Book = Database.Books.FirstOrDefault(book => book.Id == bookId)
                        ?? throw new Exception("File Logs.csv | bookId does not exist | Line - " + (i + 1).ToString()),
                TakeDate = logTakeDate,
                ReturnDate = logStringSplitted[4] != "Not defined" ? logReturnDate : (DateTime?)null
            };

            Database.Logs.Add(log);
        }
    }
}