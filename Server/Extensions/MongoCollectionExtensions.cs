using Server.Models;
using MongoDB.Driver;

namespace Server.Extensions;

public static class MongoCollectionExtensions
{
    public static async Task<MeasureStatistic<T>> CreateMeasureStatisticFor<T>(this IMongoCollection<SensorData> collection,
        string measureName, FilterDefinition<SensorData> filter, CancellationToken ct)
        => await MeasureStatistic<T>.Create(collection, measureName, filter, ct);

    public static async Task<SensorData> MinBy(this IMongoCollection<SensorData> collection,
        FilterDefinition<SensorData> filter, string propertyName, CancellationToken ct)
        => await collection.FirstOrDefault(filter, propertyName, true, ct);

    public static async Task<SensorData> MaxBy(this IMongoCollection<SensorData> collection,
        FilterDefinition<SensorData> filter, string propertyName, CancellationToken ct)
        => await collection.FirstOrDefault(filter, propertyName, false, ct);

    public static async Task<SensorData> LastWithValue(this IMongoCollection<SensorData> collection,
        FilterDefinition<SensorData> filter, string propertyName, CancellationToken ct)
        => await collection.MaxBy(filter & Builders<SensorData>.Filter.Exists(propertyName), "Timestamp", ct);

    private static async Task<SensorData> FirstOrDefault(this IMongoCollection<SensorData> collection,
        FilterDefinition<SensorData> filter, string sortField, bool ascending, CancellationToken ct)
    {
        var sort = Builders<SensorData>.Sort;
        var sortDefinition = ascending ?
            sort.Ascending(sortField) :
            sort.Descending(sortField);

        var sortFieldNotNullFilter = Builders<SensorData>.Filter.Exists(sortField);

        return await collection.Find(filter & sortFieldNotNullFilter)
            .Sort(sortDefinition)
            .FirstOrDefaultAsync(ct);
    }
}