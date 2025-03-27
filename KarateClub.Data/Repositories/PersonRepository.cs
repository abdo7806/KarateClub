using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarateClub.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace KarateClub.Data.Repositories
{
    
    public class PersonRepository : IRepository<Person>
    {
        private ApplicationDbContext2 db;
        private Person _table;
        public PersonRepository()
        {
            db = new ApplicationDbContext2();

        }


        public int Add(Person table)
        {
            if (db.Database.CanConnect())
            {
                db.People.Add(table);
                db.SaveChanges();
                return table.PersonId;
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
                    db.People.Remove(_table);
                    db.SaveChanges();
                }
                return _table.PersonId;

            }
            
                return -1;
        }

        public int Edit(int Id, Person table)
        {
            if (db.Database.CanConnect())
            {
                db.People.Update(table);
                db.SaveChanges();
                return table.PersonId;
            }
            else
                return -1;
        }

        public Person Find(int Id)
        {
            if (db.Database.CanConnect())

                return db.People.Where(x => x.PersonId == Id).FirstOrDefault();
            else
                return null;
        }

        public List<Person> GetAllData()
        {

            /*SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            SqlCommand command = new SqlCommand("SP_GetAllPerson", connection);
            List<Person> list = new List<Person>();

            try
            {


                connection.Open();

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader result = command.ExecuteReader();

                // bool personExists = (int)returnParameter.Value == 1


                while (result.Read())
                {
                    Person person = new Person();

                    person.PersonId = (int)result["PersonID"];
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
                // return list;
                return db.People.ToList();
            }
            return null;
        }

        public List<Person> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Person> Search(string SerachItem)
        {
            if (db.Database.CanConnect())
            {
                return db.People.Where(x => x.Name.Contains(SerachItem)
                || x.PersonId.ToString().Contains(SerachItem)
                || x.Address.ToString().Contains(SerachItem)
                || x.ContactInfo.ToString().Contains(SerachItem)
                ).ToList();
            }
            return null;
        }

 
    }
}
