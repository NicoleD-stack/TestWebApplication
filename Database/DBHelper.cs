using System.Data;
using System.Data.SQLite;

namespace TestWebApplication.Database
{
    public class DBHelper
    {
        private readonly IConfiguration _configuration;
        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            return _configuration.GetConnectionString("SQLite");
        }

        public SQLiteConnection GetConnection()
        {
            var connectionString = GetConnectionString();
            return new SQLiteConnection(connectionString);
        }

        public SQLiteCommand GetCommand(SQLiteConnection sqLiteConnection, string commandText, CommandType commandType)
        {
            return new SQLiteCommand
            {
                Connection = sqLiteConnection,
                CommandType = commandType,
                CommandText = commandText
            };
        }
        }

    }
