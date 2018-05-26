using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComicBookLibraryManagerWebApp.ViewModels
{
	public class ComicBooksDeleteViewModel
	{
		public ComicBook ComicBook { get; set; } = new ComicBook();

		/// <summary>
		/// This property enables model binding to be able to bind the "id"
		/// route parameter value to the "ComicBook.Id" model property.
		/// </summary>
		public int Id
		{
			get { return ComicBook.Id; }
			set { ComicBook.Id = value; }
		}

		/// <summary>
		/// To indicate that the entity has been deleted, to avoid a 404 error when trying to return the view
		/// </summary>
		public bool ComicBookHasBeenDeleted { get; set; }

	}
}