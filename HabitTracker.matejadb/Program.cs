using Microsoft.Data.Sqlite;

namespace HabitTracker.matejadb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=habit_tracker.db";

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"CREATE TABLE IF NOT EXISTS coding (
                  Id INTEGER PRIMARY KEY AUTOINCREMENT, 
                  Date TEXT NOT NULL,
                  Occurance INTEGER NOT NULL)";

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
