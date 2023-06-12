using System;
using LabAllowance;

class Program
{
    public static Database Database;
    static void Main()
    {
        Database = new Database();
        InitializeDatabase();
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
            if (readerStringSplitted.Length != Reader.FieldCount)
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
            if (bookStringSplitted.Length != Book.FieldCount)
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

            if (logStringSplitted.Length != Log.FieldCount)
                throw new Exception("File Logs.csv | Invalid count of fields | Line - " + (i + 1).ToString());
            if (!uint.TryParse(logStringSplitted[0], out uint logId))
                throw new Exception("File Logs.csv | Invalid logId format | Line - " + (i + 1).ToString());
            if (!uint.TryParse(logStringSplitted[1], out uint readerId))
                throw new Exception("File Logs.csv | Invalid readerId format | Line - " + (i + 1).ToString());
            if (!uint.TryParse(logStringSplitted[2], out uint bookId))
                throw new Exception("File Logs.csv | Invalid bookId format | Line - " + (i + 1).ToString());
            if (!DateTime.TryParse(logStringSplitted[3], out DateTime logTakeDate))
                throw new Exception("File Logs.csv | Invalid logTakeDate format | Line - " + (i + 1).ToString());
            if (!DateTime.TryParse(logStringSplitted[4], out DateTime logReturnDate))
                throw new Exception("File Logs.csv | Invalid logReturnDate format | Line - " + (i + 1).ToString());

            Database.Logs.Add(new Log()
            {
                Id = logId,
                Reader = Database.Readers.Find(reader => reader.Id == readerId) ?? throw new Exception("File Logs.csv | readerId is not Exist | Line - " + (i + 1).ToString()),
                Book = Database.Books.Find(book => book.Id == bookId) ?? throw new Exception("File Logs.csv | bookId is not Exist | Line - " + (i + 1).ToString()),
                TakeDate = logTakeDate,
                ReturnDate = logReturnDate
            }
            );
        }
    }
}