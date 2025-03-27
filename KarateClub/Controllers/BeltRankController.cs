using KarateClub.Core;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarateClub.Controllers
{
    public class BeltRankController : Controller
    {
        private readonly IRepository<BeltRank> _BeltRankRepository;

        public BeltRankController(IRepository<BeltRank> BeltRankRepository)
        {
            _BeltRankRepository = BeltRankRepository;
        }
        // GET: BeltRankController
        public ActionResult Index()
        {
            return View(_BeltRankRepository.GetAllData());
        }

        // GET: BeltRankController/Details/5
        public ActionResult Details(int id)
        {
            return View(_BeltRankRepository.Find(id));
        }

        // GET: BeltRankController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BeltRankController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BeltRank collection)
        {
            try
            {
                _BeltRankRepository.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "The deletion was not successful.";

                return View();
            }
        }

        // GET: BeltRankController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _BeltRankRepository.Find(id);
            return View(data);
        }

        // POST: BeltRankController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BeltRank collection)
        {
            try
            {
                _BeltRankRepository.Edit(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BeltRankController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_BeltRankRepository.Find(id));
        }

        // POST: BeltRankController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,  BeltRank collection)
        {
            try
            {
                _BeltRankRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
