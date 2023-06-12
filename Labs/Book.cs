namespace LabAllowance
{
    public class Book
    {
        public uint Id { get; set; } 
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public uint ShelfNumber { get; set; } // Номер полки
        public uint ShelfPlaceNumber { get; set; } // Номер места на полке
    }
}