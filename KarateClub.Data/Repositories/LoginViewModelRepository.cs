using KarateClub.Core;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace KarateClub.Data.Repositories
{
    public class LoginViewModelRepository
    {

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

        public  User Login(LoginViewModel loginViewModel)
        {

            User User = new User();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {

                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SP_LoginUser", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;


                        string password = ComputeHash(loginViewModel.Password);

                        command.Parameters.AddWithValue("@Username", loginViewModel.Username);
                        command.Parameters.AddWithValue("@PasswordHash", password);



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

                            //Email: allows null in database so we should handle null
                        





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

    }
}
