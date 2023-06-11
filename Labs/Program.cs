using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class BookLot
{
    public int Id { get; }
    public int ShelfNumber { get; }
    public int PlaceNumber { get; }
    public BookLot(int id, int shelfNumber, int placeNumber)
    {
        Id = id;
        ShelfNumber = shelfNumber;
        PlaceNumber = placeNumber;
    }
}
class Author
{
    public int Id { get; }
    public string Name { get; }
    public Author(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
class Book
{
    public int Id { get; }
    public string Name { get; }
    public int PublicationDate { get; }
    public BookLot Coordinates { get; }
    public Author Developer { get; }
    public Book(int id, string name, int publicationDate, BookLot coordinates, Author developer)
    {
        Id = id;
        Name = name;
        PublicationDate = publicationDate;
        Coordinates = coordinates;
        Developer = developer;
    }
}
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
    public FullName( string firstName, string lastName, string middleName)
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
class Database
{
    public List<BookLot> BookLots { get; }
    public List<Author> Authors { get; }
    public List<Book> Books { get; }
    public List<ReaderTicket> ReaderTickets { get; }
    public List<Reader> Readers { get; }
}

class Program
{
    static void Main()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string databaseJsonPath = Path.Combine(currentDirectory, "..", "..", "..", "..", "database.json");
        JObject databaseJson = JObject.Parse(File.ReadAllText(databaseJsonPath));

        List<BookLot> BookLots = new List<BookLot>();
        foreach (JObject BookLot in (JArray)databaseJson["Booklots"])
        {
            BookLots.Add(new BookLot(
                (int) BookLot["Id"], 
                (int) BookLot["shelfNumber"], 
                (int) BookLot["placeNumber"]
            ));
        }

        List<Author> Authors = new List<Author>();
        foreach (JObject Author in (JArray)databaseJson["Authors"])
        {
            Authors.Add(new Author(
                (int) Author["Id"], 
                (string) Author["Name"]
            ));
        }

        List<Book> Books = new List<Book>();
        foreach (JObject Book in (JArray)databaseJson["Books"])
        {
            BookLot foundBookLot = BookLots.Find(bookLot => bookLot.Id == (int)Book["LotId"]);
            Author foundAuthor = Authors.Find(author => author.Id == (int)Book["DeveloperId"]);
            Books.Add(new Book(
                (int) Book["Id"], 
                (string) Book["Name"], 
                (int) Book["PublicationDate"],
                foundBookLot,
                foundAuthor
            ));
        }

        List<ReaderTicket> ReaderTickets = new List<ReaderTicket>();
        foreach (JObject ReaderTicket in (JArray)databaseJson["ReaderTickets"])
        {
            ReaderTickets.Add(new ReaderTicket(
                (int)ReaderTicket["Id"]
            ));
        }

        List<Reader> Readers = new List<Reader>();
        foreach (JObject Reader in (JArray)databaseJson["Readers"])
        {
            FullName fullName = new FullName(
                (string)Reader["Name"]["FirstName"],
                (string)Reader["Name"]["Lastname"],
                (string)Reader["Name"]["MiddleName"]
            );
            ReaderTicket foundReaderTicket = ReaderTickets.Find(ticket => ticket.Id == (int)Reader["TicketId"]);
            Readers.Add(new Reader(
                (int)Reader["Id"],
                fullName,
                foundReaderTicket
            ));
        }
    }
}