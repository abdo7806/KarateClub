using KarateClub.Core;
using KarateClub.Core.ViewModel;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarateClub.Controllers
{
    public class MemberInstructorController : Controller
    {
        private readonly IRepository<MemberInstructor> _MemberInstructorRepository;
        private readonly IRepository<Instructor> _InstructorRepository;
        private readonly IRepository<Member> _MemberRepository;

        private readonly IRepository<Person> _PersonRepository;

        public MemberInstructorController(IRepository<MemberInstructor> MemberInstructorRepository,
            IRepository<Instructor> InstructorRepository,
            IRepository<Member> MemberRepository,
            IRepository<Person> PersonRepository)
        {
            _MemberInstructorRepository = MemberInstructorRepository;
            _InstructorRepository = InstructorRepository;
            _MemberRepository = MemberRepository;

            _PersonRepository = PersonRepository;
        }
        // GET: MemberInstructorController
        public ActionResult Index()
        {
            var data = _MemberInstructorRepository.GetAllData();
            return View(data);
        }

        // GET: MemberInstructorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MemberInstructorController/Create
        public ActionResult Create()
        {
            var data = new MemberInstructor
            {
                Members = MemberRepository.GetAllData2(),
                Instructors = InstructorRepository.GetAllData2(),
                AssignDate = DateTime.Now,
                

            };
            return View(data);
        }

        // POST: MemberInstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberInstructor collection)
        {
            try
            {
                _MemberInstructorRepository.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberInstructorController/Edit/5
        public ActionResult Edit(int MemberId, int InstructorId)
        {
            MemberInstructor data = new MemberInstructor();
            data = MemberInstructorRepository.FindByMemberIdAndInstructorId(MemberId, InstructorId);
            data.AssignDate = DateTime.Now;
            data.Members = MemberRepository.GetAllData2();
            data.Instructors = InstructorRepository.GetAllData2();
            return View(data);
        }

        // POST: MemberInstructorController/Edit/5
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

        // GET: MemberInstructorController/Delete/5
        public ActionResult Delete(int MemberId, int InstructorId)
        {
            var data = MemberInstructorRepository.FindByMemberIdAndInstructorId(MemberId, InstructorId);

            return View(data);
        }

        // POST: MemberInstructorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MemberInstructor collection)
        {
            try
            {
                int MemberId = collection.MemberId;
                int InstructorId = collection.InstructorId;

                // حذف الاختبار من المستودع
                // _BeltTestRepository.Delete(id);
                // return Json(new { success = true });
                MemberInstructorRepository.Delete(MemberId, InstructorId);
                TempData["Message"] = "Deleted successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["Error"] = "The deletion was not successful.";

                return View();
            }
        }
    }
}
