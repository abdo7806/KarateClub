using Microsoft.AspNetCore.Mvc;
using KarateClub.Data.Repositories;
using KarateClub.Core; // استخدم النموذج المناسب
using System;


    public class PersonSearchViewComponent : ViewComponent
    {
        private readonly IRepository<Person> _personRepository;

        public PersonSearchViewComponent(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }

       // [HttpPost]
    [HttpGet("searchPersonview")]

    public IViewComponentResult Find(int id)
        {
            try
            {
                var person = _personRepository.Find(id); // استدعاء بشكل متزامن
                return View("Default", person);
            }
            catch (Exception ex)
            {
                // سجل الخطأ أو اعرض رسالة خطأ مناسبة
                Console.WriteLine(ex.Message);
                return View("Error"); // يمكنك إنشاء عرض مخصص للأخطاء
            }
        }
    }
