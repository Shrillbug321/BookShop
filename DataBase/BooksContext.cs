using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BookShop.DataBase
{
	public class BooksContext: DbContext
	{
		public DbSet<BookModel> Books { get; set; }

        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BookModel>().HasData(getBooks());
			base.OnModelCreating(modelBuilder);
		}
		
		private List<BookModel> getBooks()
		{
			List<BookModel> books = new()
			{
				new BookModel
				{
					Id = 1,
					Title = "Harry Potter i Kamień Filzoficzny",
					Author = "J.K. Rowling",
					Genre = Genre.Fantastyka,
					PublishDate = new DateTime(2008, 12, 1),
					Publisher = "Media Rodzina",
					Language = "PL",
					Price = 25.00M,
					Description = "Harry dostaje list z Hogwartu."
				},
				new BookModel
				{
					Id = 2,
					Title = "Książę i Żebrak",
					Author = "Mark Twain",
					Genre = Genre.Fantastyka,
					PublishDate = new DateTime(2008, 12, 1),
					Publisher = "Media Rodzina",
					Language = "PL",
					Price = 25.00M,
					Description = "Książę staje się żebrakiem."
				}
			};
			return books;
		}
	}
}