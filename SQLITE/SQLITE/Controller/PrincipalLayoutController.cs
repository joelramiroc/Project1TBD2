using SQLite.Services;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace SQLITE.Controller
{
    class PrincipalLayoutController
    {
        DataBaseConnection db;
        public PrincipalLayoutController() 
        {
        }

        public async Task<string> ConsultaLectura(string  consulta)
        {
            SQLiteCommand sQLiteCommand = new SQLiteCommand(consulta, this.db.SQLiteConnection);
            var resultRead = sQLiteCommand.ExecuteReader();

            return string.Empty;
        }

        public async Task<bool> OpenDataBase(string path, string databaseName)
        {
            return true;
        }

        public async Task<bool> CloseDatabase(string path, string databaseName)
        {
            return true;
        }

    }
}
