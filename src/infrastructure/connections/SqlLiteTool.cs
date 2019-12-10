using System.Data.SQLite;
using System.IO;

namespace infrastructure.connections
{
    public class SqlLiteTool
    {
        public static void CreateDatabase(string databaseName)
        {
            if (File.Exists(databaseName))
                return;
            Directory.CreateDirectory(Path.GetDirectoryName(databaseName));
            using var _ = File.Create(databaseName);
        }
        
        public static void CreateTable(IConnection connection, string tablePath)
        {
            connection.Execute(cnx =>
            {
                var table = File.ReadAllText(tablePath);
                using var cmd = new SQLiteCommand((SQLiteConnection) cnx)
                {
                    CommandText = table
                };
                cmd.ExecuteNonQuery();
            });
        }
    }
}
