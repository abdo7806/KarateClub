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
    
    public class MemberRepository : IRepository<Member>
    {
        private ApplicationDbContext2 db;
        private Member _table;
        public MemberRepository()
        {
            db = new ApplicationDbContext2();

        }


        public int Add(Member table)
        {
            if (db.Database.CanConnect())
            {
                db.Members.Add(table);
                db.SaveChanges();
                return table.MemberId;
            }
            else
                return -1;
        }

        public int Delete(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteMember", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@MemberID", (object)Id ?? DBNull.Value);

                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                            return Id;
                        else
                            return -1;

                        //connection.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($" Person doesn't Exist   {ex.Message}");
                return -1;
            }
        }

        public int Edit(int Id, Member table)
        {
            if (db.Database.CanConnect())
            {
                db.Members.Update(table);
                db.SaveChanges();
                return table.MemberId;
            }
            else
                return -1;
        }

        public Member Find(int Id)
        {
            /*if (db.Database.CanConnect())
            {

                // تأكد من وجود أقواس حول الكود
                var member = db.Members.Where(x => x.MemberId == Id)
                .Include(m => m.Person) // تأكد من أن Person هي خاصية علاقة
                .Include(m => m.LastBeltRankNavigation).First(); // تأكد من أن LastBeltRankNavigation هي خاصية علاقة
                 
                return member;
            }
            else
                return null;*/



            Member Member = new Member();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetAllMemberByMemberID", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@MemberID", Id);



                        // command.Parameters.Add(returnParameter);
                        SqlDataReader result = command.ExecuteReader();

                        if (result.Read())
                        {
                            Member.Person = new Person();
                            Member.LastBeltRankNavigation = new BeltRank();
                            //string a = (string)result["Name"];
                            Member.MemberId = (int)result["MemberID"];
                            Member.PersonId = (int)result["PersonId"];
                            Member.EmergencyContactInfo = (string)result["EmergencyContactInfo"];
                            Member.LastBeltRank = (int)result["LastBeltRank"];
                            Member.IsActive = (bool)result["IsActive"];
                            Member.LastBeltRankNavigation.RankName = (string)result["RankName"];
                            Member.Person.Name = (string)result["Name"];

                            //Email: allows null in database so we should handle null
                            if (result["Address"] != DBNull.Value)
                            {
                                Member.Person.Address = (string)result["Address"];
                            }
                            else
                            {
                                Member.Person.Address = "";
                            }
                            Member.Person.ContactInfo = (string)result["ContactInfo"];




                        }
                        else
                        {

                            return null;

                        }



                        connection.Close();

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($" Person doesn't Exist   {ex.Message}");
                return null;
            }

            return Member;
        }

        public List<Member> GetAllData()
        {

            /*   if (db.Database.CanConnect())
               {

                   var members = db.Members
         .Include(m => m.Person) // تأكد من أن Person هي خاصية علاقة
         .Include(m => m.LastBeltRankNavigation) // تأكد من أن LastBeltRank هي خاصية علاقة
         .ToList(); 
                   return members;
               }
               return null;*/

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_GetAllMember", connection);
            List<Member> list = new List<Member>();

            try
            {


                connection.Open();

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader result = command.ExecuteReader();

                // bool MemberPersonBeltRankViewModelExists = (int)returnParameter.Value == 1


                while (result.Read())
                {
                    Member Member = new Member();
                    Member.Person = new Person();
                    Member.LastBeltRankNavigation = new BeltRank();
                    //string a = (string)result["Name"];
                    Member.MemberId = (int)result["MemberID"];
                    Member.EmergencyContactInfo = (string)result["EmergencyContactInfo"];
                    Member.LastBeltRank = (int)result["LastBeltRank"];
                    Member.IsActive = (bool)result["IsActive"];
                    Member.LastBeltRankNavigation.RankName = (string)result["RankName"];
                    Member.Person.Name = (string)result["Name"];

                    //Email: allows null in database so we should handle null
                    if (result["Address"] != DBNull.Value)
                    {
                        Member.Person.Address = (string)result["Address"];
                    }
                    else
                    {
                        Member.Person.Address = "";
                    }
                    Member.Person.ContactInfo = (string)result["ContactInfo"];


                    list.Add(Member);
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

        public List<Member> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Member> Search(string SerachItem)
        {
            if (db.Database.CanConnect())
            {
                var member = GetAllData().Where(

                    x => x.Person.Name.Contains(SerachItem)
                || x.PersonId.ToString().Contains(SerachItem)
                || x.MemberId.ToString().Contains(SerachItem)
                || x.LastBeltRank.ToString().Contains(SerachItem)
                || x.LastBeltRankNavigation.RankName.ToString().Contains(SerachItem)
                || x.Person.ContactInfo.ToString().Contains(SerachItem)
                ).ToList();

                return member;

                /*return db.Members.Where(

                    x => x.Person.Name.Contains(SerachItem)
                || x.PersonId.ToString().Contains(SerachItem)
                || x.MemberId.ToString().Contains(SerachItem)
                || x.LastBeltRank.ToString().Contains(SerachItem)
                || x.LastBeltRankNavigation.RankName.ToString().Contains(SerachItem)
                || x.Person.ContactInfo.ToString().Contains(SerachItem)
                ).ToList();*/
            }
            return null;
        }


        public static List<MemberPersonBeltRankViewModel> GetAllData2()
        {

             SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

             SqlCommand command = new SqlCommand("SP_GetAllMember", connection);
             List<MemberPersonBeltRankViewModel> list = new List<MemberPersonBeltRankViewModel>();

             try
             {


                 connection.Open();

                 command.CommandType = CommandType.StoredProcedure;

                 SqlDataReader result = command.ExecuteReader();

                 // bool MemberPersonBeltRankViewModelExists = (int)returnParameter.Value == 1


                 while (result.Read())
                 {
                     MemberPersonBeltRankViewModel MemberPersonBeltRankViewModel = new MemberPersonBeltRankViewModel();

                     MemberPersonBeltRankViewModel.MemberPersonBeltRankViewModelId = (int)result["MemberID"];
                     MemberPersonBeltRankViewModel.Name = (string)result["Name"];
                     MemberPersonBeltRankViewModel.EmergencyContactInfo = (string)result["EmergencyContactInfo"];
                     MemberPersonBeltRankViewModel.LastBeltRank = (int)result["LastBeltRank"];
                     MemberPersonBeltRankViewModel.IsActive = (bool)result["IsActive"];
                     MemberPersonBeltRankViewModel.RankName = (string)result["RankName"];

                     //Email: allows null in database so we should handle null
                     if (result["Address"] != DBNull.Value)
                     {
                         MemberPersonBeltRankViewModel.Address = (string)result["Address"];
                     }
                     else
                     {
                         MemberPersonBeltRankViewModel.Address = "";
                     }
                     MemberPersonBeltRankViewModel.ContactInfo = (string)result["ContactInfo"];



                     list.Add(MemberPersonBeltRankViewModel);
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
