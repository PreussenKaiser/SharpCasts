using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

using SharpCasts.Main.Models;

namespace SharpCasts.Main.Services;

/// <summary>
/// The service which gets podcasts from the Podchaser API.
/// <br></br>
/// Documentation for which can be found <see href="https://api-docs.podchaser.com/">here</see>.
/// </summary>
public class PodcastService : IPodcastService
{
    /// <summary>
    /// The client to query Podchaser with.
    /// </summary>
    private readonly GraphQLHttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastService">PodcastService</see> service.
    /// </summary>
    public PodcastService()
    {
        string endpoint = Preferences.Get("endpoint", "");
        string accessToken = Preferences.Get("access_token", "");

        client = new GraphQLHttpClient(
            endpoint,
            new SystemTextJsonSerializer());

        client.HttpClient.DefaultRequestHeaders
                                .Add("Authorization", $"Bearer {accessToken}");
    }

    /// <summary>
    /// Gets podcasts from Podchaser
    /// </summary>
    /// <returns>The latest 10 podcasts.</returns>
    public async Task<IEnumerable<Podcast>> GetAllPodcasts()
    {
        var request = new GraphQLHttpRequest
        {
            Query = @"
            query {
                podcasts {
                    data {
                        id,
                        title,
                        description
                    }
                }
            }",
        };

        var response = await client.SendQueryAsync<PodcastResponse>(request);
        List<Podcast> podcasts = response.Data.Data.Podcasts;

        return podcasts;
    }

    /// <summary>
    /// Gets a podcast from the API.
    /// </summary>
    /// <param name="podcastId">The identifier of the podcast to get.</param>
    /// <returns>The podcast which matched the supplied identifier.</returns>
    public Task<Podcast> GetPodcast(int podcastId)
    {
        throw new NotImplementedException();
    }
}