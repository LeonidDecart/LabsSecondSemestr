namespace LabAllowance
{
    public class Log
    {
        public uint Id { get; set; }
        public Reader Reader { get; set; }
        public Book Book { get; set; }
        public DateTime TakeDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}