using MongoDB.Bson;
using MongoDB.Driver;
using sheridan_connect_backend.Models;

namespace sheridan_connect_backend.DB
{

    public class MongoDbContext
    {
        string dbName = "sheridan-connect";
        public string GetConnectionString()
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, "variables.env");
            DotEnv.Load(dotenv);

            var username = Environment.GetEnvironmentVariable("USERNAME");
            var password = Environment.GetEnvironmentVariable("PASSWORD");

            var uri = $"mongodb+srv://{username}:{password}@cluster0.wi0uk9y.mongodb.net/?retryWrites=true&w=majority";

            return uri;
        }

        public MongoClient GetMongoClient()
        {
            var client = new MongoClient(GetConnectionString());

            return client;

        }

        public IMongoCollection<User> GetUserCollection()
        {

            var client = GetMongoClient();
            return client.GetDatabase(this.dbName).GetCollection<User>("users");

        }

        public dynamic GetAllUserCommunities(ObjectId id)
        {

            var client = GetMongoClient();
            var filter = Builders<Community>.Filter.Eq("_id", id);

            return client.GetDatabase(this.dbName).GetCollection<Community>("communities").Find(filter);



        }

        public IMongoCollection<Community> GetAllCommunities()
        {
            var client = GetMongoClient();

            return client.GetDatabase(this.dbName).GetCollection<Community>("communities");
        }

        public IMongoCollection<Post> GetAllPost()
        {
            var client = GetMongoClient();

            return client.GetDatabase(this.dbName).GetCollection<Post>("posts");
        }


    }
}