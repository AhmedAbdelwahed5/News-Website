using FirstCoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstCoreApp.Controllers
{
    public class HomeController : Controller
    {

        NewsContext _db ;
     
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, NewsContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
           var categories=  _db.Categories.ToList();

            return View(categories);
        }
        public IActionResult Contact()
        {
            

            return View();
        }
        [HttpPost]
        public IActionResult SaveContact(ContactUs model)
        {
          _db.Contacts.Add(model);
          _db.SaveChanges();


            return RedirectToAction("index");
        }
        public IActionResult Messages()
        {
         

            return View(_db.Contacts.ToList());
        }
        public IActionResult News(int id)
        {
            Category cat = _db.Categories.Find(id);
            ViewBag.category = cat.Name;
            var news=_db.News.Where(x => x.CategoryId == id).OrderByDescending(x=>x.Date).ToList();

            return View(news);
        }
        public IActionResult Delete(int id)
        {
            var news = _db.News.Find(id);
            _db.News.Remove(news);
            _db.SaveChanges();

            return RedirectToAction("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}