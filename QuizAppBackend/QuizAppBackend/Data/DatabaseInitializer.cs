using MySqlConnector;

namespace QuizAppBackend.Data;

public static class DatabaseInitializer
{
    public static void Initialize(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        var builder = new MySqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;

        // Connect without database to create it if not exists
        builder.Database = "";
        using (var masterConn = new MySqlConnection(builder.ConnectionString))
        {
            masterConn.Open();
            using var checkCmd = new MySqlCommand(
                $"CREATE DATABASE IF NOT EXISTS `{databaseName}` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;",
                masterConn);
            checkCmd.ExecuteNonQuery();
        }

        // Now connect to the actual database and create tables
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        var createTablesSql = @"
            CREATE TABLE IF NOT EXISTS Users (
                Id           INT AUTO_INCREMENT PRIMARY KEY,
                Username     VARCHAR(50)  NOT NULL UNIQUE,
                Email        VARCHAR(100) NOT NULL UNIQUE,
                PasswordHash VARCHAR(200) NOT NULL,
                DisplayName  VARCHAR(100) NOT NULL,
                CreatedAt    DATETIME     NOT NULL DEFAULT (UTC_TIMESTAMP())
            );

            CREATE TABLE IF NOT EXISTS Quizzes (
                Id               INT AUTO_INCREMENT PRIMARY KEY,
                Title            VARCHAR(200)  NOT NULL,
                Description      VARCHAR(1000) NULL,
                Category         VARCHAR(100)  NULL,
                TimeLimitSeconds INT           NOT NULL DEFAULT 0,
                IsActive         TINYINT(1)    NOT NULL DEFAULT 1,
                CreatedAt        DATETIME      NOT NULL DEFAULT (UTC_TIMESTAMP())
            );

            CREATE TABLE IF NOT EXISTS Questions (
                Id            INT AUTO_INCREMENT PRIMARY KEY,
                QuizId        INT          NOT NULL,
                QuestionText  VARCHAR(500) NOT NULL,
                QuestionOrder INT          NOT NULL DEFAULT 0,
                Points        INT          NOT NULL DEFAULT 10,
                FOREIGN KEY (QuizId) REFERENCES Quizzes(Id) ON DELETE CASCADE
            );

            CREATE TABLE IF NOT EXISTS Answers (
                Id          INT AUTO_INCREMENT PRIMARY KEY,
                QuestionId  INT          NOT NULL,
                AnswerText  VARCHAR(500) NOT NULL,
                IsCorrect   TINYINT(1)   NOT NULL DEFAULT 0,
                AnswerOrder INT          NOT NULL DEFAULT 0,
                FOREIGN KEY (QuestionId) REFERENCES Questions(Id) ON DELETE CASCADE
            );

            CREATE TABLE IF NOT EXISTS QuizAttempts (
                Id               INT AUTO_INCREMENT PRIMARY KEY,
                UserId           INT      NOT NULL,
                QuizId           INT      NOT NULL,
                Score            INT      NOT NULL DEFAULT 0,
                TotalPoints      INT      NOT NULL DEFAULT 0,
                TimeTakenSeconds INT      NULL,
                CompletedAt      DATETIME NOT NULL DEFAULT (UTC_TIMESTAMP()),
                FOREIGN KEY (UserId) REFERENCES Users(Id),
                FOREIGN KEY (QuizId) REFERENCES Quizzes(Id)
            );
        ";

        // MySQL requires executing statements one at a time
        foreach (var statement in createTablesSql.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (string.IsNullOrWhiteSpace(statement)) continue;
            using var cmd = new MySqlCommand(statement, connection);
            cmd.ExecuteNonQuery();
        }

        // Create indexes (ignore if exists)
        CreateIndexIfNotExists(connection, "IX_Questions_QuizId", "Questions", "QuizId");
        CreateIndexIfNotExists(connection, "IX_Answers_QuestionId", "Answers", "QuestionId");
        CreateIndexIfNotExists(connection, "IX_QuizAttempts_UserId", "QuizAttempts", "UserId");
        CreateIndexIfNotExists(connection, "IX_QuizAttempts_QuizId", "QuizAttempts", "QuizId");
        CreateIndexIfNotExists(connection, "IX_QuizAttempts_Score", "QuizAttempts", "Score");

        // Seed data
        using var checkSeed = new MySqlCommand("SELECT COUNT(*) FROM Quizzes", connection);
        var quizCount = Convert.ToInt32(checkSeed.ExecuteScalar());
        if (quizCount == 0)
            SeedData(connection);
    }

    private static void CreateIndexIfNotExists(MySqlConnection connection, string indexName, string tableName, string columnName)
    {
        using var cmd = new MySqlCommand(
            $@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.STATISTICS
               WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = '{tableName}' AND INDEX_NAME = '{indexName}'",
            connection);
        var exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        if (!exists)
        {
            using var createCmd = new MySqlCommand($"CREATE INDEX {indexName} ON {tableName}({columnName})", connection);
            createCmd.ExecuteNonQuery();
        }
    }

    private static void SeedData(MySqlConnection connection)
    {
        // Quiz 1: General Knowledge
        var quiz1Id = InsertAndGetId(connection,
            "INSERT INTO Quizzes (Title, Description, Category, TimeLimitSeconds) VALUES ('General Knowledge', 'Test your general knowledge with these fun questions!', 'General', 300)");

        var q1 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz1Id}, 'What is the capital of France?', 1, 10)");
        InsertAnswers(connection, q1, ("London", false), ("Paris", true), ("Berlin", false), ("Madrid", false));

        var q2 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz1Id}, 'Which planet is known as the Red Planet?', 2, 10)");
        InsertAnswers(connection, q2, ("Venus", false), ("Mars", true), ("Jupiter", false), ("Saturn", false));

        var q3 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz1Id}, 'What is the largest ocean on Earth?', 3, 10)");
        InsertAnswers(connection, q3, ("Atlantic Ocean", false), ("Indian Ocean", false), ("Pacific Ocean", true), ("Arctic Ocean", false));

        var q4 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz1Id}, 'Who painted the Mona Lisa?', 4, 10)");
        InsertAnswers(connection, q4, ("Vincent van Gogh", false), ("Pablo Picasso", false), ("Leonardo da Vinci", true), ("Michelangelo", false));

        var q5 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz1Id}, 'What is the chemical symbol for water?', 5, 10)");
        InsertAnswers(connection, q5, ("CO2", false), ("H2O", true), ("O2", false), ("NaCl", false));

        // Quiz 2: Science & Technology
        var quiz2Id = InsertAndGetId(connection,
            "INSERT INTO Quizzes (Title, Description, Category, TimeLimitSeconds) VALUES ('Science & Technology', 'How well do you know science and tech?', 'Science', 600)");

        var q6 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz2Id}, 'What does CPU stand for?', 1, 10)");
        InsertAnswers(connection, q6, ("Central Processing Unit", true), ("Computer Personal Unit", false), ("Central Program Utility", false), ("Core Processing Unit", false));

        var q7 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz2Id}, 'What is the speed of light approximately?', 2, 15)");
        InsertAnswers(connection, q7, ("300,000 km/s", true), ("150,000 km/s", false), ("500,000 km/s", false), ("1,000,000 km/s", false));

        var q8 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz2Id}, 'Who is known as the father of computers?', 3, 10)");
        InsertAnswers(connection, q8, ("Alan Turing", false), ("Charles Babbage", true), ("Bill Gates", false), ("Steve Jobs", false));

        var q9 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz2Id}, 'What does DNA stand for?', 4, 10)");
        InsertAnswers(connection, q9, ("Deoxyribonucleic Acid", true), ("Dinitrogen Acid", false), ("Dynamic Nuclear Acid", false), ("Dual Nucleic Acid", false));

        var q10 = InsertAndGetId(connection,
            $"INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points) VALUES ({quiz2Id}, 'Which programming language was created by James Gosling?', 5, 15)");
        InsertAnswers(connection, q10, ("Python", false), ("C++", false), ("Java", true), ("Ruby", false));
    }

    private static long InsertAndGetId(MySqlConnection connection, string sql)
    {
        using var cmd = new MySqlCommand(sql, connection);
        cmd.ExecuteNonQuery();
        return cmd.LastInsertedId;
    }

    private static void InsertAnswers(MySqlConnection connection, long questionId, params (string text, bool isCorrect)[] answers)
    {
        for (int i = 0; i < answers.Length; i++)
        {
            using var cmd = new MySqlCommand(
                "INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@qid, @text, @correct, @order)",
                connection);
            cmd.Parameters.AddWithValue("@qid", questionId);
            cmd.Parameters.AddWithValue("@text", answers[i].text);
            cmd.Parameters.AddWithValue("@correct", answers[i].isCorrect);
            cmd.Parameters.AddWithValue("@order", i + 1);
            cmd.ExecuteNonQuery();
        }
    }
}
