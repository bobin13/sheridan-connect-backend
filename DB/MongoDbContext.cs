using MongoDB.Driver;

namespace sheridan_connect_backend.DB
{

    public static class MongoDbContext
    {
        public static string GetConnectionString()
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, "variables.env");

            DotEnv.Load(dotenv);

            var username = Environment.GetEnvironmentVariable("USERNAME");
            var password = Environment.GetEnvironmentVariable("PASSWORD");

            var uri = $"mongodb+srv://{username}:{password}@cluster0.wi0uk9y.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";


            return uri;
        }

    }
}