using Workdays.Model;

namespace Workdays.Service;

public interface IWorkdayService
{
    decimal Percentage { get; set; }
    List<Workday>? Workdays { get; set; }

    Task Populate();

    void UpdatePercentage();

    Task PersistData();

    Task DeleteData();
}