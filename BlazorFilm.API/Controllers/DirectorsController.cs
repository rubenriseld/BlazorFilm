using BlazorFilm.Common.DTOs;
using BlazorFilm.Database.Entities;
using BlazorFilm.Database.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorDirector.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DirectorsController : ControllerBase
	{
		private readonly IDbService _db;
		public DirectorsController(IDbService db)
		{
			_db = db;
		}

		[HttpGet]
		public async Task<IResult> Get()
		{
			try
			{
				var directors = await _db.GetAsync<Director, DirectorDTO>();
				//if(directors.Count == 0) return Results.NotFound("Couldn't find any directors.");
				return Results.Ok(directors);
			}
			catch //(Exception ex)
			{
				return Results.NotFound("Couldn't find any directors.");  //(ex.Message);

			}
		}

		// GET api/<DirectorsController>/5
		[HttpGet("{id}")]
		public async Task<IResult> Get(int id)
		{
			try
			{
				//_db.Include<SimilarDirector>(); //laddar alla relaterade nav props
				var exists = await _db.AnyAsync<Director>(f => f.Id == id);
				if (!exists) return Results.NotFound($"Couldn't find any director with id: {id}.");

				var director = await _db.SingleAsync<Director, DirectorDTO>(c => c.Id.Equals(id));
				return Results.Ok(director);
			}
			catch
			{
				return Results.NotFound($"Couldn't find any director with id: {id}.");
			}
		}

		// POST api/<DirectorsController>
		[HttpPost]
		public async Task<IResult> Post([FromBody] DirectorCreateDTO dto)
		{
			try
			{
				var director = await _db.AddAsync<Director, DirectorCreateDTO>(dto);
				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest();

				return Results.Created(_db.GetURI(director), director); //inferred type
			}
			catch (Exception ex)
			{
				return Results.BadRequest("Couldn't add the director." + ex);
			}
		}

		// PUT api/<DirectorsController>/5
		[HttpPut("{id}")]
		public async Task<IResult> Put(int id, [FromBody] DirectorEditDTO dto)
		{
			try
			{
				if (id != dto.Id) return Results.BadRequest($"ID mismatch. URI ID: {id}, DTO ID:{dto.Id}");

				var exists = await _db.AnyAsync<Director>(c => c.Id.Equals(id));
				if (!exists) return Results.NotFound("Director not found.");

				_db.Update<Director, DirectorCreateDTO>(id, dto);

				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest("Couldn't update the director.");

				return Results.NoContent(); //behöver inte returneras nåt när man uppdaterat, data finns redan på klient
			}
			catch
			{
				return Results.BadRequest("Couldn't update the director.");
			}
		}

		// DELETE api/<DirectorsController>/5
		[HttpDelete("{id}")]
		public async Task<IResult> Delete(int id)
		{
			try
			{
				var exists = await _db.AnyAsync<Director>(c => c.Id.Equals(id));
				if (!exists) return Results.NotFound("Director not found.");

				var result = await _db.DeleteAsync<Director>(id);
				if (!result) return Results.NotFound("Director not found.");

				result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest();

				return Results.NoContent();
			}
			catch (Exception ex)
			{
				return Results.BadRequest("Couldn't delete the director." + ex);
			}
		}
	}
}
