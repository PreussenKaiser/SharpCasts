using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using SharpCasts.Main.Models;
using System.Text.Json;

namespace SharpCasts.Main.Services;

/// <summary>
/// The service which gets podcasts from the Podchaser API.
/// </summary>
public class PodcastService : IPodcastService
{
    /// <summary>
    /// The client to query Podchaser with.
    /// </summary>
    private readonly GraphQLHttpClient client;

    /// <summary>
    /// Initializes a new instance of the PodcastService service.
    /// </summary>
    public PodcastService()
    {
        string endpoint = Preferences.Get("endpoint", "");
        string accessToken = Preferences.Get("access_token", "");

        this.client = new GraphQLHttpClient(
            endpoint,
            new SystemTextJsonSerializer());

        this.client.HttpClient.DefaultRequestHeaders
                                .Add("Authorization", $"Bearer {accessToken}");
    }

    /// <summary>
    /// Gets podcasts from Podchaser
    /// </summary>
    /// <returns>The latest 10 podcasts.</returns>
    public async Task<IEnumerable<Podcast>> GetPodcasts()
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

        var response = await this.client.SendQueryAsync<PodcastResponse>(request);
        List<Podcast> podcasts = response.Data.Data.Podcasts;

        return podcasts;
    }
}