using BlazorFilm.Common.HttpClients;
using BlazorFilm.User.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorFilm.Common.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<BlazorFilmHttpClient>(client =>
	client.BaseAddress = new Uri("https://localhost:6001/api/"));

builder.Services.AddScoped<IUserService, UserService>();

await builder.Build().RunAsync();
