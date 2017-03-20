namespace xAPI.Client.Resources
{
    /// <summary>
    /// A representation which defines an Actor uniquely.
    /// </summary>
    public class Agent : Actor
    {
        /// <summary>
        /// Always "Agent".
        /// </summary>
        public override string ObjectType => "Agent";
    }
}
