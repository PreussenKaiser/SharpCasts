using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using SharpCasts.Models;

namespace SharpCasts.Services;

/// <summary>
/// The service which gets podcasts from the Podchaser API.
/// </summary>
public class PodcastService : IPodcastService
{
    private readonly GraphQLHttpClient client;

    /// <summary>
    /// Initializes a new instance of the PodcastService service.
    /// </summary>
    public PodcastService()
    {
        this.client = new GraphQLHttpClient(
            "https://api.podchaser.com/graphql",
            new NewtonsoftJsonSerializer());
    }

    public async Task<IEnumerable<Podcast>> GetPodcasts()
    {
        var request = new GraphQLHttpRequest
        {
            Query = @"
            mutation {
                requestAccessToken(
                    input: {
                        grant_type: CLIENT_CREDENTIALS
                        client_id: ""9658c114-cb86-4b8f-bee4-4dccd513ffb5""
                        client_secret: ""HBUpbYzpiODqqgALt1z5ujf7ern0buWKhOjK0jbk""
                    }
                ) {
                    access_token
                    expires_in
                    token_type
                }
            }"
        };

        var response = await this.client.SendMutationAsync<dynamic>(request);

        return new List<Podcast>();
    }
}
