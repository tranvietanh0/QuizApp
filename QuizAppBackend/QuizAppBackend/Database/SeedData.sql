-- QuizApp Seed Data
-- Run after InitializeDatabase.sql

USE QuizAppDb;
GO

-- Only seed if no quizzes exist
IF NOT EXISTS (SELECT 1 FROM Quizzes)
BEGIN
    -- Quiz 1: General Knowledge
    INSERT INTO Quizzes (Title, Description, Category, TimeLimitSeconds)
    VALUES (N'General Knowledge', N'Test your general knowledge with these fun questions!', N'General', 300);

    DECLARE @Quiz1Id INT = SCOPE_IDENTITY();

    -- Q1
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz1Id, N'What is the capital of France?', 1, 10);
    DECLARE @Q1Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q1Id, N'London', 0, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q1Id, N'Paris', 1, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q1Id, N'Berlin', 0, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q1Id, N'Madrid', 0, 4);

    -- Q2
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz1Id, N'Which planet is known as the Red Planet?', 2, 10);
    DECLARE @Q2Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q2Id, N'Venus', 0, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q2Id, N'Mars', 1, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q2Id, N'Jupiter', 0, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q2Id, N'Saturn', 0, 4);

    -- Q3
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz1Id, N'What is the largest ocean on Earth?', 3, 10);
    DECLARE @Q3Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q3Id, N'Atlantic Ocean', 0, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q3Id, N'Indian Ocean', 0, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q3Id, N'Pacific Ocean', 1, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q3Id, N'Arctic Ocean', 0, 4);

    -- Q4
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz1Id, N'Who painted the Mona Lisa?', 4, 10);
    DECLARE @Q4Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q4Id, N'Vincent van Gogh', 0, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q4Id, N'Pablo Picasso', 0, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q4Id, N'Leonardo da Vinci', 1, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q4Id, N'Michelangelo', 0, 4);

    -- Q5
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz1Id, N'What is the chemical symbol for water?', 5, 10);
    DECLARE @Q5Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q5Id, N'CO2', 0, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q5Id, N'H2O', 1, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q5Id, N'O2', 0, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q5Id, N'NaCl', 0, 4);

    -- Quiz 2: Science & Technology
    INSERT INTO Quizzes (Title, Description, Category, TimeLimitSeconds)
    VALUES (N'Science & Technology', N'How well do you know science and tech?', N'Science', 600);

    DECLARE @Quiz2Id INT = SCOPE_IDENTITY();

    -- Q1
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz2Id, N'What does CPU stand for?', 1, 10);
    DECLARE @Q6Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q6Id, N'Central Processing Unit', 1, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q6Id, N'Computer Personal Unit', 0, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q6Id, N'Central Program Utility', 0, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q6Id, N'Core Processing Unit', 0, 4);

    -- Q2
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz2Id, N'What is the speed of light approximately?', 2, 15);
    DECLARE @Q7Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q7Id, N'300,000 km/s', 1, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q7Id, N'150,000 km/s', 0, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q7Id, N'500,000 km/s', 0, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q7Id, N'1,000,000 km/s', 0, 4);

    -- Q3
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz2Id, N'Who is known as the father of computers?', 3, 10);
    DECLARE @Q8Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q8Id, N'Alan Turing', 0, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q8Id, N'Charles Babbage', 1, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q8Id, N'Bill Gates', 0, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q8Id, N'Steve Jobs', 0, 4);

    -- Q4
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz2Id, N'What does DNA stand for?', 4, 10);
    DECLARE @Q9Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q9Id, N'Deoxyribonucleic Acid', 1, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q9Id, N'Dinitrogen Acid', 0, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q9Id, N'Dynamic Nuclear Acid', 0, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q9Id, N'Dual Nucleic Acid', 0, 4);

    -- Q5
    INSERT INTO Questions (QuizId, QuestionText, QuestionOrder, Points)
    VALUES (@Quiz2Id, N'Which programming language was created by James Gosling?', 5, 15);
    DECLARE @Q10Id INT = SCOPE_IDENTITY();

    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q10Id, N'Python', 0, 1);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q10Id, N'C++', 0, 2);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q10Id, N'Java', 1, 3);
    INSERT INTO Answers (QuestionId, AnswerText, IsCorrect, AnswerOrder) VALUES (@Q10Id, N'Ruby', 0, 4);

    PRINT 'Seed data inserted successfully!';
END
ELSE
BEGIN
    PRINT 'Seed data already exists, skipping.';
END
GO
