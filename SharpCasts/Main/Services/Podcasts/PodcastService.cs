using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using SharpCasts.Main.Models.Podcast;
using SharpCasts.Main.Models.Podcast.Fields;

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
                podcasts(searchTerm: $search) {
                    data {
                        id,
                        title,
                        description,
                        imageUrl,
                        author {
                            name
                        }
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

    /// <summary>
    /// Gets a list of episodes from a podcast.
    /// </summary>
    /// <param name="podcastId"></param>
    /// <returns></returns>
    public async Task<List<Episode>> GetEpisodes(int podcastId)
    {
        GraphQLHttpRequest request = new()
        {
            Query = @"
            query GetEpisodes($id: PodcastIdentifier!) {
                podcast(identifier: $id) {
                    episodes {
                        data {
                            title,
                            description,
                            airDate,
                            audioUrl
                        }
                    }
                }
            }",
            OperationName = "GetEpisodes",
            Variables = new { id = podcastId }
        };

        var response = await this.client.SendQueryAsync<EpisodeList>(request);
        List<Episode> episodes = response.Data.Episodes;

        return episodes;
    }
}