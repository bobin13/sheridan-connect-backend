namespace sheridan_connect_backend.DB{
    
    //For user authentication
    public class MongoDBSetting{
        public string ConnectionString {get;set;}
        public string DatabaseName {get;set;}
        public string UserCollectionName {get;set;}
    }

}