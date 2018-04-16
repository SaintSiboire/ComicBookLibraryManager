using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookShared.Data
{
	public class ComicBooksRepository
	{
		private Context _context = null;

		public ComicBooksRepository(Context context)
		{
			_context = context;
		}

		public IList<ComicBook> GetList()
		{
			return _context.ComicBooks
				.Include(cb => cb.Series)
				.OrderBy(cb => cb.Series.Title)
				.ThenBy(cb => cb.IssueNumber)
				.ToList();
		}


		public ComicBook Get(int id, bool includedRelatedEntities = true)
		{
			var comicBooks = _context.ComicBooks.AsQueryable();

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

		public void Add(ComicBook comicBook)
		{
			_context.ComicBooks.Add(comicBook);
			_context.SaveChanges();
		}

		public void Update(ComicBook comicBook)
		{
			_context.Entry(comicBook).State = EntityState.Modified;
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var comicBook = new ComicBook { Id = id };
			_context.Entry(comicBook).State = EntityState.Deleted;
			_context.SaveChanges();
		}

		public bool ComicBookSeriesHasIssueNumber(int comicBookId, int seriesId, int issueNumber)
		{
			return _context.ComicBooks
						.Any(cb => cb.Id != comicBookId &&
									   cb.SeriesId == seriesId &&
									   cb.IssueNumber == issueNumber);
		}

		public bool ComicBookArtistsHasCombinationId(int artistId, int roleId, int comicBookId)
		{
			return _context.ComicBookArtists
						.Any(cba => cba.ArtistId == artistId &&
									cba.RoleId == roleId &&
									cba.ComicBookId == comicBookId);
		}


	}
}
