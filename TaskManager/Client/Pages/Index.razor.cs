using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Threading.Tasks;
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
    private async Task CheckboxChecked(TaskItem task)
    {
        task.IsComplete = !task.IsComplete;

        string requestUri = $"api/TaskItems/{task.TaskItemId}";

        var response =await Http.PutAsJsonAsync<TaskItem>(requestUri, task);

        if (!response.IsSuccessStatusCode)
        {
            error = response.ReasonPhrase;
        }
    }
    private async Task DeleteTask(TaskItem taskItem)
    {
        _tasks!.Remove(taskItem);
        StateHasChanged();

        string requestUri = $"api/TaskItems/{taskItem.TaskItemId}";
        var response = await Http.DeleteAsync(requestUri);

        if (!response.IsSuccessStatusCode)
        {
            error = response.ReasonPhrase;
        }


    }
}
