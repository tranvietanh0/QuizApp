-- QuizApp Database Initialization Script (MySQL)
-- Run: mysql -u root -p < InitializeDatabase.sql

CREATE DATABASE IF NOT EXISTS QuizAppDb CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE QuizAppDb;

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

-- Indexes
CREATE INDEX IX_Questions_QuizId ON Questions(QuizId);
CREATE INDEX IX_Answers_QuestionId ON Answers(QuestionId);
CREATE INDEX IX_QuizAttempts_UserId ON QuizAttempts(UserId);
CREATE INDEX IX_QuizAttempts_QuizId ON QuizAttempts(QuizId);
CREATE INDEX IX_QuizAttempts_Score ON QuizAttempts(Score);
