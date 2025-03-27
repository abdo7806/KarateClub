using KarateClub.Core;
using KarateClub.Core.ViewModel;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.DataContracts;

namespace KarateClub.Controllers
{
    public class BeltTestController : Controller
    {
        private readonly IRepository<BeltTest> _BeltTestRepository;
        private readonly IRepository<Instructor> _InstructorRepository;
        private readonly IRepository<Member> _MemberRepository;
        private readonly IRepository<BeltRank> _BeltRankRepository;
        private readonly IRepository<Payment> _PaymentRepository;
        private readonly IRepository<Person> _PersonRepository;

        public BeltTestController(IRepository<BeltTest> BeltTestRepository,
            IRepository<Instructor> InstructorRepository,
            IRepository<Member> MemberRepository,
            IRepository<BeltRank> BeltRankRepository,
            IRepository<Payment> PaymentRepository,
            IRepository<Person> PersonRepository)
        {
            _BeltTestRepository = BeltTestRepository;
            _InstructorRepository = InstructorRepository;
            _MemberRepository = MemberRepository;
            _BeltRankRepository = BeltRankRepository;
            _PaymentRepository = PaymentRepository;
            _PersonRepository = PersonRepository;
        }
        // GET: BeltTestController
        public ActionResult Index()
        {
            var data = BeltTestRepository.GetAllData2();
            return View(data);
        }

        // GET: BeltTestController/Details/5
        public ActionResult Details(int id)
        {
            var BeltTest = _BeltTestRepository.Find(id);

            var data = new BeltTestBeltRankInstructorPaymentMemberPersonViewModel
            {
                Id = id,
                Result = (BeltTest.Result) ? "1" : "0",
                Date = BeltTest.Date,
                PaymentId = BeltTest.PaymentId,
                MemberId = BeltTest.MemberId,
                RankId = BeltTest.RankId,
                TestedByInstructorId = BeltTest.TestedByInstructorId,
                Amount = BeltTest.Payment.Amount,
                Name = _MemberRepository.Find(BeltTest.MemberId).Person.Name,
                InstructorName = _InstructorRepository.Find(BeltTest.TestedByInstructorId).Person.Name,
                RankName = _BeltRankRepository.Find(BeltTest.RankId).RankName,
            };
            return View(data);
        }

        // GET: BeltTestController/Create
        public ActionResult Create()
        {
            var data = new BeltTestBeltRankInstructorPaymentMemberPersonViewModel
            {
                Members = MemberRepository.GetAllData2(),
                Instructors = InstructorRepository.GetAllData2(),
                BeltRanks = _BeltRankRepository.GetAllData()
            };
            return View(data);
        }

        // POST: BeltTestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BeltTestBeltRankInstructorPaymentMemberPersonViewModel collection)
        {
            try
            {
                var payment = new Payment
                {
                    Amount = collection.Amount,
                    Date = DateTime.Now,
                    MemberId = collection.MemberId,
                };
                int paymentId = _PaymentRepository.Add(payment);
                var BeltTestBelt = new BeltTest
                {
                    MemberId = collection.MemberId,
                    TestedByInstructorId = collection.TestedByInstructorId,
                    RankId = collection.RankId,
                    Result = (collection.Result == "1") ? true : false,
                    Date = collection.Date,
                    PaymentId = paymentId

                };

                _BeltTestRepository.Add(BeltTestBelt);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BeltTestController/Edit/5
        public ActionResult Edit(int id)
        {
            var BeltTest = _BeltTestRepository.Find(id);

            var data = new BeltTestBeltRankInstructorPaymentMemberPersonViewModel
            {
                Id = id,
                Result = (BeltTest.Result) ? "1" : "0",
                Date = BeltTest.Date,
                PaymentId = BeltTest.PaymentId,
                MemberId = BeltTest.MemberId,
                RankId = BeltTest.RankId,
                TestedByInstructorId = BeltTest.TestedByInstructorId,
                Amount = BeltTest.Payment.Amount,
                Members = MemberRepository.GetAllData2(),
                Instructors = InstructorRepository.GetAllData2(),
                BeltRanks = BeltRankRepository.GetAllData2()
            };
            return View(data);
        }

        // POST: BeltTestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BeltTestBeltRankInstructorPaymentMemberPersonViewModel collection)
        {
            try
            {
             

                // إدارة الدفع
                int paymentId = collection.PaymentId;
                if (paymentId != -1)
                {
                    var payment = new Payment
                    {
                        PaymentId = paymentId,
                        Amount = collection.Amount,
                        Date = DateTime.Now,
                        MemberId = collection.MemberId,
                    };
                    _PaymentRepository.Edit(paymentId, payment);
                }
                else
                {
                    var payment = new Payment
                    {
                        Amount = collection.Amount,
                        Date = DateTime.Now,
                        MemberId = collection.MemberId,
                    };
                    paymentId = _PaymentRepository.Add(payment);
                }

              //  beltTest = null;
                // إنشاء كائن الاختبار المحرر
                var beltTestEdit = new BeltTest
                {
                    TestId = id,
                    MemberId = collection.MemberId,
                    TestedByInstructorId = collection.TestedByInstructorId,
                    RankId = collection.RankId,
                    Result = collection.Result == "1", // تحويل مباشرة إلى bool
                    Date = collection.Date,
                    PaymentId = paymentId,
                };

                // تحديث اختبار الحزام
                _BeltTestRepository.Edit(id, beltTestEdit);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // تسجيل الخطأ
                Console.WriteLine($"Error: {ex.Message}");
                // عرض صفحة الخطأ مع الرسالة أو إعادة توجيه إلى صفحة معينة
                return View("Error");
            }
        }

        // GET: BeltTestController/Delete/5
        public ActionResult Delete(int id)
        {
            var BeltTest = _BeltTestRepository.Find(id);

            var data = new BeltTestBeltRankInstructorPaymentMemberPersonViewModel
            {
                Id = id,
                Result = (BeltTest.Result) ? "1" : "0",
                Date = BeltTest.Date,
                PaymentId = BeltTest.PaymentId,
                MemberId = BeltTest.MemberId,
                RankId = BeltTest.RankId,
                TestedByInstructorId = BeltTest.TestedByInstructorId,
                Amount = BeltTest.Payment.Amount,
                Name = _MemberRepository.Find(BeltTest.MemberId).Person.Name,
                InstructorName = _InstructorRepository.Find(BeltTest.TestedByInstructorId).Person.Name,
                RankName = _BeltRankRepository.Find(BeltTest.RankId).RankName,
            };
            return View(data);
        }

        // POST: BeltTestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BeltTestBeltRankInstructorPaymentMemberPersonViewModel collection)
        {
            try
            {
                _BeltTestRepository.Delete(id);

                _PaymentRepository.Delete(collection.PaymentId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
