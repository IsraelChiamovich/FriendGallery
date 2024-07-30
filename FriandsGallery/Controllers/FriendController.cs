using FriandsGallery.Data;
using FriandsGallery.Models;
using FriandsGallery.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using static FriandsGallery.Utils.ImageUtils;
using static FriandsGallery.Utils.NullUtils;

namespace FriandsGallery.Controllers
{
	public class FriendController : Controller
	{
		private readonly ApplicationDbContext _context;

		public FriendController(ApplicationDbContext dbcontext)
		{
			_context = dbcontext;
		}


		public IActionResult Index()
		{
			ImmutableList<FriendModel> allFriends = _context.Friends.ToImmutableList();

			return View(allFriends);
		}

		public IActionResult Delete(long id)
		{
			FriendModel? toDelete = _context.Friends.Find(id);
			if (toDelete != null)
			{
				_context.Friends.Remove(toDelete);
				_context.SaveChanges();
			}

			return RedirectToAction("Index");
		}

		public IActionResult Edit(long id)
		{
			FriendModel? feiendById = _context.Friends.Find(id);

			FriendVM friendVM = new() { Id = id };

			if (feiendById != null)
			{
				friendVM.FirstName = feiendById.FirstName;
				friendVM.LastName = feiendById.LastName;
			}
			return View(friendVM);
		}

		[HttpPost]
		public IActionResult Edit(FriendVM friendVM)
		{
			FriendModel? byId = _context.Friends.Find(friendVM.Id);
			if (byId != null)
			{
				byId.FirstName = friendVM.FirstName;
				byId.LastName = friendVM.LastName;
				_context.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		public IActionResult Create() => View(new FriendVM());

		[HttpPost]
		public IActionResult Create(FriendVM friendVM)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest("Model is invalid");
			}

			FriendModel model = new() { FirstName = friendVM.FirstName, LastName = friendVM.LastName };

			_context.Friends.Add(model);
			_context.SaveChanges();
			return RedirectToAction("Index");

		}

		public async Task<IActionResult> AddImage(long id)
		{
			FriendModel? model = await _context.Friends.FindAsync(id);

			if (model == null) { return RedirectToAction("Index"); }

			FriendVM friendVM = new()
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				Id = id
			};
			return View(friendVM);
		}


		[HttpPost]
		public async Task<IActionResult> AddImage(FriendVM friendVM)
		{
			FriendModel? model = await _context.Friends.FindAsync(friendVM.Id);

			if (model == null) {return RedirectToAction("Index");}
			if (friendVM.UploadedImage ==  null) { return RedirectToAction("Index"); }

			byte[]? image = ConvertFormIFormFile(friendVM.UploadedImage);

			if (image is byte[] img)
			{
				model.Images.Add(
					new() { Data = img }
				);	
				await _context.SaveChangesAsync();
			}
			return RedirectToAction("Index");
		}

        public async Task<IActionResult> FriendDetails(long id)
        {
            FriendModel? byId = await _context.Friends
                .Include(f => f.Images)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (byId == null) { return RedirectToAction("Index"); }
            return View(byId);
        }
    }
}
