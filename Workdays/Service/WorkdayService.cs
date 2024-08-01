using Workdays.Model;

namespace Workdays.Service;

public class WorkdayService : IWorkdayService
{
    private const string ConfigName = "workdays-config";
    public decimal Percentage { get; set; }
    public List<Workday>? Workdays { get; set; }

    private ILocalDataPersistenceService _localDataPersistenceService;

    public WorkdayService(ILocalDataPersistenceService localDataPersistenceService)
    {
        _localDataPersistenceService = localDataPersistenceService;
    }

    public async Task Populate()
    {
        var workdays = await _localDataPersistenceService.RetrieveObject<List<Workday>>(ConfigName);

        if (workdays != null && workdays[0].Date.Month == DateTime.Today.Month && workdays[0].Date.Year == DateTime.Today.Year)
        {
            Workdays = workdays;
        }
        else
        {
            Workdays = GenerateWorkdays();
        }
    }

    public void UpdatePercentage()
    {
        decimal officeCount = Workdays!.Count(workday => workday.SelectedLocation == SelectedLocation.Office);
        decimal workdayCount = Workdays!.Count(workday => workday.SelectedLocation != SelectedLocation.Leave);
        Percentage = (officeCount == 0 && workdayCount == 0) ? 100 : (officeCount / workdayCount) * 100;
        Percentage = Math.Round(Percentage, 1);
    }

    public async Task PersistData()
    {
        await _localDataPersistenceService.StoreObject(ConfigName, Workdays);
    }
    public async Task DeleteData()
    {
        await _localDataPersistenceService.DeleteObject(ConfigName);
    }

    private List<Workday> GenerateWorkdays()
    {
        var year = DateTime.Now.Year;
        var month = DateTime.Now.Month;

        var workdays = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
            .Select(dayOfMonth
            => new Workday
            {
                Date = new DateTime(year, month, dayOfMonth)
            })
            .ToList();

        foreach (var workday in workdays)
        {
            workday.Day = workday.Date.DayOfWeek;
            if (workday.Day is DayOfWeek.Wednesday or DayOfWeek.Thursday)
            {
                workday.SelectedLocation = SelectedLocation.Office;
            }
        }
        return workdays.Where(workday => workday.Day != DayOfWeek.Saturday && workday.Day != DayOfWeek.Sunday).ToList();
    }
}