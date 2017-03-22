using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace xAPI.Client.Resources
{
    /// <summary>
    /// The type of interaction.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InteractionType
    {
        /// <summary>
        /// Another type of interaction that does not fit into those
        /// defined above.
        /// </summary>
        [EnumMember(Value = "other")]
        Other = 0,

        /// <summary>
        /// An interaction with two possible responses: true or false.
        /// </summary>
        [EnumMember(Value = "true-false")]
        TrueFalse,

        /// <summary>
        /// An interaction with a number of possible choices from which
        /// the learner can select. This includes interactions in which
        /// the learner can select only one answer from the list and
        /// those where the learner can select multiple items.
        /// </summary>
        [EnumMember(Value = "choice")]
        Choice,

        /// <summary>
        /// An interaction which requires the learner to supply a short
        /// response in the form of one or more strings of characters.
        /// Typically, the correct response consists of part of a word,
        /// one word or a few words. "Short" means that the correct
        /// responses pattern and learner response strings will normally
        /// be 250 characters or less. 
        /// </summary>
        [EnumMember(Value = "fill-in")]
        FillIn,

        /// <summary>
        /// An interaction which requires the learner to supply a
        /// response in the form of a long string of characters. "Long"
        /// means that the correct responses pattern and learner response
        /// strings will normally be more than 250 characters.
        /// </summary>
        [EnumMember(Value = "long-fill-in")]
        LongFillIn,

        /// <summary>
        /// An interaction where the learner is asked to match items in
        /// one set (the source set) to items in another set (the target
        /// set). Items do not have to pair off exactly and it is
        /// possible for multiple or zero source items to be matched to
        /// a given target and vice versa.
        /// </summary>
        [EnumMember(Value = "matching")]
        Matching,

        /// <summary>
        /// An interaction that requires the learner to perform a task
        /// that requires multiple steps.
        /// </summary>
        [EnumMember(Value = "performance")]
        Performance,

        /// <summary>
        /// An interaction where the learner is asked to order items in
        /// a set.
        /// </summary>
        [EnumMember(Value = "sequencing")]
        Sequencing,

        /// <summary>
        /// An interaction which asks the learner to select from a
        /// discrete set of choices on a scale.
        /// </summary>
        [EnumMember(Value = "likert")]
        Likert,

        /// <summary>
        /// Any interaction which requires a numeric response from the
        /// learner.
        /// </summary>
        [EnumMember(Value = "numeric")]
        Numeric
    }
}
