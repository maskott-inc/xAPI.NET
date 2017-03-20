namespace xAPI.Client.Resources
{
    /// <summary>
    /// The statement target defines the thing that was acted on.
    /// The target of a Statement can be an Activity, Agent/Group,
    /// SubStatement, or Statement Reference.
    /// </summary>
    public interface IStatementTarget : IObjectResource
    {
    }

    /// <summary>
    /// A sub statement's target has the same requirements as a statement's
    /// target, except it cannot be another sub statement.
    /// </summary>
    public interface ISubStatementTarget : IObjectResource
    {
    }
}
