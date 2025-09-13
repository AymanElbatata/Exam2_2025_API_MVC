# Exam2_2025_API_MVC
Online Exams for students and teachers

🧪 Online Exam System (Exam Website), it has no control panel yet.
A complete mini online examination system built using ASP.NET Core web api & mvc and Entity Framework Core. This project includes:


Setup and Configuration:
 Set up a .NET Core MVC project.
 Entity Framework to connect to a local SQL Server instance.
Database Schema:
 Design the schema to include Users, Questions, UserResponses, and TestResults.
 the Questions table with sample questions for test.
User Management:
 Implement registration and login pages.
 Handle user authentication and maintain session information.
API Development:
Develop a RESTful API to retrieve test results:
 GET /api/results  return the results for the authenticated user's most recent test.
  the API is secured and only accessible to authenticated users.
Test Functionality and UI:
  the user interface for taking tests, submitting answers directly through the MVC
application.
  functionality for users to retake tests, submitting new answers.
Results History:
  a page to list all the tests a user has taken, including scores.
  the API fetches and display test results from the database.
Deliverables:

implementation. Discuss any significant design decisions and challenges encountered during
development.
Evaluation Criteria:
 API Integration and Design: Efficiency and security in retrieving test results via the API.
 Security and Session Management: Effective user authentication and session management.
 Code Quality: Organization, readability, and application of best coding practices.
 Functionality: Implementation of all required features, especially the API for retrieving results.
 Database Integration: Effective use of Entity Framework with SQL Server.


Developed by Ayman Elbatata

🔧 Technologies Used
ASP.NET Core 9 (API/C#/MVC) 4 layers

Entity Framework Core (with Migrations)

ASP.NET Identity (for secure authentication)

JavaScript & AJAX

Bootstrap (for responsive UI)

SQL Server

🛠 Features
🔐 Admin Panel
Login JWT API & claims for mvc.


🧑‍🎓 User Website
Login with pre-added credentials.

View and select available exams.

Take exams with multiple-choice questions.

Submit exams via AJAX (no page reload).

See real-time score and feedback:

Total Score in percentage

Number of Correct/Incorrect answers

Pass/Fail status (based on 60% threshold)

✅ Evaluation Logic
Each question = 1 point.

Score formula:

Score
=
(
Correct Answers
Total Questions
)
×
100
Score=( 
Total Questions
Correct Answers
​
 )×100
Pass if score ≥ 60%, otherwise Fail.

📁 Project Structure
pgsql
Copy

/User         --> Frontend for exam-taking
/Models       --> Database models
/Data         --> DbContext, Migrations
/Services     --> Business logic & Repository pattern
📌 Installation
Clone the repository:

bash
Copy
Edit
git clone https://github.com/AymanElbatata/Exam2_2025_API_MVC.git
Update your connection string in appsettings.json.

Apply database migrations:

bash
Copy
Edit
dotnet ef database update
Run the project:

bash
Copy
Edit
dotnet run
📊 Screenshots
(Add screenshots here for Admin Panel, Exam UI, and Score Page)

📇 Connect with the Developer LinkedIn: Ayman Elbatata https://www.linkedin.com/in/ayman-elbatata/
------
I can make any update according to your needs and you can buy it via https://aymanelbatata.gumroad.com/l/ExamSystem2025
Or via the following link just if you need a commercial invoice with 14% taxes and other fees: https://checkouts.kashier.io/en/prepaymenpages?ppLink=PP-927176201,live 
