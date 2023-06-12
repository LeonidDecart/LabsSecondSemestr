using System;
using LabAllowance;

class Program
{
    static void Main()
    {
        Database database = new Database();

        string[] readerStrings = File.ReadAllLines("../../../../Data/Readers.csv");
        foreach (string readerString in readerStrings)
        {
            string[] readerStringSplitted = readerString.Split(";");
            database.Readers.Add(new Reader() 
                { 
                    Id = uint.Parse(readerStringSplitted[0]), 
                    Name = readerStringSplitted[1]
                }
            );
        }

        string[] bookStrings = File.ReadAllLines("../../../../Data/Books.csv");
        foreach (string bookString in bookStrings)
        {
            string[] bookStringSplitted = bookString.Split(";");
            database.Books.Add(new Book() 
                { 
                    Id = uint.Parse(bookStringSplitted[0]), 
                    Name = bookStringSplitted[1],  
                    Author = bookStringSplitted[2],
                    PublicationYear = int.Parse(bookStringSplitted[3]),
                    ShelfNumber = uint.Parse(bookStringSplitted[4]), 
                    ShelfPlaceNumber = uint.Parse(bookStringSplitted[5])
                }
            );
        }

        string[] logStrings = File.ReadAllLines("../../../../Data/Logs.csv");
        foreach (string logString in logStrings)
        {
            string[] logStringSplitted = logString.Split(";");
            database.Logs.Add(new Log()
                {
                    Id = uint.Parse(logStringSplitted[0]),
                    Reader = database.Readers.Find(reader => reader.Id == uint.Parse(logStringSplitted[1])),
                    Book = database.Books.Find(book => book.Id == uint.Parse(logStringSplitted[2])),
                    TakeDate = DateTime.Parse(logStringSplitted[3]),
                    ReturnDate = DateTime.Parse(logStringSplitted[4])
                }
            );
        }
    }
}