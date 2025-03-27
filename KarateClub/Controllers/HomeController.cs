using KarateClub.Core;
using KarateClub.Core.ViewModel;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static KarateClub.Core.ViewModel.DashboardViewModel;

namespace KarateClub.Controllers
{
    public class HomeController : Controller
    {
        private DashboardViewModel DashboardViewModel;

        private readonly IRepository<SubscriptionPeriod> _SubscriptionPeriodRepository;
        private readonly IRepository<Member> _MemberRepository;
        private readonly IRepository<Person> _PersonRepository;
        private readonly IRepository<BeltRank> _BeltRankRepository;
        private readonly IRepository<Payment> _PaymentRepository;
        private readonly IRepository<BeltTest> _BeltTestRepository;
        private readonly IRepository<Instructor> _InstructorRepository;

        public HomeController(IRepository<SubscriptionPeriod> SubscriptionPeriodRepository,
            IRepository<Member> MemberRepository,
            IRepository<Person> PersonRepository,
            IRepository<BeltRank> BeltRankRepository,
            IRepository<Payment> PaymentRepository,
            IRepository<BeltTest> BeltTestRepository,
            IRepository<Instructor> InstructorRepository)
        {
            //DashboardViewModel = new DashboardViewModel();

            _SubscriptionPeriodRepository = SubscriptionPeriodRepository;
            _MemberRepository = MemberRepository;
            _PersonRepository = PersonRepository;
            _BeltRankRepository = BeltRankRepository;
            _PaymentRepository = PaymentRepository;
            _BeltTestRepository = BeltTestRepository;
            _InstructorRepository = InstructorRepository;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            DashboardViewModel = new DashboardViewModel
            {
                MemberCount = _MemberRepository.GetAllData().Count,
                InstructorCount = _InstructorRepository.GetAllData().Count,
                TestCount = _BeltTestRepository.GetAllData().Count,
                PaymentDueCount = _PaymentRepository.GetAllData().Count,
                SubscriptionCount = _SubscriptionPeriodRepository.GetAllData().Count
            };
            DashboardViewModel.RevenueSources = new List<DashboardViewModel.RevenueSource>
        {
            new DashboardViewModel.RevenueSource { Name = "اشتراكات", Percentage =  DashboardViewModel.SubscriptionCount},
            new DashboardViewModel.RevenueSource { Name = "اختبارات", Percentage = DashboardViewModel.TestCount },
            new DashboardViewModel.RevenueSource { Name = "معسكرات", Percentage = DashboardViewModel.PaymentDueCount }
        };
            return View(DashboardViewModel);
        }

        // GET: HomeController/Details/5
       /* public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
