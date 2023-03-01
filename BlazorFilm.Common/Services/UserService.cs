

namespace BlazorFilm.Common.Services;

public class UserService : IUserService
{
	private readonly BlazorFilmHttpClient _http;

	public UserService(BlazorFilmHttpClient http)
	{
		_http = http;
	}

	public async Task<List<FilmDTO>> GetFilmsAsync(bool freeOnly = false)
	{
		try
		{
			//bool freeOnly = false;
			//HttpResponseMessage response = await _http.Client.GetAsync($"courses?freeOnly={freeOnly}");
			//HttpResponseMessage response = await _http.Client.GetAsync($"films?freeOnly={freeOnly}");
			HttpResponseMessage response = await _http.Client.GetAsync($"films?freeOnly={freeOnly}");
			response.EnsureSuccessStatusCode(); //slänger ex om den misslyckas

			var result = JsonSerializer.Deserialize<List<FilmDTO>>(await response.Content.ReadAsStreamAsync(),
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true //C# använder PascalCasing, JSON använder camelCasing
				});
			return result ?? new List<FilmDTO>(); // ?? = if null
		}
		catch (Exception)
		{

			return new(); //känner av vilken datatyp som ska returneras
		}

	}
	public async Task<FilmDTO> SingleFilmAsync(string uri)
	{
		try
		{
			using HttpResponseMessage response = await _http.Client.GetAsync(uri); //kommer anropa httpclient "films/123"
			response.EnsureSuccessStatusCode(); //läser statuskoden och slänger ex om nåt gått fel

			var result = JsonSerializer.Deserialize<FilmDTO>(await response.Content.ReadAsStreamAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result ?? new FilmDTO(); //returnera resultatet, annars tom lista om null
		}
		catch (Exception ex)
		{

			throw;
		}
	}

	public async Task<List<GenreDTO>> GetGenresAsync()
	{
		try
		{
			//bool freeOnly = false;
			//HttpResponseMessage response = await _http.Client.GetAsync($"courses?freeOnly={freeOnly}");
			HttpResponseMessage response = await _http.Client.GetAsync("genres");
			response.EnsureSuccessStatusCode(); //slänger ex om den misslyckas

			var result = JsonSerializer.Deserialize<List<GenreDTO>>(await response.Content.ReadAsStreamAsync(),
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true //C# använder PascalCasing, JSON använder camelCasing
				});
			return result ?? new List<GenreDTO>(); // ?? = if null
		}
		catch (Exception)
		{

			return new(); //känner av vilken datatyp som ska returneras
		}

	}
	public async Task<GenreDTO> SingleGenreAsync(string uri)
	{
		try
		{
			using HttpResponseMessage response = await _http.Client.GetAsync(uri); //kommer anropa httpclient "films/123"
			response.EnsureSuccessStatusCode(); //läser statuskoden och slänger ex om nåt gått fel

			var result = JsonSerializer.Deserialize<GenreDTO>(await response.Content.ReadAsStreamAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result ?? new GenreDTO(); //returnera resultatet, annars tom lista om null
		}
		catch (Exception ex)
		{

			throw;
		}
	}
	//public async Task<CourseInfoDTO> GetCourseAsync(int id) //false blir defaultvärde, så om den anropas utan parameter blir det false auto
	//{
	//	try
	//	{
	//		//bool freeOnly = false;
	//		//HttpResponseMessage response = await _http.Client.GetAsync($"courses?freeOnly={freeOnly}");
	//		HttpResponseMessage response = await _http.Client.GetAsync($"courses/{id}");
	//		response.EnsureSuccessStatusCode(); //slänger ex om den misslyckas

	//		var result = JsonSerializer.Deserialize<CourseInfoDTO>(await response.Content.ReadAsStreamAsync(),
	//			new JsonSerializerOptions
	//			{
	//				PropertyNameCaseInsensitive = true //C# använder PascalCasing, JSON använder camelCasing
	//			});
	//		return result ?? new(); // ?? = if null
	//	}
	//	catch (Exception)
	//	{

	//		return new(); //känner av vilken datatyp som ska returneras
	//	}

	//}
}
