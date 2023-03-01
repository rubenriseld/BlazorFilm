namespace BlazorFilm.Common.DTOs;

public class FilmBaseDTO
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public DateTime Released { get; set; }
	public int DirectorId { get; set; }
	public bool Free { get; set; }
	public string Description { get; set; } = null!;
	public string FilmUrl { get; set; } = null!;

	public List<GenreBaseDTO> Genres { get; set; } = new();
    public DirectorDTO? Director { get; set; }
}
public class FilmDTO : FilmBaseDTO
{ 
	public List<SimilarFilmBaseDTO> SimilarFilms { get; set; } = new();
}

public class FilmCreateDTO
{
	public string Title { get; set; } = null!;
	public DateTime Released { get; set; }
	public int DirectorId { get; set; }
	public bool Free { get; set; }
	public string Description { get; set; } = null!;
	public string FilmUrl { get; set; } = null!;
}

public class FilmEditDTO : FilmCreateDTO
{
	public int Id { get; set; }
}
