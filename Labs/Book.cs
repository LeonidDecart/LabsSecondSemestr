namespace LabAllowance
{
    public class Book
    {
        public static int FieldCount = 6;
        public uint Id { get; set; } 
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public uint ShelfNumber { get; set; } // Номер полки
        public uint ShelfPlaceNumber { get; set; } // Номер места на полке
    }
}