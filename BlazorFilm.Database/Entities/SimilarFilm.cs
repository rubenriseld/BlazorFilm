using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFilm.Database.Entities;

public class SimilarFilm : IReferenceEntity
{
	public int FilmId { get; set; }
	public int SimilarFilmId { get; set; }
	//[ForeignKey("FilmId")]
	public virtual Film Film { get; set; } = null!;

	[ForeignKey("SimilarFilmId")]
	public virtual Film Similar { get; set; } = null!;
}
