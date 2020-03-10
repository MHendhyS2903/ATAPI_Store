using MongoDB.Driver;
using System;

namespace ATAPI.Data
{
    public class BaseDA
    {
        public MongoClient client;
        public IMongoDatabase DB;

        public BaseDA()
        {
            client = new MongoClient("mongodb://localhost:27017");
            DB = client.GetDatabase("asiatrans_dev");
        }
    }
}
