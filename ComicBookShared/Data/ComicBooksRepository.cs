﻿using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
	public class ComicBooksRepository : BaseRepository<ComicBook>
	{
	
		public ComicBooksRepository(Context context)
			: base(context)
		{
		}

		public override IList<ComicBook> GetList()
		{
			return Context.ComicBooks
				.Include(cb => cb.Series)
				.OrderBy(cb => cb.Series.Title)
				.ThenBy(cb => cb.IssueNumber)
				.ToList();
		}


		public override ComicBook Get(int id, bool includedRelatedEntities = true)
		{
			var comicBooks = Context.ComicBooks.AsQueryable();

			if (includedRelatedEntities)
			{
				comicBooks = comicBooks
					.Include(cb => cb.Series)
					.Include(cb => cb.Artists.Select(a => a.Artist))
					.Include(cb => cb.Artists.Select(a => a.Role));
			}

			return comicBooks
					.Where(cb => cb.Id == id)
					.SingleOrDefault();
		}

		public bool ComicBookSeriesHasIssueNumber(int comicBookId, int seriesId, int issueNumber)
		{
			return Context.ComicBooks
						.Any(cb => cb.Id != comicBookId &&
									   cb.SeriesId == seriesId &&
									   cb.IssueNumber == issueNumber);
		}

		public bool ComicBookArtistsHasCombinationId(int artistId, int roleId, int comicBookId)
		{
			return Context.ComicBookArtists
						.Any(cba => cba.ArtistId == artistId &&
									cba.RoleId == roleId &&
									cba.ComicBookId == comicBookId);
		}


	}
}
