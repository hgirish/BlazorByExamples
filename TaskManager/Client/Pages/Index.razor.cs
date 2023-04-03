using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using TaskManager.Shared;

namespace TaskManager.Client.Pages;

public partial class Index
{
    [Inject]
    public HttpClient Http { get; set; }

    private IList<TaskItem>?   _tasks;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            string requestUri = "api/TaskItems";
            _tasks = await Http.GetFromJsonAsync<IList<TaskItem>>(requestUri); 
        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message); ;
            error = "Error Encountered";
        } 
    }
}
