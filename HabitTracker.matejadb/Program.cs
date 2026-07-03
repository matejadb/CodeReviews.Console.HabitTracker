using Microsoft.Data.Sqlite;

namespace HabitTracker.matejadb;

class Program
{
    static string connectionString = "Data Source=habit_tracker.db";
    static void Main(string[] args)
    {
        try
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

            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Menu();

    }

    private static void Menu()
    {
        Console.Clear();
        Console.WriteLine("========HABIT TRACKER========\n");
        Console.WriteLine("1. Insert a new coding habit");
        Console.WriteLine("2. Update a coding habit");
        Console.WriteLine("3. View coding habits");
        Console.WriteLine("4. Delete a coding habit");
        Console.WriteLine("5. Exit\n");
        Console.WriteLine("=============================");
        Console.Write("Please select an option: ");

        string input = Console.ReadLine();

        switch (input)
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
        Console.Clear();
        Console.WriteLine("==========================NEW HABIT==========================");

        string dateInput = GetDateFromUser();

        int occuranceInput = GetOccuranceFromUser();

        Console.WriteLine("=============================================================");

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO coding(Date, Occurance) VALUES('{dateInput}', '{occuranceInput}')";
                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        };

        Console.WriteLine("\nHabit inserted successfully!\nPress any key to continue...");
        Console.ReadKey();

        Menu();
    }
    private static string GetDateFromUser() 
    {
        Console.Write("\nInsert the date of the coding habit (YYYY-MM-DD): ");
        string date = Console.ReadLine();

        return date;
    }

    private static int GetOccuranceFromUser()
    {
        int occurance = -1;

        Console.Write("\nInsert the number of occurrences: ");
        do
        {
            occurance = Int32.TryParse(Console.ReadLine(), out occurance) ? occurance : -1;

            if(occurance == -1 || occurance < 0)
            {
                Console.Write("\nInvalid input. Please enter a valid number of occurrences: ");
            }
        } while (occurance == -1 || occurance < 0);

        return occurance;
    }

}
