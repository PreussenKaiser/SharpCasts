using Microsoft.Extensions.Configuration;
using SharpCasts.Core.Exceptions;
using SharpCasts.Core.Models;
using SharpCasts.Core.Services;
using SharpCasts.Infrastructure.Responses;
using System.Text.Json;
using System.Xml.Serialization;

namespace SharpCasts.Infrastructure.Services;

/// <summary>
/// The service which gets podcasts from the <see href="https://allfeeds.ai/api">AllFeeds</see> API.
/// </summary>
public class PodcastService : IPodcastService
{
    /// <summary>
    /// The client to send requests with.
    /// </summary>
    private readonly HttpClient client;

    /// <summary>
    /// The key for the API.
    /// </summary>
    private readonly string apiKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastService"/> class.
    /// </summary>
    /// <param name="config">Configuration for the services <see cref="HttpClient"/>.</param>
    public PodcastService(IConfiguration config)
    {
        this.client = new HttpClient()
        {
            BaseAddress = new Uri(config["Api:Endpoint"])
        };

        this.apiKey = config["Api:Key"];
    }

    /// <summary>
    /// Gets a podcast by it's unique identifier.
    /// </summary>
    /// <param name="podcastId">The identifier of the podcast.</param>
    /// <returns>The found podcast.</returns>
    public async Task<Podcast> GetPodcastAsync(int podcastId)
    {
        string query = $"podcast_by_itunesid?key={this.apiKey}&itunes_id={podcastId}";

        HttpRequestMessage request = new(HttpMethod.Get, query);
        HttpResponseMessage response = await this.client.SendAsync(request);

        using Stream body = await response.Content.ReadAsStreamAsync();
        var podcast = await JsonSerializer.DeserializeAsync<Podcast>(body);

        return podcast;
    }

    /// <summary>
    /// Searches for a podcast in the API.
    /// </summary>
    /// <param name="search">The search term to use.</param>
    /// <returns>An enumerable of podcasts.</returns>
    /// <exception cref="UnknownPodcastException">Thrown when the search term returns no results.</exception>
    public async Task<IEnumerable<Podcast>> SearchPodcastsAsync(string search)
    {
        string query = $"find_podcasts?key={this.apiKey}&keyword={search}";

        HttpRequestMessage request = new(HttpMethod.Get, query);
        HttpResponseMessage response = await this.client.SendAsync(request);

        using Stream body = await response.Content.ReadAsStreamAsync();
        PodcastResponse podcasts = new();

        try
        {
            podcasts = await JsonSerializer.DeserializeAsync<PodcastResponse>(body);
        }
        catch (JsonException)
        {
            throw new UnknownPodcastException($"Could not find any podcasts matching '{search}'");
        }

        return podcasts.Podcasts;
    }

    /// <summary>
    /// Gets a channel from a feed.
    /// </summary>
    /// <param name="feedUrl">The feed to get the channel from.</param>
    /// <returns>The channel which hosts the feed.</returns>
    public async Task<Channel> GetChannelAsync(string feedUrl)
    {
        HttpRequestMessage request = new(HttpMethod.Get, feedUrl);
        HttpResponseMessage response = await this.client.SendAsync(request);

        using Stream body = await response.Content.ReadAsStreamAsync();
        XmlSerializer serializer = new(typeof(EpisodeResponse));

        var episodes = (EpisodeResponse)serializer.Deserialize(body);

        return episodes.Channel;
    }
}
