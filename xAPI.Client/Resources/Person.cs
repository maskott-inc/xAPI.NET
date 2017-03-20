using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A set of one or more representations which defines an Actor uniquely. Conceptually,
    /// this is like having a "home email" and a "work email". Both are the same person,
    /// but have different data, associations, etc.
    /// </summary>
    public class Person : IObjectResource
    {
        /// <summary>
        /// List of names of Agents retrieved.
        /// </summary>
        [JsonProperty("name")]
        public List<string> Name { get; set; }

        /// <summary>
        /// List of e-mail addresses of Agents retrieved.
        /// </summary>
        [JsonProperty("mbox")]
        public List<Uri> MBox { get; set; }

        /// <summary>
        /// List of the SHA1 hashes of mailto IRIs (such as go in an mbox property).
        /// </summary>
        [JsonProperty("mbox_sha1sum")]
        public List<string> MBoxSHA1Sum { get; set; }

        /// <summary>
        /// List of openids that uniquely identify the Agents retrieved.
        /// </summary>
        [JsonProperty("openid")]
        public List<Uri> OpenId { get; set; }

        /// <summary>
        /// List of accounts to match. Complete account Objects (homePage and name)
        /// MUST be provided.
        /// </summary>
        [JsonProperty("account")]
        public List<AccountObject> Account { get; set; }

        /// <summary>
        /// Always "Person".
        /// </summary>
        public string ObjectType => "Person";

    }
}
