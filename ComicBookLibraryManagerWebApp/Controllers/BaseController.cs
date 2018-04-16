using ComicBookShared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicBookLibraryManagerWebApp.Controllers
{
	public abstract class BaseController : Controller
	{
		private Context _context = null;
		private bool _diposed = false;

		protected Repository Repository { get; private set; }

		public BaseController()
		{
			_context = new Context();
			Repository = new Repository(_context);
		}

		protected override void Dispose(bool disposing)
		{
			if (_diposed == true)
				return;

			if (disposing)
			{
				_context.Dispose();
			}

			_diposed = true;

			base.Dispose(disposing);
		}

	}
}