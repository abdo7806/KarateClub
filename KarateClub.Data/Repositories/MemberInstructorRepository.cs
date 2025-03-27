using KarateClub.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Data.Repositories
{
    public class MemberInstructorRepository : IRepository<MemberInstructor>
    {
        private ApplicationDbContext2 db;
        private MemberInstructor _table;
        public MemberInstructorRepository()
        {
            db = new ApplicationDbContext2();

        }


        public int Add(MemberInstructor table)
        {
            if (db.Database.CanConnect())
            {
                db.MemberInstructors.Add(table);
                db.SaveChanges();
                return table.MemberId;
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
                    db.MemberInstructors.Remove(_table);
                    db.SaveChanges();
                }
                return _table.MemberId;

            }

            return -1;
        }

        public int Edit(int Id, MemberInstructor table)
        {
            if (db.Database.CanConnect())
            {
                db.MemberInstructors.Update(table);
                db.SaveChanges();
                return table.MemberId;
            }
            else
                return -1;
        }

        public MemberInstructor Find(int Id)
        {
            if (db.Database.CanConnect())
                return db.MemberInstructors.Where(x => x.MemberId == Id).First();
            else
                return null;
        }
        // ارجاع عن طريق رقن الطبيب و رقم المريض
        public static MemberInstructor FindByMemberIdAndInstructorId(int MemberId, int InstructorId)
        {
            MemberInstructorRepository memberInstructor = new MemberInstructorRepository();
            if (memberInstructor.db.Database.CanConnect())
            {
                var membersInstructor = memberInstructor.db.MemberInstructors.Where(x => (x.MemberId == MemberId && x.InstructorId == InstructorId))
.Include(m => m.Member.Person)
.Include(m => m.Instructor.Person)
.First();
                return membersInstructor;
            }
            else
                return null;
        }
 
        public List<MemberInstructor> GetAllData()
        {

            /*SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_GetAllMemberInstructor", connection);
            List<MemberInstructor> list = new List<MemberInstructor>();

            try
            {


                connection.Open();

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader result = command.ExecuteReader();

                // bool personExists = (int)returnParameter.Value == 1


                while (result.Read())
                {
                    MemberInstructor person = new MemberInstructor();

                    person.MemberInstructorId = (int)result["MemberInstructorID"];
                    person.Name = (string)result["Name"];
                    //Email: allows null in database so we should handle null
                    if (result["Address"] != DBNull.Value)
                    {
                        person.Address = (string)result["Address"];
                    }
                    else
                    {
                        person.Address = "";
                    }
                    person.ContactInfo = (string)result["ContactInfo"];



                    list.Add(person);
                }

            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }*/

            if (db.Database.CanConnect())
            {
                var membersInstructor = db.MemberInstructors
.Include(m => m.Member.Person) // تأكد من أن Person هي خاصية علاقة
.Include(m => m.Instructor.Person) // تأكد من أن LastBeltRank هي خاصية علاقة
.ToList();
                // return list;
                return membersInstructor;
            }
            return null;
        }

        public List<MemberInstructor> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<MemberInstructor> Search(string SerachItem)
        {
            throw new NotImplementedException();
        }


        public static void Delete(int MemberId, int InstructorId)
        {
            MemberInstructorRepository memberInstructor = new MemberInstructorRepository();
            if (memberInstructor.db.Database.CanConnect())
            {
                MemberInstructor table = FindByMemberIdAndInstructorId(MemberId, InstructorId);
                if (table != null)
                {
                    memberInstructor.db.MemberInstructors.Remove(table);
                    memberInstructor.db.SaveChanges();
                }
            }

        }

    }
}

