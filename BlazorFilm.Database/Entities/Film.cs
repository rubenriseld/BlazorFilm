using System.ComponentModel.DataAnnotations;
using System.IO;

namespace BlazorFilm.Database.Entities;

public class Film : IEntity
{

	public Film()
	{
		SimilarFilms = new HashSet<SimilarFilm>();
		Genres = new HashSet<Genre>();
	}
	public int Id { get; set; }
	[MaxLength(50), Required]
	public string Title { get; set; }
	public DateTime Released { get; set; }
	public int DirectorId { get; set; }
	public bool Free { get; set; }
	[MaxLength(1024)]
	public string Description { get; set; }
	[MaxLength(1024)]
	public string FilmUrl { get; set; }

	public virtual ICollection<SimilarFilm>? SimilarFilms { get; set; }
	public virtual ICollection<Genre>? Genres { get; set; }

	public virtual Director? Director { get; set; }
}


//public Film()
//{
//	SimilarFilms = new HashSet<SimilarFilm>();
//	Genres = new HashSet<Genre>();
//}

//public int Id { get; set; }
//public string Title { get; set; } = null!;
//public int DirectorId { get; set; }

//public virtual ICollection<SimilarFilm> SimilarFilms { get; set; }
//public virtual ICollection<Genre> Genres { get; set; }

//public virtual Director Director { get; set; } = null!;
