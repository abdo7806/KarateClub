using KarateClub.Core;
using KarateClub.Core.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Data.Repositories
{
    
    public class BeltRankRepository : IRepository<BeltRank>
    {
        private ApplicationDbContext2 db;
        private BeltRank _table;
        public BeltRankRepository()
        {
            db = new ApplicationDbContext2();

        }


        public int Add(BeltRank table)
        {
         /*   if (db.Database.CanConnect())
            {
                db.BeltRanks.Add(table);
                db.SaveChanges();
                return table.RankId;
            }
            else
                return -1;*/

            int RankId = -1;

            try
            {

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand("SP_AddNewBeltRank", connection);
                //إعداد الأمر
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                // إضافة المعلمات
                command.Parameters.AddWithValue("@RankName", table.RankName);
                command.Parameters.AddWithValue("@TestFees", table.TestFees);

                // إعداد معلمة الخرج
                SqlParameter outputIdParam = new SqlParameter("@RankId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outputIdParam);


                // Execute
                //تنفيذ الأمر
                connection.Open();
                command.ExecuteNonQuery();


                // Retrieve the ID of the new person
                //استرجاع معرف الشخص الجديد 
                RankId = (int)command.Parameters["@RankId"].Value;
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Person doesn't Exist   {ex.Message}");
                return -1;
            }
            return RankId;
        }

        public int Delete(int Id)
        {
            /*  if (db.Database.CanConnect())
              {
                  _table = Find(Id);
                  if (_table != null)
                  {
                      db.BeltRanks.Remove(_table);
                      db.SaveChanges();
                  }
                  return _table.RankId;

              }

              return -1;*/

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteBeltRank", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@RankId", (object)Id ?? DBNull.Value);

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

        public int Edit(int Id, BeltRank table)
        {
            /* if (db.Database.CanConnect())
             {
                 db.BeltRanks.Update(table);
                 db.SaveChanges();
                 return table.RankId;
             }
             else
                 return -1;*/

            try
            {
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand("SP_UpdateBeltRank", connection);
                //إعداد الأمر
                command.CommandType = CommandType.StoredProcedure;



                command.Parameters.AddWithValue("@RankId", table.RankId);

                command.Parameters.AddWithValue("@RankName", table.RankName);
                command.Parameters.AddWithValue("@TestFees", table.TestFees);

                // Execute
                //تنفيذ الأمر
                connection.Open();
                int result = command.ExecuteNonQuery();

                // bool personExists = (int)returnParameter.Value == 1;


                if (result > 0)
                    return Id;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Person doesn't Exist   {ex.Message}");
                return -1;
            }
        }

        public BeltRank Find(int Id)
        {
            /* if (db.Database.CanConnect())
             return db.BeltRanks.Where(x => x.RankId == Id).First();
             else
                 return null;*/
            BeltRank BeltRank;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetBeltRankByRankID", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@RankID", Id);



                        // command.Parameters.Add(returnParameter);
                        SqlDataReader result = command.ExecuteReader();

                        if (result.Read())
                        {
                             BeltRank = new BeltRank
                            {
                                RankId = (int)result["RankId"],
                                RankName = (string)result["RankName"],
                                TestFees = (decimal)result["TestFees"],
                            };
                      

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

            return BeltRank;

        }

        public List<BeltRank> GetAllData()
        {
            List<BeltRank> list = new List<BeltRank>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllBeltRank", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            BeltRank BeltRank = new BeltRank
                            {
                                RankId = (int)result["RankId"],
                                RankName = (string)result["RankName"],
                                TestFees = (decimal)result["TestFees"],

                            };

                            list.Add(BeltRank);
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

            /*  if (db.Database.CanConnect())
              {
                  // return list;
                  return db.BeltRanks.ToList();
              }
              return null;*/
        }

        public List<BeltRank> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<BeltRank> Search(string SerachItem)
        {
            throw new NotImplementedException();
        }


        public static List<BeltRank> GetAllData2()
        {
            List<BeltRank> list = new List<BeltRank>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllBeltRank", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            BeltRank BeltRank = new BeltRank
                            {
                                RankId = (int)result["RankId"],
                                RankName = (string)result["RankName"],
                                TestFees = (decimal)result["TestFees"],
                               
                            };
                          
                            list.Add(BeltRank);
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
