using System.ComponentModel.DataAnnotations;

namespace BlazorFilm.Database.Entities;

public class Genre : IEntity
{
	public Genre()
	{
		Films = new HashSet<Film>();
	}

	public int Id { get; set; }
	[MaxLength(50), Required]
	public string Name { get; set; } = null!;

	public virtual ICollection<Film> Films { get; set; }
}
