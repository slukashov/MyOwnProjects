using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogSpider.Database.Repository.ReadRepository
{
    public class ReadRepository
    {
        private MongoClient Client { get; }
        private IMongoDatabase DatabaseName { get; }
        private IMongoCollection<BsonDocument> DocumentCollection { get; }

        public ReadRepository(MongoClient client, string databaseName, string collectionName)
        {
            Client = client;
            DatabaseName = Client.GetDatabase(databaseName);
            DocumentCollection = DatabaseName.GetCollection<BsonDocument>(collectionName);
        }

        public List<BsonDocument> GetAllFromCollection()
        {
            return DocumentCollection.Find(new BsonDocument()).ToList();
        }

        public BsonDocument GetFirstOrDefaultFromCollection(FilterDefinition<BsonDocument> filter )
        {
            return DocumentCollection.Find(filter).FirstOrDefault();
        }
    }
}
