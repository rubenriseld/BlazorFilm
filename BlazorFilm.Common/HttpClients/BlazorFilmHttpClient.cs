namespace BlazorFilm.Common.HttpClients;

public class BlazorFilmHttpClient
{
    public HttpClient Client { get; }
	public BlazorFilmHttpClient(HttpClient client)
	{
        Client = client;
        //client.BaseAddress = new Uri("https://localhost:6001/api/");
    }
}
