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
    public class BeltTestRepository : IRepository<BeltTest>
    {
        private ApplicationDbContext2 db;
        private BeltTest _table;
        public BeltTestRepository()
        {
            db = new ApplicationDbContext2();

        }


        public int Add(BeltTest table)
        {
            if (db.Database.CanConnect())
            {
                db.BeltTests.Add(table);
                db.SaveChanges();
                return table.TestId;
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
                    db.BeltTests.Remove(_table);
                    db.SaveChanges();
                }
                return _table.TestId;

            }

            return -1;
        }

        public int Edit(int Id, BeltTest table)
        {
            if (db.Database.CanConnect())
            {
                db.BeltTests.Update(table);
                db.SaveChanges();
                return table.TestId;
            }
            else
                return -1;
        }

        public BeltTest Find(int Id)
        {
            if (db.Database.CanConnect())
            {
                var BeltTest = db.BeltTests.Where(x => x.TestId == Id)
                    .Include(B => B.Payment).First();

                return BeltTest;
            }
            else
                return null;
        }

        public List<BeltTest> GetAllData()
        {

            /*SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_GetAllBeltTest", connection);
            List<BeltTest> list = new List<BeltTest>();

            try
            {


                connection.Open();

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader result = command.ExecuteReader();

                // bool BeltTestExists = (int)returnParameter.Value == 1


                while (result.Read())
                {
                    BeltTest BeltTest = new BeltTest();

                    BeltTest.BeltTestId = (int)result["BeltTestID"];
                    BeltTest.Name = (string)result["Name"];
                    //Email: allows null in database so we should handle null
                    if (result["Address"] != DBNull.Value)
                    {
                        BeltTest.Address = (string)result["Address"];
                    }
                    else
                    {
                        BeltTest.Address = "";
                    }
                    BeltTest.ContactInfo = (string)result["ContactInfo"];



                    list.Add(BeltTest);
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
                // return list;
                return db.BeltTests.ToList();
            }
            return null;
        }

        public List<BeltTest> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<BeltTest> Search(string SerachItem)
        {
            throw new NotImplementedException();
        }



        public static List<BeltTestBeltRankInstructorPaymentMemberPersonViewModel> GetAllData2()
        {
            List<BeltTestBeltRankInstructorPaymentMemberPersonViewModel> list = new List<BeltTestBeltRankInstructorPaymentMemberPersonViewModel>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllBeltTest", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            BeltTestBeltRankInstructorPaymentMemberPersonViewModel beltTest = new BeltTestBeltRankInstructorPaymentMemberPersonViewModel
                            {
                                Id = (int)result["TestID"],
                                Name = result["MemberName"] as string ?? "Unknown",
                                InstructorName = result["InstructorName"] as string ?? "N/A",
                                RankId = (int)result["RankId"],
                                RankName = result["RankName"] as string ?? "N/A",
                                Result = (string)result["Result"],
                                Date = (DateTime)result["Date"],
                                PaymentId = (int)result["PaymentId"],
                                Amount = (decimal)result["Amount"]
                            };

                            list.Add(beltTest);
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    // تسجيل أو معالجة استثناء SQL
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                }
                catch (Exception ex)
                {
                    // تسجيل أو معالجة الاستثناءات العامة
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return list; // إرجاع القائمة المحملة
        }



    }
}

