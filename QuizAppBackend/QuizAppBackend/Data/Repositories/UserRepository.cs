using Dapper;
using QuizAppBackend.Data.Repositories.Interfaces;
using QuizAppBackend.Models;

namespace QuizAppBackend.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbConnectionFactory _dbFactory;

    public UserRepository(DbConnectionFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Username = @Username",
            new { Username = username });
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        using var connection = _dbFactory.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<User>(
            "SELECT * FROM Users WHERE Email = @Email",
            new { Email = email });
    }

    public async Task<User> CreateAsync(User user)
    {
        using var connection = _dbFactory.CreateConnection();
        var id = await connection.QuerySingleAsync<int>(
            @"INSERT INTO Users (Username, Email, PasswordHash, DisplayName)
              VALUES (@Username, @Email, @PasswordHash, @DisplayName);
              SELECT LAST_INSERT_ID();",
            new { user.Username, user.Email, user.PasswordHash, user.DisplayName });

        user.Id = id;
        return user;
    }
}
