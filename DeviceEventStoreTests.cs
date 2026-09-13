using Xunit;

public class DeviceEventStoreTests
{
    [Fact]
    public void AddEvent_AddsNewEvent()
    {
        var store = new DeviceEventStore();

        var result = store.AddEvent(
            new DeviceEvent(
                "E1",
                "D1",
                "Speeding",
                DateTime.UtcNow));

        Assert.True(result);
    }

    [Fact]
    public void AddEvent_DuplicateIdIsRejected()
    {
        var store = new DeviceEventStore();

        store.AddEvent(
            new DeviceEvent(
                "E1",
                "D1",
                "Speeding",
                DateTime.UtcNow));

        var result = store.AddEvent(
            new DeviceEvent(
                "E1",
                "D2",
                "Phone",
                DateTime.UtcNow));

        Assert.False(result);
    }

    [Fact]
    public void GetEventsForDevice_ReturnsEventsOrderedByTimestamp()
    {
        var store = new DeviceEventStore();

        store.AddEvent(
            new DeviceEvent(
                "E1",
                "D1",
                "Phone",
                new DateTime(2026, 9, 1, 12, 5, 0)));

        store.AddEvent(
            new DeviceEvent(
                "E2",
                "D1",
                "Speeding",
                new DateTime(2026, 9, 1, 12, 0, 0)));

        var result = store.GetEventsForDevice("D1");

        Assert.Equal("E2", result[0].Id);
        Assert.Equal("E1", result[1].Id);
    }

    [Fact]
    public void CountEventsByType_ReturnsCorrectCount()
    {
        var store = new DeviceEventStore();

        store.AddEvent(
            new DeviceEvent("1", "A", "Phone", DateTime.UtcNow));

        store.AddEvent(
            new DeviceEvent("2", "B", "Phone", DateTime.UtcNow));

        store.AddEvent(
            new DeviceEvent("3", "C", "Speeding", DateTime.UtcNow));

        Assert.Equal(
            2,
            store.CountEventsByType("Phone"));
    }
}