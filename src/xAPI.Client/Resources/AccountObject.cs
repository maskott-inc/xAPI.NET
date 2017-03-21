using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A user account on an existing system, such as a private system
    /// (LMS or intranet) or a public system (social networking site).
    /// </summary>
    public class AccountObject
    {
        /// <summary>
        /// The canonical home page for the system the account is on.
        /// This is based on FOAF's accountServiceHomePage.
        /// </summary>
        [JsonProperty("homePage", Required = Required.Always)]
        [Required]
        public Uri HomePage { get; set; }

        /// <summary>
        /// The unique id or name used to log in to this account.
        /// This is based on FOAF's accountName.
        /// </summary>
        [JsonProperty("name", Required = Required.Always)]
        [Required]
        public string Name { get; set; }
    }
}
