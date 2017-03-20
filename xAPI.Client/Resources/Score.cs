using Newtonsoft.Json;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// An optional property that represents the outcome of a graded
    /// Activity achieved by an Agent.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// The score related to the experience as modified by scaling
        /// and/or normalization.
        /// </summary>
        [JsonProperty("scaled")]
        public decimal? Scaled { get; set; }

        /// <summary>
        /// The score achieved by the Actor in the experience described
        /// by the Statement. This is not modified by any scaling or
        /// normalization.
        /// </summary>
        [JsonProperty("raw")]
        public decimal? Raw { get; set; }

        /// <summary>
        /// The lowest possible score for the experience described by
        /// the Statement.
        /// </summary>
        [JsonProperty("min")]
        public decimal? Min { get; set; }

        /// <summary>
        /// The highest possible score for the experience described by
        /// the Statement.
        /// </summary>
        [JsonProperty("max")]
        public decimal? Max { get; set; }
    }
}
