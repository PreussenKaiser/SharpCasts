using Newtonsoft.Json;

namespace SharpCasts.Models;

/// <summary>
/// The model that represents an access token for the Podchaser API.
/// </summary>
public class AccessToken
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    [JsonProperty("access_token")]
    public string Token { get; set; }

    /// <summary>
    /// gets or sets when the token expires.
    /// </summary>
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Gets or sets the token's type.
    /// </summary>
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
}
