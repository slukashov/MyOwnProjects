using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogSpider.Database.Enities
{
    public class Enitity
    {
        public Enitity(DateTime date, string link)
        {
            Date = date;
            Link = link;
        }

        [BsonElement("date")]
        public DateTime Date { get; }

        [BsonElement("uri")]
        public string Link { get; }

        public BsonDocument ToBsonDocument()
        {
            return new BsonDocument
            {
                ["date"] = Date,
                ["uri"] = Link
            };
        }
    }
}