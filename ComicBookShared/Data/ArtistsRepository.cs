using ComicBookShared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
	public class ArtistsRepository : BaseRepository<Artist>
	{
		public ArtistsRepository(Context context)
			: base(context)
		{
		}

		public override Artist Get(int id, bool includeRelatedEntities = true)
		{
			var artists = Context.Artists.AsQueryable();

			if (includeRelatedEntities)
			{
				artists = artists
					.Include(a => a.ComicBooks.Select(cb => cb.ComicBook))
					.Include(a => a.ComicBooks.Select(cb => cb.Role))
					.Include(a => a.ComicBooks.Select(cb => cb.ComicBook.Series));
			}

			return artists
						.Where(a => a.Id == id)
						.SingleOrDefault();
		}

		public override IList<Artist> GetList()
		{
			return Context.Artists
							.OrderBy(a => a.Name)
							.ToList();
		}


	}
}
