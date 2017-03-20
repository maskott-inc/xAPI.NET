using System;
using System.Text;
using System.Threading.Tasks;
using xAPI.Client.Configuration;

namespace xAPI.Client.Authenticators
{
    /// <summary>
    /// This authenticator provides authentication through Basic HTTP
    /// credentials, i.e. "Authorization: Basic {b64(username:password)}".
    /// </summary>
    public class BasicHttpAuthenticator : ILRSAuthenticator
    {
        private readonly string _username;
        private readonly string _password;

        /// <summary>
        /// Creates a new instance of BasicHttpAuthenticator using the
        /// given configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public BasicHttpAuthenticator(BasicEndpointConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            if (string.IsNullOrEmpty(config.Username))
            {
                throw new ArgumentNullException(nameof(config.Username));
            }
            if (string.IsNullOrEmpty(config.Password))
            {
                throw new ArgumentNullException(nameof(config.Password));
            }

            this._username = config.Username;
            this._password = config.Password;
        }

        Task<AuthorizationHeaderInfos> ILRSAuthenticator.GetAuthorization()
        {
            if (string.IsNullOrEmpty(this._username))
            {
                throw new ArgumentNullException(nameof(this._username));
            }
            if (string.IsNullOrEmpty(this._password))
            {
                throw new ArgumentNullException(nameof(this._password));
            }

            var result = new AuthorizationHeaderInfos()
            {
                Scheme = "Basic",
                Parameter = this.GetEncodedCredentials()
            };
            return Task.FromResult(result);
        }

        private string GetEncodedCredentials()
        {
            string clearText = $"{this._username}:{this._password}";
            byte[] bytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(clearText);
            return Convert.ToBase64String(bytes);
        }
    }
}
