﻿using ComicBookShared.Models;
using ComicBookShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ComicBookShared.Data
{
	public class SeriesRepository : BaseRepository<Series>
	{
		public SeriesRepository(Context context)
			: base(context)
		{
		}

		public override Series Get(int id, bool includeRelatedEntities = true)
		{
			var series = Context.Series.AsQueryable();

			if(includeRelatedEntities)
			{
				series = series
							.Include(s => s.ComicBooks);
			}

			return series
						.Where(s => s.Id == id)
						.SingleOrDefault();
		}

		public override IList<Series> GetList()
		{
			return Context.Series
							.OrderBy(s => s.Title)
							.ToList();
		}
	}
}
