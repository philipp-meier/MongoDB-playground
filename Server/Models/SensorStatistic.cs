using MongoDB.Driver;
using Server.Extensions;

namespace Server.Models;

public class SensorStatistic
{
    public string SensorId { get; set; }
    public long Count { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public MeasureStatistic<float?> Temp { get; set; }
    public MeasureStatistic<int?> A { get; set; }
    public MeasureStatistic<int?> B { get; set; }
    public MeasureStatistic<int?> C { get; set; }
    public MeasureStatistic<int?> D { get; set; }
    public MeasureStatistic<int?> E { get; set; }
    public MeasureStatistic<int?> F { get; set; }
    public MeasureStatistic<int?> G { get; set; }
    public MeasureStatistic<int?> H { get; set; }
    public MeasureStatistic<int?> I { get; set; }
    public MeasureStatistic<int?> J { get; set; }
    public MeasureStatistic<int?> K { get; set; }
    public MeasureStatistic<int?> L { get; set; }
}

public class MeasureStatistic<T>
{
    public T Min { get; set; }
    public T Max { get; set; }
    public T Last { get; set; }

    public static async Task<MeasureStatistic<T>> Create(IMongoCollection<SensorData> collection, string measureName,
        FilterDefinition<SensorData> filter, CancellationToken ct)
    {
        return new MeasureStatistic<T>
        {
            Min = (T)(await collection.MinBy(filter, measureName, ct))?.GetPropertyValue(measureName),
            Max = (T)(await collection.MaxBy(filter, measureName, ct))?.GetPropertyValue(measureName),
            Last = (T)(await collection.LastWithValue(filter, measureName, ct))?.GetPropertyValue(measureName)
        };
    }
}