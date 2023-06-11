using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace book
{
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
}
