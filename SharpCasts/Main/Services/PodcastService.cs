using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

using SharpCasts.Main.Models;

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
        this.client = new GraphQLHttpClient(
            Preferences.Get("endpoint", ""),
            new NewtonsoftJsonSerializer());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Podcast>> GetPodcasts()
    {
        return new List<Podcast>();
    }
}
