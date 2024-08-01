using Blazored.LocalStorage;

namespace Workdays.Service;

public class LocalDataPersistenceService : ILocalDataPersistenceService
{
    private readonly ILocalStorageService _localStorageService;

    public LocalDataPersistenceService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task StoreObject<T>(string name, T objectToStore)
    {
        await _localStorageService.SetItemAsync(name, objectToStore);
    }

    public async Task<T?> RetrieveObject<T>(string name)
    {
        return await _localStorageService.GetItemAsync<T>(name);
    }

    public async Task DeleteObject(string name)
    {
        await _localStorageService.RemoveItemAsync(name);
    }
}