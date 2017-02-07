using NUnit.Framework;

/// <summary>
/// JBT created all new unit tests, as Bugfree chose to create their own testing framework instead of using an existing one
/// </summary>
public class MapTest
{
    [Test]
    public void InitialiseMap()
    {
        Map m = new Map();

        Assert.AreEqual(Map.MAP_DIMENSIONS.x * Map.MAP_DIMENSIONS.y, m.GetTiles().Count);
    }

    [Test]
    public void OutOfBoundsTest()
    {
        Map m = new Map();

        Assert.Throws<System.IndexOutOfRangeException>(() => m.GetTile((int)(Map.MAP_DIMENSIONS.x * Map.MAP_DIMENSIONS.y) + 2));
    }

    [Test]
    public void UnownedTilesRemainingTest()
    {
        Map m = new Map();

        Assert.AreEqual(Map.MAP_DIMENSIONS.x * Map.MAP_DIMENSIONS.y, m.GetNumUnownedTilesRemaining());
    }
}
