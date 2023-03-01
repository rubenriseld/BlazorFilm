using BlazorFilm.Database.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorFilm.Database.Contexts;

public class BlazorFilmContext : DbContext
{
	public virtual DbSet<Film> Films { get; set; } = null!;
	public virtual DbSet<SimilarFilm> SimilarFilms { get; set; } = null!;
	public virtual DbSet<Director> Directors { get; set; } = null!;
	public virtual DbSet<Genre> Genres { get; set; } = null!;
	public virtual DbSet<FilmGenre> FilmGenres { get; set; } = null!;

	public BlazorFilmContext(DbContextOptions<BlazorFilmContext> options) : base(options){}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<SimilarFilm>().HasKey(sf => new { sf.FilmId, sf.SimilarFilmId });

		modelBuilder.Entity<FilmGenre>().HasKey(fg => new { fg.FilmId, fg.GenreId });

		modelBuilder.Entity<Film>(entity =>
		{
			entity.HasMany(f => f.SimilarFilms)
			.WithOne(s => s.Film)
			.HasForeignKey(f => f.FilmId)
			.OnDelete(DeleteBehavior.ClientSetNull);

			entity.HasMany(f => f.Genres)
			.WithMany(g => g.Films)
			.UsingEntity<FilmGenre>()
			.ToTable("FilmGenres");

		});
		//modelBuilder.Entity<SimilarFilm>()
		//	.HasOne(f => f.Film)
		//	.WithMany(sf => sf.SimilarFilms)
		//	.HasForeignKey(f => f.FilmId)
		//	.OnDelete(DeleteBehavior.ClientSetNull);

		//modelBuilder.Entity<SimilarFilm>()
		//	.HasOne(f => f.Similar)
		//	.WithMany(sf => sf.SimilarFilms)
		//	.HasForeignKey(f => f.SimilarFilmId)
		//	.OnDelete(DeleteBehavior.ClientSetNull);

	}
}



