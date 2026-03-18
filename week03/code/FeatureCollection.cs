using System.Text.Json.Serialization;

public class FeatureCollection
{
    // Olaku: this property maps to the top-level "features" array in the USGS JSON feed.
    // We only model the pieces this assignment actually uses so the file stays easy to review.
    public List<Feature> Features { get; set; } = [];
}

public class Feature
{
    // Olaku: each feature has a nested "properties" object, and that is where
    // the place name and magnitude live in the earthquake feed.
    public FeatureProperties Properties { get; set; } = new();
}

public class FeatureProperties
{
    // Olaku: "mag" is numeric in the feed, so I store it as nullable double
    // in case the API returns a record without a magnitude.
    [JsonPropertyName("mag")]
    public double? Magnitude { get; set; }

    // Olaku: "place" is the human-readable earthquake location shown in the summary.
    [JsonPropertyName("place")]
    public string Place { get; set; } = "";
}
