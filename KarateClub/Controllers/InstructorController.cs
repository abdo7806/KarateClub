using KarateClub.Core;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KarateClub.Core.ViewModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KarateClub.Controllers
{
    public class InstructorController : Controller
    {
        
        private readonly IRepository<Instructor> _InstructorRepository;
        private readonly IRepository<Person> _PersonRepository;
        private readonly IRepository<BeltRank> _BeltRankRepository;

        public InstructorController(IRepository<Instructor> InstructorRepository,

            IRepository<Person> PersonRepository,
            IRepository<BeltRank> BeltRankRepository)
        {
            _InstructorRepository = InstructorRepository;
            _PersonRepository = PersonRepository;
            _BeltRankRepository = BeltRankRepository;
        }

        // GET: InstructorController
        public ActionResult Index(int? id)
        {
            var data = _InstructorRepository.GetAllData();
            if (id == 0 || id == null)
            {
                var Instructor = data.Take(10);
                return View(Instructor);
            }
            else
            {
                var Instructor = data.Where(x => x.InstructorId > id).Take(10);
                return View(Instructor);
            }
        }

        // GET: InstructorController/Details/5
        public ActionResult Details(int id)
        {
            return View(_InstructorRepository.Find(id));
        }

        // GET: InstructorController/Create
        public ActionResult Create()
        {
            var InstructorPersonBeltRankViewModel = new InstructorPersonBeltRankViewModel
            {
                BeltRanks = _BeltRankRepository.GetAllData()
            };
            return View(InstructorPersonBeltRankViewModel);
        }

        // POST: InstructorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstructorPersonBeltRankViewModel collection)
        {
            try
            {
                var Person = new Person
                {
                    Name = collection.Name,
                    Address = collection.Address,
                    ContactInfo = collection.ContactInfo,
                };

                int PersonId = _PersonRepository.Add(Person);

                var Instructor = new Instructor
                {
                    PersonId = PersonId,
                    Qualification = collection.Qualification,
                };

                int InstructorId = _InstructorRepository.Add(Instructor);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstructorController/Edit/5
        public ActionResult Edit(int id)
        {
            var Instructor = _InstructorRepository.Find(id);
            var Person = _PersonRepository.Find(Instructor.PersonId);
            var InstructorPersonBeltRankViewModel = new InstructorPersonBeltRankViewModel
            {
                Id = id,
                PersonId = Person.PersonId,
                Name = Person.Name,
                Address = Person.Address,
                ContactInfo = Person.ContactInfo,
                Qualification = Instructor.Qualification,
                BeltRanks = _BeltRankRepository.GetAllData()
            };
            return View(InstructorPersonBeltRankViewModel);
        }

        // POST: InstructorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InstructorPersonBeltRankViewModel collection)
        {
            try
            {
                var Person = new Person
                {
                    PersonId = collection.PersonId,
                    Name = collection.Name,
                    Address = collection.Address,
                    ContactInfo = collection.ContactInfo,
                };

                _PersonRepository.Edit(collection.PersonId, Person);

                var Instructor = new Instructor
                {
                    InstructorId = collection.Id,
                    PersonId = collection.PersonId,
                    Qualification = collection.Qualification,
                };

                _InstructorRepository.Edit(id, Instructor);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InstructorController/Delete/5
        public ActionResult Delete(int id)
        {

            return View(_InstructorRepository.Find(id));
        }

        // POST: InstructorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Instructor collection)
        {
            try
            {
               // int id2 = collection.PersonId;
                _InstructorRepository.Delete(id);
               _PersonRepository.Delete(collection.PersonId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("searchInstructor")]
        public ActionResult search(string SearchItem)
        {

            if (string.IsNullOrEmpty(SearchItem))
            {
      
                var results2 = _InstructorRepository.GetAllData();

                var html2 = "";
                foreach (var item in results2)
                {
                    html2 += $"<tr><td>{item.InstructorId}</td><td>{item.Person.Name}</td>" +
                        $"<td>{item.Qualification}</td><td>{item.Person.ContactInfo}</td>" +
                      
                  $"<td> <a  href='/Instructor/Edit/{item.InstructorId}'>Edit</a> |" +
                    $"<a href='/Instructor/Details/{item.InstructorId}'>Details</a> |" +
                    $"<a href='/Instructor/Delete/{item.InstructorId}'>Delete</a> " +
                    $"</td> " +
                    $"</tr>";

                }

                return Content(html2);
            }

            var results = _InstructorRepository.Search(SearchItem);

            if (!results.Any())
            {
                return Content("<tr><td colspan='3'>لا توجد نتائج مطابقة.</td></tr>");
            }

            var html = "";
            foreach (var item in results)
            {
                html  += $"<tr><td>{item.InstructorId}</td><td>{item.Person.Name}</td>" +
                        $"<td>{item.Qualification}</td><td>{item.Person.ContactInfo}</td>" +

                  $"<td> <a  href='/Instructor/Edit/{item.InstructorId}'>Edit</a> |" +
                    $"<a href='/Instructor/Details/{item.InstructorId}'>Details</a> |" +
                    $"<a href='/Instructor/Delete/{item.InstructorId}'>Delete</a> " +
                    $"</td> " +
                    $"</tr>";
            }

            return Content(html);

        }


    }
}
