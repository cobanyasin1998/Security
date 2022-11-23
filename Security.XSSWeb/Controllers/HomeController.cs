using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Security.XSSWeb.Models;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace Security.XSSWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private HtmlEncoder _htmlEncoder;
        private JavaScriptEncoder _javascriptEncoder;
        private UrlEncoder _urlEncoder;

        public HomeController(ILogger<HomeController> logger, JavaScriptEncoder javascriptEncoder, UrlEncoder urlEncoder, HtmlEncoder htmlEncoder)
        {
            _logger = logger;
            _javascriptEncoder = javascriptEncoder;
            _urlEncoder = urlEncoder;
            _htmlEncoder = htmlEncoder;
        }

        public IActionResult CommentAdd()
        {
            HttpContext.Response.Cookies.Append("email", "yasincoban1998@outlook.com");
            HttpContext.Response.Cookies.Append("password", "123");

            if (System.IO.File.Exists("comment.txt"))
            {
                ViewBag.comment = System.IO.File.ReadAllLines("comment.txt");
            }

            return View();
        }
        [HttpPost]
        public IActionResult CommentAdd(string name, string comment)
        {
            /*
             
             <script>console.log("Merhaba")</script>
             
             */

            string encodeName = _urlEncoder.Encode(name);


            System.IO.File.AppendAllText("comment.txt", $"{name}-{comment}\n");

            ViewBag.Name = name;
            ViewBag.Comment = comment;

            return RedirectToAction("CommentAdd");
        }
        public IActionResult Index()
        {

            ////if (Url.IsLocalUrl)
            ////{
            // is redirect to attack koruması 
            ////}

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
