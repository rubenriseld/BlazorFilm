using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BlazorFilm.Common.Services;

public class AdminService : IAdminService
{
	private readonly BlazorFilmHttpClient _http;

	public AdminService(BlazorFilmHttpClient http)
	{
		_http = http;
	}
	public async Task<List<TDto>> GetAsync<TDto>(string uri)
	{
		try
		{
			using HttpResponseMessage response = await _http.Client.GetAsync(uri);// $"films?freeOnly=false");
			response.EnsureSuccessStatusCode();//läser statuskoden och slänger ex om nåt gått fel

			var result = JsonSerializer.Deserialize<List<TDto>>(await response.Content.ReadAsStreamAsync(),
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result ?? new List<TDto>(); //returnera resultatet, annars tom lista om null
		}
		catch (Exception ex)
		{
			throw;
		}
	}
	public async Task<TDto> SingleAsync<TDto>(string uri)
	{
		try
		{
			using HttpResponseMessage response = await _http.Client.GetAsync(uri); //kommer anropa httpclient "films/123"
			response.EnsureSuccessStatusCode(); 

			var result = JsonSerializer.Deserialize<TDto>(await response.Content.ReadAsStreamAsync(),
				new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return result;
		}
		catch (Exception ex)
		{

			throw;
		}
	}
	public async Task CreateAsync<TDto>(string uri, TDto dto)
	{
		try
		{
			using StringContent jsonContent = new(
				JsonSerializer.Serialize(dto),
				Encoding.UTF8,
				"application/json");

			using HttpResponseMessage response = await _http.Client.PostAsync(uri, jsonContent); //"films", jsonContent);

			response.EnsureSuccessStatusCode();
		}
		catch (Exception ex)
		{
			throw;
		}
	}
	public async Task UpdateAsync<TDto>(string uri, TDto dto)
	{
		try
		{
			using StringContent jsonContent = new(
				JsonSerializer.Serialize(dto),
				Encoding.UTF8,
				"application/json");

			using HttpResponseMessage response = await _http.Client.PutAsync(uri, jsonContent); //"films/123", jsonContent);

			response.EnsureSuccessStatusCode();
		}
		catch (Exception ex)
		{
			throw;
		}
	}
	public async Task DeleteAsync<TDto>(string uri)
	{
		try
		{
			using HttpResponseMessage response = await _http.Client.DeleteAsync(uri);// $"films/123");
			response.EnsureSuccessStatusCode();
		}
		catch (Exception ex)
		{
			throw;
		}
	}
	public async Task DeleteAsync<TDto>(string uri, TDto dto) //för refs
	{
		try
		{
			var request = new HttpRequestMessage(HttpMethod.Delete, uri);
			request.Content = JsonContent.Create(dto);
			using var response = await _http.Client.SendAsync(request);
			response.EnsureSuccessStatusCode();
		}
		catch (Exception)
		{

			throw;
		}
	}
}
