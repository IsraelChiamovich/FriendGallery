using FriandsGallery.Data;
using FriandsGallery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FriandsGallery.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext dbcontext)
		{
			_context = dbcontext;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult About()
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
