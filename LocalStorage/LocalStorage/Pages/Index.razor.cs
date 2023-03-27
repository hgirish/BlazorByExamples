using LocalStorage.Models;
using LocalStorage.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LocalStorage.Pages
{
    public partial class Index
    {
        [Inject]
        ILocalStorageService? localStorage { get; set; }
        [Inject]
        IJSRuntime? jSRuntime { get; set; }

        private const string Key = "localStorageData";
        private IJSObjectReference? module;


        private string? data;

       async Task SaveToLocalStorageAsync()
        {
            var dataInfo = new DataInfo
            {
                Value = data,
                Length = data!.Length,
                Timestamp = DateTime.Now,
            };

            await localStorage!.SetItemAsync(Key, dataInfo);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                module = await jSRuntime!.InvokeAsync<IJSObjectReference>(
                    "import",
                    "./Pages/Index.razor.js");
            }
        }

        async Task ReadFromLocalStorageAsync()
        {
            if (module is not null)
            {
                DataInfo? savedData =
                    await localStorage!.GetItemAsync<DataInfo>(Key);
                string result = $"localStorageData: {savedData!.Value}";

                await module.InvokeVoidAsync("showLocalStorage", result);
            }
        }
    }
}
