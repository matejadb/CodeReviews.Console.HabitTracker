# Habit Tracker Console Application
Simple CRUD console application that tracks water intake habits.
Developed in C#, database used is SQLite.

## Acceptance Criteria
- This is an application where you’ll log occurrences of a habit.
- This habit can't be tracked by time (ex. hours of sleep), only by quantity (ex. number of water glasses a day)
- Users need to be able to input the date of the occurrence of the habit
- The application should store and retrieve data from a real database
- When the application starts, it should create a sqlite database, if one isn’t present.
- It should also create a table in the database, where the habit will be logged.
- The users should be able to insert, delete, update and view their logged habit.
- You should handle all possible errors so that the application never crashes.
- You can only interact with the database using ADO.NET. You can’t use mappers such as Entity Framework or Dapper.
- Follow the DRY Principle, and avoid code repetition.
- Your project needs to contain a Read Me file where you'll explain how your app works and tell a little bit about your thought progress. What was hard? What was easy? What have you learned? Here's a nice example:

## Features
### SQLite Database
- Program uses SQLite to store and read information.
- If no database or table exists, they will be created when the program starts.

### Console UI

#### Main Menu
<img width="240" height="162" alt="Image" src="https://github.com/user-attachments/assets/a55e42fa-2414-4340-8d29-bbacef93df60" />

#### Insert 
<img width="509" height="163" alt="Image" src="https://github.com/user-attachments/assets/fe644c0a-78ac-4ef7-85c4-17cb417ba736" />

### View all
<img width="260" height="263" alt="Image" src="https://github.com/user-attachments/assets/952b5177-9dcc-4c6a-ab2b-b9a5050fecd4" />

### Delete
<img width="403" height="308" alt="Image" src="https://github.com/user-attachments/assets/c8a0f449-2974-4cb5-99a8-ac8a2f124223" />

## Challenges

## Lessons Learned

## Areas to Improve
