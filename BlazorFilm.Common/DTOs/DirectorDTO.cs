namespace BlazorFilm.Common.DTOs;

public class DirectorDTO
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
}
public class DirectorCreateDTO
{
	public string Name { get; set; } = null!;
}
public class DirectorEditDTO : DirectorCreateDTO
{
	public int Id { get; set; }
}