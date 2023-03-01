using BlazorFilm.Common.DTOs;
using BlazorFilm.Database.Entities;
using BlazorFilm.Database.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorFilm.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FilmsController : ControllerBase
	{
		private readonly IDbService _db;

		public FilmsController(IDbService db)
		{
			_db = db;
		}
		[HttpGet]
		public async Task<IResult> Get(bool freeOnly)
		{
			try
			{
                _db.Include<Film>();
                _db.IncludeReference<FilmGenre>();
				var films = freeOnly ?
					await _db.GetAsync<Film, FilmDTO>(f => f.Free == freeOnly) :
					await _db.GetAsync<Film, FilmDTO>();
				//foreach (var film in films)
				//{
				//	foreach (var genre in film.Genres)
				//	{
				//		film.GenreNames.Add(genre.Name);
				//	}
				//}
				//foreach (var item in films)
				//{
				//	//var director = await _db.SingleAsync<Director, DirectorDTO>(d => d.Id == item.DirectorId);
				//	//item.DirectorName = director.Name;

				//	var filmgenre = await _db.GetReferenceAsync<FilmGenre, FilmGenreDTO>();
				//	foreach (var fg in filmgenre)
				//	{
				//		if (fg.FilmId == item.Id) item.Genres.Add(await _db.SingleAsync<Genre, GenreBaseDTO>(g => g.Id == fg.GenreId));
				//	}
				//	//var genres = await _db.GetAsync<Genre, GenreDTO>(g => g.GenreId == item.Id);
				//	//item.Genres = genres;
				//}
				return Results.Ok(films);
			}
			catch (Exception ex)
			{
				return Results.NotFound("Couldn't find any films." + ex);  //(ex.Message);

			}
		}

		// GET api/<FilmsController>/5
		[HttpGet("{id}")]
		public async Task<IResult> Get(int id)
		{
			try
			{
				//_db.Include<SimilarFilm>(); //laddar alla relaterade nav props
				_db.Include<Film>();
				_db.IncludeReference<FilmGenre>();
				var exists = await _db.AnyAsync<Film>(f => f.Id== id);
				if (!exists) return Results.NotFound($"Couldn't find any film with id: {id}.");

				var film = await _db.SingleAsync<Film, FilmDTO>(c => c.Id.Equals(id));

				//var filmgenre = await _db.GetReferenceAsync<FilmGenre, FilmGenreDTO>();
				//foreach (var fg in filmgenre)
				//{
				//	if (fg.FilmId == film.Id) film.Genres.Add(await _db.SingleAsync<Genre, GenreBaseDTO>(g => g.Id == fg.GenreId));
				//}
				return Results.Ok(film);
			}
			catch
			{
				return Results.NotFound($"Couldn't find any film with id: {id}.");
			}
		}

		// POST api/<FilmsController>
		[HttpPost]
		public async Task<IResult> Post([FromBody] FilmCreateDTO dto)
		{
			try
			{
				if (dto == null) return Results.BadRequest("dto is null");
				var film = await _db.AddAsync<Film, FilmCreateDTO>(dto);
				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest("something went wrong here");

				return Results.Created(_db.GetURI<Film>(film), film); //inferred type
			}
			catch
			{
				return Results.BadRequest("Couldn't add the film.");
			}
		}

		// PUT api/<FilmsController>/5
		[HttpPut("{id}")]
		public async Task<IResult> Put(int id, [FromBody] FilmEditDTO dto)
		{
			try
			{
				if (id != dto.Id) return Results.BadRequest($"ID mismatch. URI ID: {id}, DTO ID:{dto.Id}");

				var exists = await _db.AnyAsync<Director>(c => c.Id.Equals(dto.DirectorId));
				if (!exists) return Results.NotFound("Director not found.");

				exists = await _db.AnyAsync<Film>(c => c.Id.Equals(id));
				if (!exists) return Results.NotFound("Film not found.");

				_db.Update<Film, FilmCreateDTO>(id, dto);

				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest("Couldn't update the film.");

				return Results.NoContent(); //behöver inte returneras nåt när man uppdaterat, data finns redan på klient
			}
			catch
			{
				return Results.BadRequest("Couldn't update the film.");
			}
		}

		// DELETE api/<FilmsController>/5
		[HttpDelete("{id}")]
		public async Task<IResult> Delete(int id)
		{
			try
			{
				var exists = await _db.AnyAsync<Film>(c => c.Id.Equals(id));
				if (!exists) return Results.NotFound("Film not found.");

				var result = await _db.DeleteAsync<Film>(id);
				if (!result) return Results.NotFound("Film not found.");

				result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest();

				return Results.NoContent();
			}
			catch
			{
				return Results.BadRequest("Couldn't delete the film.");
			}
		}
	}
}
