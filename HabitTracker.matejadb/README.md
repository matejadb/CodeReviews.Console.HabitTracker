# Habit Tracker Console Application

Simple CRUD console application that tracks water intake habits.
Developed in C#, database used is SQLite.

## About

This is a console application for logging occurrences of a habit that is tracked
by **quantity** rather than time (e.g. "4 glasses of water" instead of "8 hours of sleep").
Each entry records the date the habit occurred and how many times it happened that day.
Data is persisted in a local SQLite database and accessed directly through ADO.NET
(`Microsoft.Data.Sqlite`), no ORMs or mappers.

## How To Run
- Download `Microsoft.Data.Sqlite` from the NuGet Package Manager
- Run the application (CTRL + F5)
- On first run, the app automatically creates the `habit_tracker.db` SQLite database
  file and the `drinking_water` table if they don't already exist, no manual setup needed

## Features

### SQLite Database
- Program uses SQLite to store and read information.
- If no database or table exists, they will be created when the program starts.
- All database access goes through ADO.NET (`Microsoft.Data.Sqlite`) directly, no
  Entity Framework, Dapper, or other mappers.

### Console UI

#### Main Menu
<img width="240" height="162" alt="Image" src="https://github.com/user-attachments/assets/a55e42fa-2414-4340-8d29-bbacef93df60" />

#### Insert
<img width="509" height="163" alt="Image" src="https://github.com/user-attachments/assets/fe644c0a-78ac-4ef7-85c4-17cb417ba736" />

#### View all
<img width="260" height="263" alt="Image" src="https://github.com/user-attachments/assets/952b5177-9dcc-4c6a-ab2b-b9a5050fecd4" />

#### Delete
<img width="403" height="308" alt="Image" src="https://github.com/user-attachments/assets/c8a0f449-2974-4cb5-99a8-ac8a2f124223" />

## Project Structure

```
HabitTracker.matejadb.sln
├── HabitTracker.matejadb/       # Main console application
│   ├── Program.cs               # Entry point, menu loop, CRUD operations, validation helpers
│   └── habit_tracker.db         # SQLite database file (auto-created on first run)
└── UnitTests/                   # NUnit test project
    └── ValidationTests.cs       # Tests for date validation logic
```

## Database Schema

**Table: `drinking_water`**

| Column    | Type    | Notes                             |
|-----------|---------|------------------------------------|
| Id        | INTEGER | Primary key, autoincrement        |
| Date      | TEXT    | Date of the occurrence (`yyyy-MM-dd`) |
| Occurance | INTEGER | Number of occurrences logged that day |

Created on startup with:
```sql
CREATE TABLE IF NOT EXISTS drinking_water (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Date TEXT NOT NULL,
    Occurance INTEGER NOT NULL
);
```

## Menu Options
1. **Insert a new habit** – prompts for a date and a number of occurrences, then inserts a new row.
2. **Update a habit** – shows all entries, asks for an Id, then overwrites the date and occurrence count for that row.
3. **View all** – lists every logged entry (Id, Date, Occurance).
4. **Delete a habit** – shows all entries, asks for an Id, then deletes that row if it exists.
5. **Exit** – closes the application.

## Input Validation
- **Dates** are validated with `DateTime.TryParseExact` against the `yyyy-MM-dd` format
  before being accepted, via a reusable `ValidateDate` method and a `GetDateFromUser`
  loop that keeps re-prompting until the input is valid.
- **Ids** and **occurrence counts** are validated with `Int32.TryParse` and a
  non-negative check, via `GetIdFromUser` and `GetOccuranceFromUser`, following the
  same "loop until valid" pattern as the date input.
- Centralizing this logic into small reusable helper methods (rather than repeating
  parsing/validation inline in each menu option) is the main way the project follows DRY.

## Error Handling
- Every database operation (`CREATE TABLE`, `INSERT`, `UPDATE`, `SELECT`, `DELETE`)
  is wrapped in a `try/catch (SqliteException ex)` block, so a database-level failure
  is caught and printed instead of crashing the app.
- All SQL commands use parameterized queries (`@date`, `@occurance`, `@id`) to avoid
  SQL injection and type-mismatch issues.
- `UpdateHabit` and `DeleteHabit` check `rowsAffected` after the command runs and let
  the user know if the Id they entered didn't match any row, instead of silently doing nothing.
- User input is validated *before* it ever reaches a SQL command, which keeps the
  data-access code simpler since it can trust the values it receives.

## Unit Tests
The `UnitTests` project uses NUnit to test the date validation logic in isolation:
- `CorrectDateInput_ReturnsTrue` - confirms a properly formatted date (`yyyy-MM-dd`) passes validation.
- `BadDateInput_ReturnsFalse` - confirms a malformed date is correctly rejected.

`ValidateDate` was kept as a small, static, side-effect-free method specifically so it
could be tested without needing a database connection or console input.

## Challenges
- It has been a while since I've used SQL so I needed a refresher.
- Since I've never used SQLite with C# (or SQLite at all), it took a bit of time to
  get comfortable writing it.
- Getting comfortable with `TryParseExact` and making sure the date format was strictly
  enforced (so something like `202-1-3` wouldn't sneak through) took some trial and error.
- Deciding how much validation belongs in the console-input loop versus the database
  layer, and keeping that logic reusable across Insert/Update/Delete without duplicating it.

## Lessons Learned
- Got hands-on practice with ADO.NET fundamentals (connections, commands, readers,
  parameterized queries) without the safety net of an ORM.
- Reinforced the importance of validating user input at the boundary, before it
  ever reaches a SQL command — this made the error handling in the data-access code
  much simpler overall.
- Writing `ValidateDate` as a standalone static method made it trivial to unit test,
  which reinforced how useful it is to isolate pure logic from I/O (console/db) when possible.

## Areas to Improve
- Add more descriptive validation feedback (e.g. telling the user exactly why a date
  was rejected, rather than a generic "invalid input" message).
- Extract the repeated `using (var connection = new SqliteConnection(...))` /
  `try/catch` boilerplate in each method into a shared helper to reduce repetition further.
- Expand unit test coverage to include the Id and occurrence validation helpers,
  not just date validation.

## Future Features
- Support tracking multiple habits, not just water intake.
- Seeding data