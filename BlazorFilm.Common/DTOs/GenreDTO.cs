using System.ComponentModel.DataAnnotations;

namespace BlazorFilm.Common.DTOs;


public class GenreBaseDTO
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
public class GenreDTO : GenreBaseDTO
{
	public List<FilmBaseDTO> Films { get; set; } = new();
}

public class GenreCreateDTO
{
	public string Name { get; set; } = null!;
}

public class GenreEditDTO : GenreCreateDTO
{
	public int Id { get; set; }
}