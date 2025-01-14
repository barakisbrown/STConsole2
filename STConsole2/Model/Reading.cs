namespace STConsole2.Model
{
    internal class Reading
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public DateOnly Added { get; set; }

        public override string ToString()
        {
            return $"ID = {ID}, Amount = {Amount}, Date Added = {Added}";
        }
    }
}
