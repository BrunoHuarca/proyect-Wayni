using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("FirstName")]
    public string? FirstName { get; set; }

    [BsonElement("LastName")]
    public string? LastName { get; set; }

    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
