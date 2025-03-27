using KarateClub.Core;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarateClub.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IRepository<Payment> _PaymentRepository;
        private readonly IRepository<Member> _MemberRepository;

        public PaymentController(IRepository<Payment> PaymentRepository,
           IRepository<Member> MemberRepository)
        {
            _PaymentRepository = PaymentRepository;
            _MemberRepository = MemberRepository;
        }
        // GET: PaymentController
        public ActionResult Index()
        {
            return View(_PaymentRepository.GetAllData());
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(int id)
        {
            return View(_PaymentRepository.Find(id));
        }

        // GET: PaymentController/Create
        public ActionResult Create()
        {
          /*  var data = new Payment
            {
                Members = _MemberRepository.GetAllData(),
            };*/
            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Payment collection)
        {
            try
            {
                collection.Date = DateTime.Now;
                _PaymentRepository.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_PaymentRepository.Find(id));
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Payment collection)
        {
            try
            {
                _PaymentRepository.Edit(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_PaymentRepository.Find(id));
        }

        // POST: PaymentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Payment collection)
        {
            try
            {
                _PaymentRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
