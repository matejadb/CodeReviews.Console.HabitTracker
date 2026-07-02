using Microsoft.Data.Sqlite;

namespace HabitTracker.matejadb;

class Program
{
    static string connectionString = "Data Source=habit_tracker.db";
    static void Main(string[] args)
    {

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

            Menu();
        }

    }

    private static void Menu()
    {
        Console.WriteLine("========HABIT TRACKER========\n");
        Console.WriteLine("1. Insert a new coding habit");
        Console.WriteLine("2. Update a coding habit");
        Console.WriteLine("3. View coding habits");
        Console.WriteLine("4. Delete a coding habit");
        Console.WriteLine("5. Exit\n");
        Console.WriteLine("=============================");
        Console.Write("Please select an option: ");

        string input = Console.ReadLine();

        switch(input)
        {
            case "1":
                InsertCodingHabit();
                break;
            case "2":
                UpdateCodingHabit();
                break;
            case "3":
                ViewCodingHabits();
                break;
            case "4":
                DeleteCodingHabit();
                break;
            case "5":
                Console.WriteLine("Exiting the application.");
                return;
            default:
                Console.WriteLine("Invalid option. Please try again.");
                Console.ReadKey();
                Console.Clear();
                Menu();
                break;
        }
    }

    private static void DeleteCodingHabit()
    {
        throw new NotImplementedException();
    }

    private static void ViewCodingHabits()
    {
        throw new NotImplementedException();
    }

    private static void UpdateCodingHabit()
    {
        throw new NotImplementedException();
    }

    private static void InsertCodingHabit()
    {
        throw new NotImplementedException();
    }
}
