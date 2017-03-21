using Newtonsoft.Json;
using System.Collections.Generic;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A Group represents a collection of Agents and can be used in most of the
    /// same situations an Agent can be used. There are two types of Groups:
    /// Anonymous Groups and Identified Groups.
    /// </summary>
    public class Group : Actor
    {
        /// <summary>
        /// The members of this Group. This is an unordered list.
        /// </summary>
        [JsonProperty("member")]
        [ValidateProperty]
        public List<Agent> Members { get; set; }

        /// <summary>
        /// Always "Group".
        /// </summary>
        public override string ObjectType => "Group";
    }
}
