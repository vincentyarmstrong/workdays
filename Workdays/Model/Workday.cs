namespace Workdays.Model;

public class Workday
{
    public DateTime Date { get; set; }
    public DayOfWeek Day { get; set; }
    public SelectedLocation SelectedLocation { get; set; }
}

public enum SelectedLocation { Home, Office, Leave }