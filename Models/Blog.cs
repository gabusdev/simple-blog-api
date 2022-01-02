using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace simple_blog_api.Models;
public class Blog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = null!;

    [BsonElement("author")]
    public string Author { get; set; } = null!;

    [BsonElement("body")]
    public string Body { get; set; } = null!;
}