using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Data.Repositories
{
    public interface IRepository<Table>
    {
        //Read

        List<Table> GetAllData();

        List<Table> GetDataById(string Id);

        List<Table> Search(string SerachItem);

        Table Find(int Id);
        //Write

        int Add(Table table);
        int Edit(int Id, Table table);
        int Delete(int Id);

    }
}
