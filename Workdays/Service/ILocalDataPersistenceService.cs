namespace Workdays.Service;

public interface ILocalDataPersistenceService
{
    Task StoreObject<T>(string name, T objectToStore);

    Task<T?> RetrieveObject<T>(string name);

    Task DeleteObject(string name);
}