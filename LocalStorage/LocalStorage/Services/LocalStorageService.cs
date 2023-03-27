using Microsoft.JSInterop;
using System.Text.Json;

namespace LocalStorage.Services;

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jSRuntime;

    public LocalStorageService(IJSRuntime jSRuntime)
    {
        _jSRuntime = jSRuntime;
    }
    public async ValueTask<T?> GetItemAsync<T>(string key)
    {
        var json = await _jSRuntime.InvokeAsync<string>(
            "bweInterop.getLocalStorage", key);
        return JsonSerializer.Deserialize<T>(json);
    }

    public async ValueTask SetItemAsync<T>(string key, T item)
    {
        await _jSRuntime.InvokeVoidAsync(
            "bweInterop.setLocalStorage",
            key,
            JsonSerializer.Serialize(item));
    }
}