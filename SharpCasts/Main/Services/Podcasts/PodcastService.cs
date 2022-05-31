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
        string endpoint = Preferences.Get("Endpoint", "");
        string accessToken = Preferences.Get("AccessToken", "");

        this.client = new GraphQLHttpClient(
            endpoint,
            new SystemTextJsonSerializer());

        this.client.HttpClient.DefaultRequestHeaders
                                .Add("Authorization", $"Bearer {accessToken}");
    }

    /// <summary>
    /// Gets a podcast from the API.
    /// </summary>
    /// <param name="podcastId">The identifier of the podcast to get.</param>
    /// <returns>The podcast which matched the supplied identifier.</returns>
    public async Task<Podcast> GetPodcast(int podcastId)
    {
        PodcastIdentifier identifier = new()
        {
            Id = podcastId.ToString(),
            Type = PodcastIdentifierType.PODCHASER
        };

        GraphQLHttpRequest request = new()
        {
            Query = @"
            query GetPodcast($identifier: PodcastIdentifier!) {
                podcast(identifier: $identifier) {
                    id,
                    title,
                    description,
                    imageUrl,
                    author {
                        name
                    }
                }
            }",
            OperationName = "GetPodcast",
            Variables = new { identifier }
        };

        var response = await this.client.SendQueryAsync<PodcastResponse>(request);
        Podcast podcast = response.Data.Data.Podcasts.FirstOrDefault();

        return podcast;
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
            Variables = new { search }
        };

        var response = await this.client.SendQueryAsync<PodcastResponse>(request);
        List<Podcast> podcasts = response.Data.Data.Podcasts;

        return podcasts;
    }

    /// <summary>
    /// Gets a list of episodes from a podcast.
    /// </summary>
    /// <param name="podcastId">The podcast to get episodes from.</param>
    /// <returns>Episodes from that podcast.</returns>
    public async Task<List<Episode>> GetEpisodes(int podcastId)
    {
        PodcastIdentifier identifier = new()
        {
            Id = podcastId.ToString(),
            Type = PodcastIdentifierType.PODCHASER
        };

        GraphQLHttpRequest request = new()
        {
            Query = @"
            query GetEpisodes($identifier: PodcastIdentifier!) {
                podcast(identifier: $identifier) {
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
            Variables = new { identifier }
        };

        var response = await this.client.SendQueryAsync<dynamic>(request);

        return null;
    }
}