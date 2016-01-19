using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PostIT.Controllers
{
    public class BaseController : Controller
    {
        // GET: Language
        public ActionResult ChangeLanguage(string language, string redirectUrl)
        {
            if (language != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }

            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            if (redirectUrl != null)
            {
                return this.Redirect(redirectUrl);
            }

            return View("/");
        }
    }
}