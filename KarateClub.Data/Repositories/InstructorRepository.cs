using KarateClub.Core;
using KarateClub.Core.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Data.Repositories
{
    public class InstructorRepository : IRepository<Instructor>
    {
        private ApplicationDbContext2 db;
        private Instructor _table;
        public InstructorRepository()
        {
            db = new ApplicationDbContext2();

        }


        public int Add(Instructor table)
        {
            if (db.Database.CanConnect())
            {
                db.Instructors.Add(table);
                db.SaveChanges();
                return table.InstructorId;
            }
            else
                return -1;
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {
                _table = Find(Id);
                if (_table != null)
                {
                    db.Instructors.Remove(_table);
                    db.SaveChanges();
                }
                return _table.InstructorId;

            }

            return -1;
        }

        public int Edit(int Id, Instructor table)
        {
            if (db.Database.CanConnect())
            {
                db.Instructors.Update(table);
                db.SaveChanges();
                return table.InstructorId;
            }
            else
                return -1;
        }

        public Instructor Find(int Id)
        {
            if (db.Database.CanConnect())
            {

                // تأكد من وجود أقواس حول الكود
                var member = db.Instructors.Where(x => x.InstructorId == Id)
                .Include(m => m.Person).First(); // تأكد من أن Person هي خاصية علاقة

                return member;
            }
            else
                return null;
        }

        public List<Instructor> GetAllData()
        {

            
            if (db.Database.CanConnect())
            {
                //var Instructor = db.Instructors.Include(x => x.PersonId).ToList();
                // Instructor = db.Instructors.Include(x => x.LastBeltRank).ToList();
                var members = db.Instructors
      .Include(m => m.Person) // تأكد من أن Person هي خاصية علاقة
      .ToList();
                return members;
            }
            return null;
        }

        public List<Instructor> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Instructor> Search(string SerachItem)
        {
            if (db.Database.CanConnect())
            {
                var Instructor = GetAllData().Where(

                    x =>
                 x.InstructorId.ToString().Contains(SerachItem)
                || x.Person.Name.Contains(SerachItem)
                || x.PersonId.ToString().Contains(SerachItem)
                || x.InstructorId.ToString().Contains(SerachItem)
                || x.Person.ContactInfo.ToString().Contains(SerachItem)
                || x.Qualification.ToString().Contains(SerachItem)
                ).ToList();
                return Instructor;
            }
            return null;
        }




        public static List<InstructorPersonBeltRankViewModel> GetAllData2()
        {


            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_GetAllInstructor", connection);
            List<InstructorPersonBeltRankViewModel> list = new List<InstructorPersonBeltRankViewModel>();

            try
            {


                connection.Open();

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader result = command.ExecuteReader();


                while (result.Read())
                {
                    InstructorPersonBeltRankViewModel InstructorPersonBeltRankViewModel = new InstructorPersonBeltRankViewModel
                    {
                        Id = (int)result["InstructorId"],
                        Name = (string)result["Name"],
                        Address = (string)result["Address"],
                        ContactInfo = (string)result["ContactInfo"],
                        PersonId = (int)result["PersonId"],
                        Qualification = (string)result["Qualification"]

                    };


                    list.Add(InstructorPersonBeltRankViewModel);
                }

            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }


            return list;
        }



    }
}
