using Microsoft.Data.Sqlite;
using System.Globalization;

namespace HabitTracker.matejadb;

public class Program
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
                Menu();
                break;
            case "2":
                UpdateHabit();
                Menu();
                break;
            case "3":
                ViewHabits();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Menu();
                break;
            case "4":
                DeleteHabit();
                Menu();
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

    private static void InsertHabit()
    {
        Console.Clear();
        Console.WriteLine("==========================NEW OCCURANCE==========================");

        Console.Write("\nInsert the date of the habit (yyyy-MM-dd): ");
        string dateInput = GetDateFromUser();

        Console.Write("\nInsert the number of occurrences: ");
        int occuranceInput = GetOccuranceFromUser();

        Console.WriteLine("==================================================================");

        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"INSERT INTO drinking_water(Date, Occurance) VALUES(@date, @occurance)";
                command.Parameters.AddWithValue("@date", dateInput);
                command.Parameters.AddWithValue("@occurance", occuranceInput);

                command.ExecuteNonQuery();

                Console.WriteLine("\nOccurance inserted successfully!\nPress any key to continue...");
                Console.ReadKey();

                connection.Close();
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        };


    }

    private static void UpdateHabit()
    {
        ViewHabits();

        Console.Write("Select an id of the occurance you wish to edit: ");
        int id = GetIdFromUser();

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

                command.CommandText = @"UPDATE drinking_water " +
                    $"SET Date = @date, Occurance = @occurance " +
                    $"WHERE Id = @id";

                command.Parameters.AddWithValue("@date", updatedDate);
                command.Parameters.AddWithValue("@occurance", updatedOccurance);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    Console.WriteLine($"No occurance found with Id {id}. Nothing was updated.\nPress any key to continue...");
                }
                else
                {
                    Console.WriteLine("\nOccurance updated successfully!\nPress any key to continue...");
                }
                Console.ReadKey();

                connection.Close();
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
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

                connection.Close();
            }

        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("\n==================================");
    }

    private static void DeleteHabit()
    {
        ViewHabits();
        Console.Write("Select an id of the occurance you wish to delete: ");
        int id = GetIdFromUser();

        try
        {

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();

                command.CommandText = @"DELETE FROM drinking_water WHERE Id=@id";

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if(rowsAffected == 0)
                {
                    Console.WriteLine($"\n\nNo occurance found with Id {id}. Nothing was deleted.\nPress any key to continue...");
                } else
                {
                    Console.WriteLine("\nOccurance deleted successfully!\nPress any key to continue...");
                }
                Console.ReadKey();

                connection.Close();
            }
        }
        catch (SqliteException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    // Util functions

    public static bool ValidateDate(string date)
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

    private static int GetIdFromUser()
    {
        int id = -1;

        do
        {
            id = Int32.TryParse(Console.ReadLine(), out id) ? id : -1;

            if (id == -1 || id < 0)
            {
                Console.Write("\nInvalid Id. Please enter a valid number: ");
            }
        } while (id == -1 || id < 0);

        return id;
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
