using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ClaimsViewer;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.LoginMode = "redirect";
});
var objectId = builder.Configuration["AzureAd:GroupObjectId"];
Console.WriteLine( $"Object Id: {objectId}");
builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("view-all", policy =>
    policy.RequireAssertion(context =>
    context.User.HasClaim(c =>
    c.Type == "groups" &&
    c.Value.Contains(objectId))));
});

await builder.Build().RunAsync();
