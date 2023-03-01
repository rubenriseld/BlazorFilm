using AutoMapper;
using BlazorFilm.Common.DTOs;
using BlazorFilm.Database.Contexts;
using BlazorFilm.Database.Entities;
using BlazorFilm.Database.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BlazorFilmContext>(
	options =>
		options.UseSqlServer(
			builder.Configuration.GetConnectionString("BlazorFilmConnection"))
	);

builder.Services.AddCors(policy =>
{
	policy.AddPolicy("CorsAllAccessPolicy", opt =>
		opt.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod()
	);
});
ConfigureAutoMapper(builder.Services);
builder.Services.AddScoped<IDbService, DbService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureAutoMapper(IServiceCollection services)
{
	var config = new MapperConfiguration(cfg =>
	{
		cfg.CreateMap<Film, FilmDTO>().ReverseMap();
		cfg.CreateMap<FilmCreateDTO, Film>();
			//.ForMember(dest => dest.Genres, src => src.Ignore())
			//.ForMember;
		cfg.CreateMap<FilmEditDTO, Film>();
		cfg.CreateMap<Film, FilmBaseDTO>();

		cfg.CreateMap<Director, DirectorDTO>().ReverseMap();
		cfg.CreateMap<DirectorCreateDTO, Director>();
		cfg.CreateMap<DirectorEditDTO, Director>();

		cfg.CreateMap<Genre, GenreDTO>().ReverseMap();
		cfg.CreateMap<GenreCreateDTO, Genre>();
		cfg.CreateMap<GenreEditDTO, Genre>();
		cfg.CreateMap<Genre,GenreBaseDTO>();

		cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();
		cfg.CreateMap<FilmGenreCreateDTO, FilmGenre>();

		cfg.CreateMap<SimilarFilm, SimilarFilmDTO>().ReverseMap();
		cfg.CreateMap<SimilarFilm, SimilarFilmBaseDTO>();
		cfg.CreateMap<SimilarFilmCreateDTO, SimilarFilm>();
	});
	var mapper = config.CreateMapper();
	services.AddSingleton(mapper);
}