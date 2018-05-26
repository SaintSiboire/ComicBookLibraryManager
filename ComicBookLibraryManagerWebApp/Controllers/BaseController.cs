using ComicBookShared.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicBookLibraryManagerWebApp.Controllers
{
	public abstract class BaseController : Controller
	{
	
		private bool _diposed = false;

		protected Context Context { get; private set; }
		protected Repository Repository { get; private set; }

		public BaseController()
		{
			Context = new Context();
			Context.Database.Log = (message) => Debug.WriteLine(message);
			Repository = new Repository(Context);
		}

		protected override void Dispose(bool disposing)
		{
			if (_diposed == true)
				return;

			if (disposing)
			{
				Context.Dispose();
			}

			_diposed = true;

			base.Dispose(disposing);
		}

	}
}