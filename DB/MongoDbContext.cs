using MongoDB.Bson;
using MongoDB.Driver;

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

        public MongoClient GetMongoClient(string connectionString)
        {
            var client = new MongoClient(connectionString);

            return client;

        }

        public IMongoCollection<dynamic> GetCollection(string collectionName)
        {
            string connectionString = GetConnectionString();
            var db = GetMongoClient(connectionString);
            return db.GetDatabase(this.dbName).GetCollection<dynamic>(collectionName);

        }

        public void GetAllCommunities()
        {
            //var collection = GetCollection<Store>("stores");

        }


    }
}