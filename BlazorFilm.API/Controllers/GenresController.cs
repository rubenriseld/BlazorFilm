using BlazorFilm.Common.DTOs;
using BlazorFilm.Database.Entities;
using BlazorFilm.Database.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorFilm.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenresController : ControllerBase
	{
		private readonly IDbService _db;

		public GenresController(IDbService db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<IResult> Get()
		{
			try
			{
				_db.Include<Genre>();
				_db.IncludeReference<FilmGenre>();
				var genres = await _db.GetAsync<Genre, GenreDTO>();
				if (genres.Count == 0) return Results.NotFound("Couldn't find any genres.");
				return Results.Ok(genres);
			}
			catch //(Exception ex)
			{
				return Results.NotFound("Couldn't find any genres.");  //(ex.Message);

			}
		}

		// GET api/<GenresController>/5
		[HttpGet("{id}")]
		public async Task<IResult> Get(int id)
		{
			try
			{
				//_db.Include<SimilarGenre>(); //laddar alla relaterade nav props
				_db.Include<Genre>();
                _db.IncludeReference<FilmGenre>();

                var exists = await _db.AnyAsync<Genre>(f => f.Id == id);
				if (!exists) return Results.NotFound($"Couldn't find any Genre with id: {id}.");

				var genre = await _db.SingleAsync<Genre, GenreDTO>(c => c.Id.Equals(id));
				return Results.Ok(genre);
			}
			catch
			{
				return Results.NotFound($"Couldn't find any genre with id: {id}.");
			}
		}

		// POST api/<GenresController>
		[HttpPost]
		public async Task<IResult> Post([FromBody] GenreCreateDTO dto)
		{
			try
			{
				var genre = await _db.AddAsync<Genre, GenreCreateDTO>(dto);
				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest();

				return Results.Created(_db.GetURI(genre), genre); //inferred type
			}
			catch
			{
				return Results.BadRequest("Couldn't add the genre.");
			}
		}

		// PUT api/<GenresController>/5
		[HttpPut("{id}")]
		public async Task<IResult> Put(int id, [FromBody] GenreEditDTO dto)
		{
			try
			{
				if (id != dto.Id) return Results.BadRequest($"ID mismatch. URI ID: {id}, DTO ID:{dto.Id}");

				var exists = await _db.AnyAsync<Genre>(c => c.Id.Equals(id));
				if (!exists) return Results.NotFound("Genre not found.");

				_db.Update<Genre, GenreCreateDTO>(id, dto);

				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest("Couldn't update the Genre.");

				return Results.NoContent(); //behöver inte returneras nåt när man uppdaterat, data finns redan på klient
			}
			catch
			{
				return Results.BadRequest("Couldn't update the Genre.");
			}
		}

		// DELETE api/<GenresController>/5
		[HttpDelete("{id}")]
		public async Task<IResult> Delete(int id)
		{
			try
			{
				var exists = await _db.AnyAsync<Genre>(c => c.Id.Equals(id));
				if (!exists) return Results.NotFound("Genre not found.");

				var result = await _db.DeleteAsync<Genre>(id);
				if (!result) return Results.NotFound("Genre not found.");

				result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest();

				return Results.NoContent();
			}
			catch
			{
				return Results.BadRequest("Couldn't delete the Genre.");
			}
		}
	}
}
