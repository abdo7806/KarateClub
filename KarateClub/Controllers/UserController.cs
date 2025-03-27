using KarateClub.Core;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace KarateClub.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _UserRepository;
     

        public UserController(IRepository<User> UserRepository
            )
        {
            _UserRepository = UserRepository;

            

        }


      
        // GET: UserController
        public ActionResult Index(int? id)
        {
            var data = _UserRepository.GetAllData();

            if (id == 0 || id == null)
            {
                var Users = data.Take(10);
                return View(Users);
            }
            else
            {
                var Users = data.Where(x => x.UserID > id).Take(10);
                return View(Users);
            }

     
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View(_UserRepository.Find(id));
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User collection)
        {
            try
            {
                _UserRepository.Add(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            

            return View(_UserRepository.Find(id));
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, User collection)
        {
            try
            {
                _UserRepository.Edit(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _UserRepository.Delete(id);
                TempData["Message"] = "Deleted successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        [HttpGet("searchUser")]
        public ActionResult search(string SearchItem)
        {

         

            if (string.IsNullOrEmpty(SearchItem))
            {
                var results2 = _UserRepository.GetAllData();

                var html2 = "";
                foreach (var item in results2)
                {
                    html2 += $"<tr><td>{item.UserID}</td><td>{item.Username}</td><td>{item.Role}</td>" +
                        $"<td>{item.PersonId}</td> <td>{item.Person.Name}</td>" +
                        $"<td>" +
                        $"  <div class='action-links'>" +
                        $"<a  class='btn btn-primary btn-sm' href='/User/Edit/{item.UserID}'>" +
                        $" <i class='fas fa-edit'></i> Edit  </a>   " +
                        $"<a class='btn btn-info btn-sm' href='/User/Details/{item.UserID}'>" +
                        $"   <i class='fas fa-info-circle'></i> Details </a>" +
                        $"<form asp-action='Delete' method='post' style='display:inline;'" +
                        $" onsubmit='return confirmDelete();' asp-route-id='{@item.UserID}'>   " +
                        $" <span class='btn btn-danger btn-sm'>" +
                        $"<button type='submit' class='btn btn-danger btn-sm'>Delete</button>" +
                        $"  <i class='fas fa-trash-alt'></i>  " +
                        $"</span>     " +
                        $"</form>  " +
                        $"  </div>" +
                        $"</td>" +

                    $"</tr>";
                }

                return Content(html2);
            }

            var results = _UserRepository.Search(SearchItem);

            if (!results.Any())
            {
                return Content("<tr><td colspan='3'>لا توجد نتائج مطابقة.</td></tr>");
            }

            var html = "";
            foreach (var item in results)
            {
                html += $"<tr><td>{item.UserID}</td><td>{item.Username}</td><td>{item.Role}</td>" +
                        $"<td>{item.PersonId}</td> <td>{item.Person.Name}</td>" +
                        $"<td>" +
                        $"  <div class='action-links'>" +
                        $"<a  class='btn btn-primary btn-sm' href='/User/Edit/{item.UserID}'>" +
                        $" <i class='fas fa-edit'></i> Edit  </a>   " +
                        $"<a class='btn btn-info btn-sm' href='/User/Details/{item.UserID}'>" +
                        $"   <i class='fas fa-info-circle'></i> Details </a>" +
                        $"<form asp-action='Delete' method='post' style='display:inline;'" +
                        $" onsubmit='return confirmDelete();' asp-route-id='{@item.UserID}'>   " +
                        $" <span class='btn btn-danger btn-sm'>" +
                        $"<button type='submit' class='btn btn-danger btn-sm'>Delete</button>" +
                        $"  <i class='fas fa-trash-alt'></i>  " +
                        $"</span>     " +
                        $"</form>  " +
                        $"  </div>" +
                        $"</td>" +

                    $"</tr>";

               
            }

            return Content(html);

        }



    }
}
