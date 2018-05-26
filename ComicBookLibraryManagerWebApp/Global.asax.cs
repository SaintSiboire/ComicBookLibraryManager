using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ComicBookLibraryManagerWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

		protected void Application_BeginRequest()
		{
			var currentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
			currentCulture.NumberFormat.NumberDecimalSeparator = ".";
			currentCulture.NumberFormat.NumberGroupSeparator = " ";
			currentCulture.NumberFormat.CurrencyDecimalSeparator = ".";

			Thread.CurrentThread.CurrentCulture = currentCulture;
		}
	}
}
