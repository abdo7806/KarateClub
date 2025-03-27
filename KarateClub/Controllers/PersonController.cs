using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KarateClub.Core;
using KarateClub.Data;
using KarateClub.Data.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
namespace KarateClub.Controllers
{
    public class PersonController : Controller
    {
        private readonly IRepository<Person> _PersonRepository;
        public int countRows = 5;
        public PersonController(IRepository<Person> PersonRepository)
        {
            _PersonRepository = PersonRepository;
        }
        // [HttpGet("search")]

        // GET: PersonController
        public ActionResult Index(int? id)
        {
            var data = _PersonRepository.GetAllData();

            if (id == 0 || id == null)
            {
                var people = data.Take(10);
                return View(people);
            }
            else
            {
                var people = data.Where(x => x.PersonId > id).Take(10);
                return View(people);
            }

        }


      
        // GET: PersonController/Details/5
        public ActionResult Details(int id)
        {
            return View(_PersonRepository.Find(id));
        }

        // GET: PersonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person collection)
        {
            try
            {
                _PersonRepository.Add(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_PersonRepository.Find(id));
        }

        // POST: PersonController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Person collection)
        {
            try
            {
                _PersonRepository.Edit(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_PersonRepository.Find(id));
        }

        // POST: PersonController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _PersonRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("searchPerson")]
        public ActionResult search(string SearchItem)
        {
          
            if (string.IsNullOrEmpty(SearchItem))
            {
                var results2 = _PersonRepository.GetAllData();

                var html2 = "";
                foreach (var item in results2)
                {
                    html2 += $"<tr><td>{item.PersonId}</td><td>{item.Name}</td><td>{item.Address}</td><td>{item.ContactInfo}</td>" +
                                 $"<td> <a  href='/Person/Edit/{item.PersonId}'>Edit</a> |" +
                    $"<a href='/Person/Details/{item.PersonId}'>Details</a> |" +
                    $"<a href='/Person/Delete/{item.PersonId}'>Delete</a> " +
                    $"</td> " +
                    $"</tr>";
                }

                return Content(html2);
            }

            var results = _PersonRepository.Search(SearchItem);

            if (!results.Any())
            {
                return Content("<tr><td colspan='3'>لا توجد نتائج مطابقة.</td></tr>");
            }

            var html = "";
            foreach (var item in results)
            {
                html += $"<tr><td>{item.PersonId}</td><td>{item.Name}</td><td>{item.Address}</td><td>{item.ContactInfo}</td>" +
                    $"<td> <a  href='/Person/Edit/{item.PersonId}'>Edit</a> |" +
                    $"<a href='/Person/Details/{item.PersonId}'>Details</a> |" +
                    $"<a href='/Person/Delete/{item.PersonId}'>Delete</a> " +
                    $"</td> " +
                    $"</tr>";
            }

            return Content(html);

        }

        [HttpGet("LoadPersons")]
        public ActionResult LoadPersons(int page, int pageSize = 5)
        {
            var data = _PersonRepository.GetAllData();

            var people = data.Skip((page - 1) * pageSize).Take(pageSize).ToList();

  


            if (people.Count == 0 || people == null)
            {
                return Content("<tr><td colspan='3'>لا توجد نتائج مطابقة.</td></tr>");
            }

            var html = "";
            foreach (var item in people)
            {
                html += $"<tr><td>{item.PersonId}</td><td>{item.Name}</td><td>{item.Address}</td><td>{item.ContactInfo}</td></tr>";
            }

            return Content(html);

        }


        [HttpGet("LoadPersons2")]

        public IActionResult Find(int id)
        {
            var person = _PersonRepository.Find(id);
            if (person == null)
            {
                return PartialView("_PersonPartial", null);
            }
            return PartialView("_PersonPartial", person);
        }

    }

    
}
