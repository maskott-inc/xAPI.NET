using Newtonsoft.Json;
using System.Collections.Generic;
using xAPI.Client.Validation;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// A map of the types of learning activity context that this Statement is related to.
    /// </summary>
    public class ContextActivities
    {
        /// <summary>
        /// An Activity with a direct relation to the Activity which is the Object of the
        /// Statement. In almost all cases there is only one sensible parent or none, not
        /// multiple. For example: a Statement about a quiz question would have the quiz
        /// as its parent Activity.
        /// </summary>
        [JsonProperty("parent")]
        [ValidateProperty]
        public List<Activity> Parent { get; set; }

        /// <summary>
        /// An Activity with an indirect relation to the Activity which is the Object of
        /// the Statement. For example: a course that is part of a qualification. The course
        /// has several classes. The course relates to a class as the parent, the
        /// qualification relates to the class as the grouping.
        /// </summary>
        [JsonProperty("grouping")]
        [ValidateProperty]
        public List<Activity> Grouping { get; set; }

        /// <summary>
        /// An Activity used to categorize the Statement. "Tags" would be a synonym. Category
        /// SHOULD be used to indicate a profile of xAPI behaviors, as well as other
        /// categorizations. For example: Anna attempts a biology exam, and the Statement is
        /// tracked using the cmi5 profile. The Statement's Activity refers to the exam, and
        /// the category is the cmi5 profile.
        /// </summary>
        [JsonProperty("category")]
        [ValidateProperty]
        public List<Activity> Category { get; set; }

        /// <summary>
        /// A contextActivity that doesn't fit one of the other properties. For example: Anna
        /// studies a textbook for a biology exam. The Statement's Activity refers to the
        /// textbook, and the exam is a contextActivity of type other.
        /// </summary>
        [JsonProperty("other")]
        [ValidateProperty]
        public List<Activity> Other { get; set; }
    }
}
