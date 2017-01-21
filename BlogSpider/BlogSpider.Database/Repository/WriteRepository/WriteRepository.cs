using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogSpider.Database.Repository.WriteRepository
{
    public class WriteRepository
    {
        private MongoClient Client { get; }
        private IMongoDatabase DatabaseName { get; }
        private IMongoCollection<BsonDocument> DocumentCollection { get; }

        public WriteRepository(MongoClient client, string databaseName, string collectionName)
        {
            Client = client;
            DatabaseName = Client.GetDatabase(databaseName);
            DocumentCollection = DatabaseName.GetCollection<BsonDocument>(collectionName);
        }

        public Task WriteRangeToCollectionAsync(IEnumerable<BsonDocument> documents)
        {
            return DocumentCollection.InsertManyAsync(documents);
        }

        public Task WriteToCollectionAsync(BsonDocument document)
        {
           return DocumentCollection.InsertOneAsync(document);
        }
    }
}
