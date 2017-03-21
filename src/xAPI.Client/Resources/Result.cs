using Newtonsoft.Json;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An optional property that represents a measured outcome related to
    /// the Statement in which it is included.
    /// </summary>
    public class Result
    {
        /// <summary>
        /// The score of the Agent in relation to the success or quality of
        /// the experience.
        /// </summary>
        [JsonProperty("score")]
        [ValidateProperty]
        public Score Score { get; set; }

        /// <summary>
        /// Indicates whether or not the attempt on the Activity was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool? Success { get; set; }

        /// <summary>
        /// Indicates whether or not the Activity was completed.
        /// </summary>
        [JsonProperty("completion")]
        public bool? Completion { get; set; }

        /// <summary>
        /// A response appropriately formatted for the given Activity.
        /// </summary>
        [JsonProperty("response")]
        public string Response { get; set; }

        /// <summary>
        /// Period of time over which the Statement occurred.
        /// </summary>
        [JsonProperty("duration")]
        public string Duration { get; set; }

        /// <summary>
        /// A map of other properties as needed.
        /// </summary>
        [JsonProperty("extensions")]
        [ValidateProperty]
        public Extensions Extensions { get; set; }
    }
}
