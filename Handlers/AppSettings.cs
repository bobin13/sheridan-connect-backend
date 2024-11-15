namespace sheridan_connect_backend.Handlers
{


// {
//     public class AppSettings
//     {
//         public MongoDBSettings MongoDB { get; set; }
//         public JwtSettings JwtSettings { get; set; }
//     }

//     public class MongoDBSettings
//     {
//         public string ConnectionString { get; set; }
//         public string Database { get; set; }
//     }

//     public class JwtSettings
//     {
//         public string SecretKey { get; set; }
//     }


        public class AppSettings
        {
            public string JwtSecret { get; set; }
 
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
            public string UserCollectionName { get; set; }
        }
}