namespace BlazorFilm.Common.DTOs;

public class FilmGenreCreateDTO
{
	public int FilmId { get; set; }
	public int GenreId { get; set; }

}
public class FilmGenreDTO : FilmGenreCreateDTO
{
    public FilmDTO? Film { get; set; }
    public GenreDTO? Genre { get; set; }
}
