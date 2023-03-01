namespace BlazorFilm.Common.Services
{
	public interface IUserService
	{
		//Task<List<FilmDTO>> GetAsync();
		//Task<List<FilmDTO>> GetFilmsAsync(bool freeOnly);
		Task<List<FilmDTO>> GetFilmsAsync(bool freeOnly = false);
		Task<List<GenreDTO>> GetGenresAsync();
		//Task<FilmDTO> SingleAsync(string uri);
		Task<FilmDTO> SingleFilmAsync(string uri);
		Task<GenreDTO> SingleGenreAsync(string uri);
	}
}