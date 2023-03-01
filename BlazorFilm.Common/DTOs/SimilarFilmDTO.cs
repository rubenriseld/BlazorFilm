using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorFilm.Common.DTOs;

public class SimilarFilmBaseDTO
{
    public int FilmId { get; set; }
    public int SimilarFilmId { get; set; }
    public FilmBaseDTO? Similar { get; set; }
}

public class SimilarFilmDTO : SimilarFilmBaseDTO
{

    public FilmBaseDTO? Film { get; set; }

}
public class SimilarFilmCreateDTO
{
	public int FilmId { get; set; }
	public int SimilarFilmId { get; set; }
}

//public class SimilarFilmViewDTO : SimilarFilmBaseDTO
//{
//    public SimilarFilmViewDTO()
//    {
//        SimilarFilmTitle = Similar.Title;
//    }
//    public string SimilarFilmTitle { get; set; }
//}