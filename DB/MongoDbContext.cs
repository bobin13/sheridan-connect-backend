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
            var dotenv = Path.Combine(root, ".env");
            DotEnv.Load(dotenv);

            var username = Environment.GetEnvironmentVariable("USERNAME");
            var password = Environment.GetEnvironmentVariable("PASSWORD");

            var uri = $"mongodb+srv://{username}:{password}@cluster-sheridan-connec.qjhvt.mongodb.net/";

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

        //-----------------Community related operations---------------------------------------------------------------------
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

        public async Task<bool> AddCommunity(Community community, string id)
        {
            try
            {
                var client = GetMongoClient();
                var db = client.GetDatabase(this.dbName);
                User user = await db.GetCollection<User>("users").Find(_ => _._id == id).FirstAsync();

                if (user == null)
                {
                    return false;
                }
                //community.Admins.Add(user._id);
                await db.GetCollection<Community>("communities").InsertOneAsync(community);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //------------------------Newsfeed related operations---------------------------------------------------------------------
        public IMongoCollection<Post> GetAllPost()
        {
            var client = GetMongoClient();

            return client.GetDatabase(this.dbName).GetCollection<Post>("posts");
        }

         public async Task<bool> AddPost(Post newPost)
        {
            try
            {
                var client = GetMongoClient();
                var db = client.GetDatabase(this.dbName);
                await db.GetCollection<Post>("posts").InsertOneAsync(newPost);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

    }
}