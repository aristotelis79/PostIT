using PostIT.Models;
using System.Globalization;
using System.IO;
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

        public ActionResult UploadImage(string imageName)
        {
            var ImagesFolder = HttpContext.Server.MapPath("~/App_Data/Images");
            var path = Path.Combine(ImagesFolder, imageName);
            if (!path.StartsWith(ImagesFolder))
            {
                throw new HttpException(403, "Forbidden");
            }
            return File(path, "image/jpeg");
        }

        public Article SaveImageIfExist(Article article, HttpPostedFileBase upload)
        {
            if (upload != null && upload.ContentLength > 0)
            {
                var photo = new FilePath
                {
                    FileName = Path.GetFileName(upload.FileName)
                };
                upload.SaveAs(Path.Combine(HttpContext.Server.MapPath("~/App_Data/Images"), upload.FileName));
                article.FilePath = photo;
            }
            return article;
        }
    }
}