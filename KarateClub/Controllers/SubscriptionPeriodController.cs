using KarateClub.Core;
using KarateClub.Core.ViewModel;
using KarateClub.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarateClub.Controllers
{
    public class SubscriptionPeriodController : Controller
    {
        private readonly IRepository<SubscriptionPeriod> _SubscriptionPeriodRepository;
        private readonly IRepository<Member> _MemberRepository;
        private readonly IRepository<Person> _PersonRepository;
        private readonly IRepository<BeltRank> _BeltRankRepository;
        private readonly IRepository<Payment> _PaymentRepository;

        public SubscriptionPeriodController(IRepository<SubscriptionPeriod> SubscriptionPeriodRepository,
            IRepository<Member> MemberRepository,
            IRepository<Person> PersonRepository,
            IRepository<BeltRank> BeltRankRepository,
            IRepository<Payment> PaymentRepository)
        {
            _SubscriptionPeriodRepository = SubscriptionPeriodRepository;
            _MemberRepository = MemberRepository;
            _PersonRepository = PersonRepository;
            _BeltRankRepository = BeltRankRepository;
            _PaymentRepository = PaymentRepository;
        }
        // GET: SubscriptionPeriodController
        public ActionResult Index(int? id)
        {
            var data = SubscriptionPeriodRepository.GetAllDat2();

            if (id == 0 || id == null)
            {
                var people = data.Take(10);
                return View(people);
            }
            else
            {
                var people = data.Where(x => x.Id > id).Take(10);
                return View(people);
            }

        }

      

        // GET: SubscriptionPeriodController/Details/5
        public ActionResult Details(int id)
        {
            var data = SubscriptionPeriodRepository.Find2(id);
            return View(data);
        }

        // GET: SubscriptionPeriodController/Create
        public ActionResult Create()
        {
            var data = new SubscriptionPeriodMemberPersonBeltRankViewModel
            {
                Members = MemberRepository.GetAllData2().ToList(),
            };
            return View(data);
        }

        // POST: SubscriptionPeriodController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubscriptionPeriodMemberPersonBeltRankViewModel collection)
        {
            try
            {
                //var SubscriptionPeriod = new SubscriptionPeriod();
                if (collection.Paid == true)
                {
                    var payment = new Payment
                    {
                        Amount = collection.Fees,
                        Date = DateTime.Now,
                        MemberId = collection.MemberId,
                    };
                    int paymentId = _PaymentRepository.Add(payment);


                    var SubscriptionPeriod = new SubscriptionPeriod
                    {
                        StartDate = collection.StartDate,
                        EndDate = collection.EndDate,
                        Paid = collection.Paid,
                        Fees = collection.Fees,
                        MemberId = collection.MemberId,
                        PaymentId = paymentId,
                    };
                    _SubscriptionPeriodRepository.Add(SubscriptionPeriod);

                }
                else
                {
                    var SubscriptionPeriod = new SubscriptionPeriod
                    {
                        StartDate = collection.StartDate,
                        EndDate = collection.EndDate,
                        Paid = collection.Paid,
                        Fees = collection.Fees,
                        MemberId = collection.MemberId,
                    };
                    _SubscriptionPeriodRepository.Add(SubscriptionPeriod);

                }
                return Redirect("/SubscriptionPeriod/Index");

            }
            catch
            {
                return View();
            }
        }

        // GET: SubscriptionPeriodController/Edit/5
        public ActionResult Edit(int id)
        {
            var data = SubscriptionPeriodRepository.Find2(id);


            data.Members = MemberRepository.GetAllData2();
            return View(data);
        }

        // POST: SubscriptionPeriodController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubscriptionPeriodMemberPersonBeltRankViewModel collection)
        {
            try
            {
                var data = SubscriptionPeriodRepository.Find2(id);

                //if (collection.Paid == true)
                if (data.PaymentId != -1)
                {
                    var payment = new Payment
                    {
                        PaymentId = data.PaymentId,
                        Amount = collection.Fees,
                        Date = DateTime.Now,
                        MemberId = collection.MemberId,
                    };

                    int paymentId = _PaymentRepository.Edit(data.PaymentId, payment);


                    var SubscriptionPeriod = new SubscriptionPeriod
                    {
                        PeriodId = id,
                        StartDate = collection.StartDate,
                        EndDate = collection.EndDate,
                        Paid = collection.Paid,
                        Fees = collection.Fees,
                        MemberId = collection.MemberId,
                        PaymentId = data.PaymentId,
                    };
                    _SubscriptionPeriodRepository.Edit(id, SubscriptionPeriod);


                }
                else if (collection.Paid == true)
                {
                    var payment = new Payment
                    {
                        Amount = collection.Fees,
                        Date = DateTime.Now,
                        MemberId = collection.MemberId,
                    };
                    int paymentId = _PaymentRepository.Add(payment);


                    var SubscriptionPeriod = new SubscriptionPeriod
                    {
                        PeriodId = id,
                        StartDate = collection.StartDate,
                        EndDate = collection.EndDate,
                        Paid = collection.Paid,
                        Fees = collection.Fees,
                        MemberId = collection.MemberId,
                        PaymentId = paymentId,
                    };
                    _SubscriptionPeriodRepository.Edit(id, SubscriptionPeriod);

                }
                else
                {
                    var SubscriptionPeriod = new SubscriptionPeriod
                    {
                        PeriodId = id,
                        StartDate = collection.StartDate,
                        EndDate = collection.EndDate,
                        Paid = collection.Paid,
                        Fees = collection.Fees,
                        MemberId = collection.MemberId,
                    };
                    _SubscriptionPeriodRepository.Edit(id,SubscriptionPeriod);

                }
                return Redirect("/SubscriptionPeriod/Index");
             //   return RedirectToRoute(new { controller = "SubscriptionPeriod", action = "Index" });
              //  return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SubscriptionPeriodController/Delete/5
        public ActionResult Delete(int id)
        {
            var data = SubscriptionPeriodRepository.Find2(id);

            return View(data);
        }

        // POST: SubscriptionPeriodController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SubscriptionPeriodMemberPersonBeltRankViewModel collection)
        {
            try
            {
                var data = SubscriptionPeriodRepository.Find2(id);

                _SubscriptionPeriodRepository.Delete(id);

                if(data.PaymentId != -1)
                {
                    _PaymentRepository.Delete(data.PaymentId);
                }
                return Redirect("/SubscriptionPeriod/Index");

               // return RedirectToAction("Index4", "SubscriptionPeriod");

                // return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        [HttpGet("SubscriptionPeriod")]
        public ActionResult search(string SearchItem)
        {

         
            if (string.IsNullOrEmpty(SearchItem))
            {
             //   var results2 = _SubscriptionPeriodRepository.GetAllData();
                var results2 = SubscriptionPeriodRepository.GetAllDat2();


                var html2 = "";
                foreach (var item in results2)
                {
                    html2 += $"<tr><td>{item.Id}</td><td>{item.StartDate}</td><td>{item.EndDate}</td><td>{item.Fees}</td>" 
                        +$"<td>{item.Paid}</td><td>{item.IsActive}</td> " 
                        +$"<td>{item.Name}</td><td>{item.ContactInfo}</td> " +
                                 $"<td> <a  href='/SubscriptionPeriod/Edit/{item.Id}'>Edit</a> |" +
                    $"<a href='/SubscriptionPeriod/Details/{item.Id}'>Details</a> |" +
                    $"<a href='/SubscriptionPeriod/Delete/{item.Id}'>Delete</a> " +
                    $"</td> " +
                    $"</tr>";
                }

                return Content(html2);
            }

            var results = SubscriptionPeriodRepository.Search2(SearchItem);

            if (!results.Any())
            {
                return Content("<tr><td colspan='3'>لا توجد نتائج مطابقة.</td></tr>");
            }

            var html = "";
            foreach (var item in results)
            {
                html += $"<tr><td>{item.Id}</td><td>{item.StartDate}</td><td>{item.EndDate}</td><td>{item.Fees}</td>"
                    + $"<td>{item.Paid}</td><td>{item.IsActive}</td> "
                    + $"<td>{item.Name}</td><td>{item.ContactInfo}</td> " +
                             $"<td> <a  href='/SubscriptionPeriod/Edit/{item.Id}'>Edit</a> |" +
                $"<a href='/SubscriptionPeriod/Details/{item.Id}'>Details</a> |" +
                $"<a href='/SubscriptionPeriod/Delete/{item.Id}'>Delete</a> " +
                $"</td> " +
                $"</tr>";
            }

            return Content(html);

        }


    }
}
