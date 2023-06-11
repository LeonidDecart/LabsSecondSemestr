using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using author;
using book;
using reader;
using log;
using database;

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