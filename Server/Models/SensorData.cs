using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models;

public class SensorData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("metadata")]
    public SensorMetadata Metadata { get; set; } = null!;

    [BsonElement("timestamp")]
    public DateTime Timestamp { get; set; }

    public float Temperature { get; set; }

    [BsonIgnoreIfNull] public int? A { get; set; }
    [BsonIgnoreIfNull] public int? B { get; set; }
    [BsonIgnoreIfNull] public int? C { get; set; }
    [BsonIgnoreIfNull] public int? D { get; set; }
    [BsonIgnoreIfNull] public int? E { get; set; }
    [BsonIgnoreIfNull] public int? F { get; set; }
    [BsonIgnoreIfNull] public int? G { get; set; }
    [BsonIgnoreIfNull] public int? H { get; set; }
    [BsonIgnoreIfNull] public int? I { get; set; }
    [BsonIgnoreIfNull] public int? J { get; set; }
    [BsonIgnoreIfNull] public int? K { get; set; }
    [BsonIgnoreIfNull] public int? L { get; set; }

    public object GetPropertyValue(string name)
        => typeof(SensorData).GetProperty(name).GetValue(this);
}

public class SensorMetadata
{
    public string SensorId { get; set; }
    public string Version { get; set; }
}