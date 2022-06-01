using System.Text.Json.Serialization;

namespace SharpCasts.Main.Models.Podcast.Fields;

public class EpisodeResponse
{
    [JsonPropertyName("podcast")]
    public Podcast Podcast { get; init; }
}
