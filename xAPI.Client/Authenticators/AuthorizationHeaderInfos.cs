namespace xAPI.Client.Authenticators
{
    /// <summary>
    /// Represents the contents of the HTTP Authorization header.
    /// </summary>
    public class AuthorizationHeaderInfos
    {
        /// <summary>
        /// The Authorization scheme (e.g. : "Basic", "Bearer"...).
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// The Authorization parameter (e.g. Basic HTTP credentials,
        /// an OAuth access token...).
        /// </summary>
        public string Parameter { get; set; }
    }
}
