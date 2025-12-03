
public class LibraryItemList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public decimal DailyLateFee { get; set; }
        public bool IsCheckedOut { get; set; }
        public string LoanPeriod { get; set; }


    public LibraryItemList(int id, string title, string type, decimal dailyfee, bool ischeckedout, string loanperiod) 
    {
        Id = id;
        Title = title;
        Type = type;
        DailyLateFee = dailyfee;
        IsCheckedOut = ischeckedout;
        LoanPeriod = loanperiod;
    }

    public string ToCSV()
    {
        return $"{Id},{Title},{Type},{DailyLateFee},{IsCheckedOut},{LoanPeriod}\n";
    }
}
