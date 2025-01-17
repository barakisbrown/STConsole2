namespace STConsole2.Model
{
    /// <summary>
    /// ReportData Class will be used in a Report mode of the application.
    /// It will show me a consise view of the following:
    /// Number of Readings
    /// Minimum Reading
    /// Maximum Reading
    /// Running AVERAGE 
    /// Number Readings over 200
    /// </summary>
    internal class ReportData
    {
        public int Count { get; set; }
        public int MIN { get; set; }
        public int Max { get; set; }
        public int AVG { get; set; }
        public int Over200 { get; set; }
    }
}
