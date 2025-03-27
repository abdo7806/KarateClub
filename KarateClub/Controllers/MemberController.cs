using KarateClub.Core;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KarateClub.Core.ViewModel;
namespace KarateClub.Controllers
{
    public class MemberController : Controller
    {
        private readonly IRepository<Member> _MemberRepository;
        private readonly IRepository<Person> _PersonRepository;
        private readonly IRepository<BeltRank> _BeltRankRepository;

        public MemberController(IRepository<Member> MemberRepository,
            IRepository<Person> PersonRepository,
            IRepository<BeltRank> BeltRankRepository)
        {
            _MemberRepository = MemberRepository;
            _PersonRepository = PersonRepository;
            _BeltRankRepository = BeltRankRepository;
        }
        // GET: MemberController
        public ActionResult Index(int? id)
        {
            var data = _MemberRepository.GetAllData();
            // var person = _PersonRepository.GetAllData();
            // var beltRank = _BeltRankRepository.GetAllData();
            /*var MemberPersonBeltRankViewModel = new MemberPersonBeltRankViewModel
              {
                  MemberId = member.
              };*/
            // var data = _AppointmentRepositories.GetAll();

         /*   member.ForEach(data =>
            {
                //int PatientPersonId = _PatientsRepositories.Find(data.PatientId).PersonId;
                data.Doctor.Person = _PersonRepository.Find(data.Doctor.PersonId);
                data.Patient.Person = _PersonRepository.Find(data.Patient.PersonId);
                // data.Payment = _PaymentRepositories.Find(data.Doctor.PersonId);
            });*/

            if (id == 0 || id == null)
            {
                var member = data.Take(10);
                return View(member);
            }
            else
            {
                var member = data.Where(x => x.MemberId > id).Take(10);
                return View(member);
            }

        }

        // GET: MemberController/Details/5
        public ActionResult Details(int id)
        {
            return View(_MemberRepository.Find(id));
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            var MemberPersonBeltRankViewModel = new MemberPersonBeltRankViewModel
            {
                BeltRanks = _BeltRankRepository.GetAllData(),
                IsActive =true,
            };
            return View(MemberPersonBeltRankViewModel);
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberPersonBeltRankViewModel collection)
        {
            try
            {
                var person = new Person
                {
                    Name = collection.Name,
                    Address = collection.Address,
                    ContactInfo = collection.ContactInfo,
                };

                var PersonId = _PersonRepository.Add(person);

                var member = new Member
                {
                    PersonId = PersonId,
                    EmergencyContactInfo = collection.EmergencyContactInfo,
                    LastBeltRank = collection.LastBeltRank,
                    IsActive = collection.IsActive,
                };

                var MemberId = _MemberRepository.Add(member);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(int id)
        {
            var member = _MemberRepository.Find(id);
            var MemberPersonBeltRankViewModel = new MemberPersonBeltRankViewModel
            {
                MemberPersonBeltRankViewModelId = member.MemberId,
                PersonId = member.PersonId,
                LastBeltRank= member.LastBeltRank,
                Name = member.Person.Name,
                Address = member.Person.Address,
                EmergencyContactInfo= member.EmergencyContactInfo,
                ContactInfo = member.Person.ContactInfo,

                BeltRanks = _BeltRankRepository.GetAllData(),
                IsActive = true,
            };
            return View(MemberPersonBeltRankViewModel);
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MemberPersonBeltRankViewModel collection)
        {
            try
            {

                var PersonId = collection.PersonId;
                var person = new Person
                {
                    PersonId = PersonId,
                    Name = collection.Name,
                    Address = collection.Address,
                    ContactInfo = collection.ContactInfo,
                };

                var Id = _PersonRepository.Edit(PersonId, person);

                var member = new Member
                {
                    MemberId = id,
                    PersonId = PersonId,
                    EmergencyContactInfo = collection.EmergencyContactInfo,
                    LastBeltRank = collection.LastBeltRank,
                    IsActive = collection.IsActive,
                };

                var MemberId = _MemberRepository.Edit(id, member);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_MemberRepository.Find(id));
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Member collection)
        {
            try
            {
                var PersonId = collection.PersonId;

                _MemberRepository.Delete(id);
                _PersonRepository.Delete(PersonId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet("searchMember")]
        public ActionResult search(string SearchItem)
        {

            if (string.IsNullOrEmpty(SearchItem))
            {
                var results2 = _MemberRepository.GetAllData();

                var html2 = "";
                foreach (var item in results2)
                {
                    html2 += $"<tr><td>{item.MemberId}</td><td>{item.Person.Name}</td>" +
                        $"<td>{item.Person.ContactInfo}</td><td>{item.EmergencyContactInfo}</td>" +
                        $"<td>{item.LastBeltRankNavigation.RankName}</td><td>{item.IsActive}</td>" +
                  $"<td> <a  href='/Member/Edit/{item.MemberId}'>Edit</a> |" +
                    $"<a href='/Member/Details/{item.MemberId}'>Details</a> |" +
                    $"<a href='/Member/Delete/{item.MemberId}'>Delete</a> " +
                    $"</td> " +
                    $"</tr>";
         
                }

                return Content(html2);
            }

            var results = _MemberRepository.Search(SearchItem);

            if (!results.Any())
            {
                return Content("<tr><td colspan='3'>لا توجد نتائج مطابقة.</td></tr>");
            }

            var html = "";
            foreach (var item in results)
            {
                html += $"<tr><td>{item.MemberId}</td><td>{item.Person.Name}</td>" +
                        $"<td>{item.Person.ContactInfo}</td><td>{item.EmergencyContactInfo}</td>" +
                        $"<td>{item.LastBeltRankNavigation.RankName}</td><td>{item.IsActive}</td>" +
                              $"<td> <a  href='/Member/Edit/{item.MemberId}'>Edit</a> |" +
                    $"<a href='/Member/Details/{item.MemberId}'>Details</a> |" +
                    $"<a href='/Member/Delete/{item.MemberId}'>Delete</a> " +
                    $"</td> " +
                    $"</tr>";
            }

            return Content(html);

        }




    }
}
