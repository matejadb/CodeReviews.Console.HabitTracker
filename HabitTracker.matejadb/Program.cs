using Microsoft.Data.Sqlite;
using System.Globalization;

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

                command.CommandText = @"CREATE TABLE IF NOT EXISTS drinking_water (
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
        Console.WriteLine("1. Insert a new habit");
        Console.WriteLine("2. Update a habit");
        Console.WriteLine("3. View all");
        Console.WriteLine("4. Delete a habit");
        Console.WriteLine("5. Exit\n");
        Console.WriteLine("=============================");
        Console.Write("Please select an option: ");

        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                InsertHabit();
                break;
            case "2":
                UpdateHabit();
                break;
            case "3":
                ViewHabits();

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Menu();
                break;
            case "4":
                DeleteHabit();
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

    private static void DeleteHabit()
    {
        throw new NotImplementedException();
    }

    private static void ViewHabits()
    {
        Console.Clear();
        Console.WriteLine("======WATER DRINKING HISTORY======");
        Console.WriteLine("\nId\tDate\t\tOccurance\n");
        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"SELECT Id, Date, Occurance FROM drinking_water";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetString(0)}.\t{reader.GetString(1)}\t{reader.GetString(2)}\n");
                }
            }

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("\n==================================");
    }

    // TODO: Validate Id
    private static void UpdateHabit()
    {
        ViewHabits();

        Console.Write("Select an id of the occurance you wish to edit: ");
        string id = Console.ReadLine();

        Console.Write("Input the updated date for the habit (yyyy-MM-dd): ");
        string updatedDate = GetDateFromUser();

        Console.Write("Input the updated number of occurances: ");
        int updatedOccurance = GetOccuranceFromUser();

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();


                var command = connection.CreateCommand();

                command.CommandText = $"UPDATE drinking_water " +
                    $"SET Date = '{updatedDate}', Occurance = '{updatedOccurance}' " +
                    $"WHERE Id = {id}";

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("\nHabit updated successfully!\nPress any key to continue...");
        Console.ReadKey();

        Menu();

    }

    private static void InsertHabit()
    {
        Console.Clear();
        Console.WriteLine("==========================NEW HABIT==========================");

        Console.Write("\nInsert the date of the habit (yyyy-MM-dd): ");
        string dateInput = GetDateFromUser();

        Console.Write("\nInsert the number of occurrences: ");
        int occuranceInput = GetOccuranceFromUser();

        Console.WriteLine("=============================================================");

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            using (var transaction = connection.BeginTransaction())
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO drinking_water(Date, Occurance) VALUES('{dateInput}', '{occuranceInput}')";
                try
                {
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqliteException ex)
                {
                    Console.WriteLine(ex.Message);
                    transaction.Rollback();
                }

                connection.Close();
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
        ;

        Console.WriteLine("\nHabit inserted successfully!\nPress any key to continue...");
        Console.ReadKey();

        Menu();
    }

    static bool ValidateDate(string date)
    {
        DateTime result;
        string format = "yyyy-MM-dd";

        return DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
    }
    private static string GetDateFromUser()
    {
        bool isValid = false;
        string date;

        do
        {
            date = Console.ReadLine();
            isValid = ValidateDate(date);

            if (!isValid)
            {
                Console.Write("\nInvalid input. Please enter a valid date: ");
            }
        } while (!isValid);

        return date;
    }

    private static int GetOccuranceFromUser()
    {
        int occurance = -1;

        do
        {
            occurance = Int32.TryParse(Console.ReadLine(), out occurance) ? occurance : -1;

            if (occurance == -1 || occurance < 0)
            {
                Console.Write("\nInvalid input. Please enter a valid number of occurrences: ");
            }
        } while (occurance == -1 || occurance < 0);

        return occurance;
    }

}
