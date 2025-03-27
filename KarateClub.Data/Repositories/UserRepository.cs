using KarateClub.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Data.Repositories
{
    
    public class UserRepository : IRepository<User>
    {
        private ApplicationDbContext2 db;
        private User _table;
        public UserRepository()
        {
            db = new ApplicationDbContext2();

        }

        // للتشفير
        private static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
        public int Add(User table)
        {
            int UserID = -1;

            try
            {

                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand("SP_AddUsers", connection);
                //إعداد الأمر
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                // إضافة المعلمات
                        string password = ComputeHash(table.PasswordHash);

                command.Parameters.AddWithValue("@Username", table.Username);
                command.Parameters.AddWithValue("@PasswordHash", password);
                command.Parameters.AddWithValue("@Role", table.Role);
                command.Parameters.AddWithValue("@PersonId", table.PersonId);
                command.Parameters.AddWithValue("@IsActive", table.IsActive);

                // إعداد معلمة الخرج
                SqlParameter outputIdParam = new SqlParameter("@UserID", SqlDbType.Int)
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
                UserID = (int)command.Parameters["@UserID"].Value;
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" Person doesn't Exist   {ex.Message}");
                return -1;
            }
            return UserID;
        }

        public int Delete(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@UserID", (object)Id ?? DBNull.Value);

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

        public int Edit(int Id, User table)
        {
            try
            {
                SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                SqlCommand command = new SqlCommand("SP_UpdateUser", connection);
                //إعداد الأمر
                command.CommandType = CommandType.StoredProcedure;

                string password = ComputeHash(table.PasswordHash);

                command.Parameters.AddWithValue("@UserID", Id);
                command.Parameters.AddWithValue("@Username", table.Username);
                command.Parameters.AddWithValue("@PasswordHash", password);
                command.Parameters.AddWithValue("@Role", table.Role);
                command.Parameters.AddWithValue("@PersonId", table.PersonId);
                command.Parameters.AddWithValue("@IsActive", table.IsActive);

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
                Console.WriteLine($" User doesn't Exist   {ex.Message}");
                return -1;
            }
        }

        public User Find(int Id)
        {

            User User = new User();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_GetUsersByUserID", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;


                        command.Parameters.AddWithValue("@UserID", Id);



                        // command.Parameters.Add(returnParameter);
                        SqlDataReader result = command.ExecuteReader();

                        if (result.Read())
                        {
                            User.Person = new Person();
                            //string a = (string)result["Name"];
                            User.UserID = (int)result["UserID"];
                            User.PasswordHash = (string)result["PasswordHash"];
                            User.Username = (string)result["Username"];
                            User.PersonId = (int)result["PersonID"];
                            User.Role = (string)result["Role"];
                            User.IsActive = (bool)result["IsActive"];
                            User.Person.Name = (string)result["Name"];
                            User.Person.ContactInfo = (string)result["ContactInfo"];

                            //Email: allows null in database so we should handle null
                            if (result["Address"] != DBNull.Value)
                            {
                                User.Person.Address = (string)result["Address"];
                            }
                            else
                            {
                                User.Person.Address = "";
                            }





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
                Console.WriteLine($" User doesn't Exist   {ex.Message}");
                return null;
            }

            return User;

        }

        public List<User> GetAllData()
        {
        

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_GetAllUsers", connection);
            List<User> list = new List<User>();

            try
            {


                connection.Open();

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader result = command.ExecuteReader();

                // bool UserPersonBeltRankViewModelExists = (int)returnParameter.Value == 1


                while (result.Read())
                {
                    User User = new User();
                    User.Person = new Person();
                    //string a = (string)result["Name"];
                    User.UserID = (int)result["UserID"];
                    User.PasswordHash = (string)result["PasswordHash"];
                    User.Username = (string)result["Username"];
                    User.PersonId = (int)result["PersonID"];
                    User.Role = (string)result["Role"];
                    User.IsActive = (bool)result["IsActive"];
                    User.Person.Name = (string)result["Name"];
                    User.Person.ContactInfo = (string)result["ContactInfo"];

                    //Email: allows null in database so we should handle null
                    if (result["Address"] != DBNull.Value)
                    {
                        User.Person.Address = (string)result["Address"];
                    }
                    else
                    {
                        User.Person.Address = "";
                    }


                    list.Add(User);
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
        

        public List<User> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<User> Search(string SerachItem)
        {
            var Users = GetAllData();

            return Users.Where(x =>
                  x.UserID.ToString().Contains(SerachItem)
                || x.Username.Contains(SerachItem)
                || x.PersonId.ToString().Contains(SerachItem)
                || x.Person.Name.ToString().Contains(SerachItem)
                ).ToList();
            
            throw new NotImplementedException();
        }





      


    }
}
