namespace xAPI.Client.Requests
{
    /// <summary>
    /// Tha base class for any xAPI request.
    /// </summary>
    public abstract class ARequest
    {
        internal abstract void Validate();
    }
}
