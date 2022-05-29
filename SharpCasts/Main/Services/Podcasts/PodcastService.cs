using SharpCasts.Main.Models;

using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace SharpCasts.Main.Services.Podcasts;

/// <summary>
/// The service which gets podcasts from the Podchaser API.
/// <br/>
/// Documentation for which can be found at <see href="https://api-docs.podchaser.com/"/>.
/// </summary>
public class PodcastService : IPodcastService
{
    /// <summary>
    /// The client to query Podchaser with.
    /// </summary>
    private readonly GraphQLHttpClient client;

    /// <summary>
    /// Initializes a new instance of the <see cref="PodcastService"/> service.
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
        GraphQLHttpRequest request = new()
        {
            Query = @"
            query {
                podcasts {
                    data {
                        id,
                        title,
                        description,
                        imageUrl
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

    /// <summary>
    /// Gets podcasts from the API that match the search term.
    /// </summary>
    /// <param name="search">The search term to filter with.</param>
    /// <returns>A list of podcasts that match the query.</returns>
    public async Task<List<Podcast>> SearchPodcasts(string search)
    {
        GraphQLHttpRequest request = new()
        {
            Query = @"
            query Search($search: String) {
                podcasts(
                    searchTerm: $search
                ) {
                    data {
                        id,
                        title,
                        description,
                        imageUrl
                    }
                }
            }",
            OperationName = "Search",
            Variables = new { search = search }
        };

        var response = await this.client.SendQueryAsync<PodcastResponse>(request);
        List<Podcast> podcasts = response.Data.Data.Podcasts;

        return podcasts;
    }
}