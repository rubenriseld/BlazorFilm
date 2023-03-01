﻿using BlazorFilm.Common.DTOs;
using BlazorFilm.Database.Entities;
using BlazorFilm.Database.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorFilm.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SimilarFilmsController : ControllerBase
	{
		private readonly IDbService _db;
		public SimilarFilmsController(IDbService db) => _db = db;

		[HttpGet]
		public async Task<IResult> Get()
		{
			try
			{
				_db.IncludeReference<SimilarFilm>();
				var refs = await _db.GetReferenceAsync<SimilarFilm, SimilarFilmDTO>();
				
				return Results.Ok(refs);
			}
			catch //(Exception ex)
			{
				return Results.NotFound("Couldn't find any films.");  //(ex.Message);

			}
		}

		// POST api/<SimilarFilmsController>
		[HttpPost]
		public async Task<IResult> Post([FromBody] SimilarFilmCreateDTO dto)
		{
			try
			{
				var entity = await _db.AddReferenceAsync<SimilarFilm, SimilarFilmCreateDTO>(dto);

				var result = await _db.SaveChangesAsync();
				if (!result) return Results.BadRequest();
				return Results.Ok();
			}
			catch
			{
				return Results.BadRequest("Couldn't add the reference.");
			}
		}


		// DELETE api/<SimilarFilmsController>/5
		[HttpDelete]
		public async Task<IResult> Delete(SimilarFilmCreateDTO dto)
		{
			try
			{
				var success = _db.DeleteReference<SimilarFilm, SimilarFilmCreateDTO>(dto);
				if (!success) return Results.NotFound();
				var result = await _db.SaveChangesAsync();
				if (!result) return Results.NoContent();
				return Results.Ok();
			}
			catch (Exception ex)
			{
				return Results.BadRequest("Couldn't delete the reference." + ex);
			}
		}
	}
}
