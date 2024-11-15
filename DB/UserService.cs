using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using sheridan_connect_backend.Models;
using sheridan_connect_backend.DB;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using sheridan_connect_backend.Handlers;

namespace sheridan_connect_backend.DB
{
    public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IOptions<MongoDBSetting> mongoDBSetting)
    {
        var client = new MongoClient(mongoDBSetting.Value.ConnectionString);
        var database = client.GetDatabase(mongoDBSetting.Value.DatabaseName);
        _users = database.GetCollection<User>(mongoDBSetting.Value.UserCollectionName);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _users.Find(user => user.Email == email).FirstOrDefaultAsync();
    }
}
}
