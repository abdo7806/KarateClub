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
    
    public class SubscriptionPeriodRepository : IRepository<SubscriptionPeriod>
    {
        private ApplicationDbContext2 db;
        private  SubscriptionPeriod _table;
        public  SubscriptionPeriodRepository()
        {
            db = new ApplicationDbContext2();

        }

        
        public int Add(SubscriptionPeriod table)
        {
            /* if (db.Database.CanConnect())
             {
                 db. SubscriptionPeriods.Add(table);
                 db.SaveChanges();
                 return table.PeriodId;
             }
             else*/
            int newPersonID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand command = new SqlCommand("SP_AddNewSubscriptionPeriods", connection);
            //إعداد الأمر
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                // تحقق من تواريخ العضو
                /*if (table.StartDate < new DateTime(1753, 1, 1) || table.EndDate > new DateTime(9999, 12, 31))
                {
                    throw new ArgumentOutOfRangeException("Date must be between 1/1/1753 and 12/31/9999.");
                }*/

                // Add parameters
                // إضافة المعلمات
                command.Parameters.AddWithValue("@StartDate", DateTime.Now);
                command.Parameters.AddWithValue("@EndDate", DateTime.Now.AddMonths(1));
                command.Parameters.AddWithValue("@Fees", table.Fees);
                command.Parameters.AddWithValue("@Paid", table.Paid);
                command.Parameters.AddWithValue("@MemberID", table.MemberId);

                if (table.PaymentId != null)
                {
                    command.Parameters.AddWithValue("@PaymentID", table.PaymentId);
                }
                else
                {
                    command.Parameters.AddWithValue("@PaymentID", DBNull.Value);
                }
                
                // إعداد معلمة الخرج
                SqlParameter outputIdParam = new SqlParameter("@NewPeriodID", SqlDbType.Int)
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
                 newPersonID = (int)command.Parameters["@NewPeriodID"].Value;
            }
            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }
            return newPersonID;
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {
                _table = Find(Id);
                if (_table != null)
                {
                    db. SubscriptionPeriods.Remove(_table);
                    db.SaveChanges();
                }
                return _table.PeriodId;

            }

            return -1;
        }

        public int Edit(int Id,  SubscriptionPeriod table)
        {
            if (db.Database.CanConnect())
            {
                db. SubscriptionPeriods.Update(table);
                db.SaveChanges();
                return table.PeriodId;
            }
            else
                return -1;
        }

        public  SubscriptionPeriod Find(int Id)
        {
            if (db.Database.CanConnect())
                return db. SubscriptionPeriods.Where(x => x.PeriodId == Id).First();
            else
                return null;
        }

        public List< SubscriptionPeriod> GetAllData()
        {

          
            if (db.Database.CanConnect())
            {
                // return list;
                return db. SubscriptionPeriods.ToList();
            }
            return null;
        }

        public List<SubscriptionPeriod> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List< SubscriptionPeriod> Search(string SerachItem)
        {
         


            throw new NotImplementedException();
        }


        public static List<SubscriptionPeriodMemberPersonBeltRankViewModel> Search2(string SerachItem)
        {
            List<SubscriptionPeriodMemberPersonBeltRankViewModel> data = GetAllDat2();

            //    data[0].Fees.ToString();
            // return data.Where(x => 
            //   x.Id.ToString() == SerachItem);
            return data.Where(x =>
              x.Id.ToString() == SerachItem ||
              x.Name.Contains(SerachItem) ||
              x.PersonId.ToString() == SerachItem ||
              x.StartDate.ToString("yyyy-MM-dd").Contains(SerachItem) || // Formatting date for better comparison
              x.Paid.ToString() == SerachItem ||
              x.Fees.ToString() == SerachItem).ToList();

            throw new NotImplementedException();
        }

        public static List<SubscriptionPeriodMemberPersonBeltRankViewModel> GetAllDat2()
        {
            List<SubscriptionPeriodMemberPersonBeltRankViewModel> list = new List<SubscriptionPeriodMemberPersonBeltRankViewModel>();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllSubscriptionPeriods", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader result = command.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            SubscriptionPeriodMemberPersonBeltRankViewModel subscriptionPeriod = new SubscriptionPeriodMemberPersonBeltRankViewModel
                            {
                                Id = (int)result["PeriodId"],
                                StartDate = (DateTime)result["StartDate"],
                                EndDate = (DateTime)result["EndDate"],
                                Fees = (decimal)result["Fees"],
                                Paid = (bool)result["Paid"],
                                Name = result["Name"] as string, // استخدام as لتجنب الاستثناء
                                EmergencyContactInfo = result["EmergencyContactInfo"] as string,
                                IsActive = (bool)result["IsActive"],
                                Address = result["Address"] as string,
                                ContactInfo = result["ContactInfo"] as string,
                                PaymentId = result.IsDBNull(result.GetOrdinal("PaymentId")) ? -1 : (int)result["PaymentId"]
                            };

                            list.Add(subscriptionPeriod);
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

            return list;
        }


        public static SubscriptionPeriodMemberPersonBeltRankViewModel Find2(int id)
        {
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllSubscriptionPeriodsByPeriodID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PeriodID", id);

                try
                {
                    connection.Open();
                    using (SqlDataReader result = command.ExecuteReader())
                    {
                        if (result.Read()) // تحقق مما إذا كان هناك سجل
                        {
                            return new SubscriptionPeriodMemberPersonBeltRankViewModel
                            {
                                Id = (int)result["PeriodId"],
                                StartDate = (DateTime)result["StartDate"],
                                EndDate = (DateTime)result["EndDate"],
                                Fees = (decimal)result["Fees"],
                                Paid = (bool)result["Paid"],
                                Name = result["Name"] as string ?? "Unknown",
                                EmergencyContactInfo = result["EmergencyContactInfo"] as string ?? "N/A",
                                IsActive = (bool)result["IsActive"],
                                Address = result["Address"] as string ?? "N/A",
                                ContactInfo = result["ContactInfo"] as string ?? "N/A",
                                MemberId = (int)result["MemberId"],
                                PersonId = (int)result["PersonId"],
                                
                                PaymentId = result.IsDBNull(result.GetOrdinal("PaymentId")) ? -1 : (int)result["PaymentId"]
                            };
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

            return null; // إرجاع null إذا لم يتم العثور على أي سجل
        }

    }
}
