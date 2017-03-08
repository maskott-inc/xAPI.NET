using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public static class PutStateRequest
    {
        public static PutStateRequest<T> Create<T>(StateDocument<T> state)
        {
            return new PutStateRequest<T>(state);
        }
    }

    public class PutStateRequest<T> : ASingleStateRequest
    {
        public StateDocument<T> State { get; private set; }

        internal PutStateRequest(StateDocument<T> state)
        {
            this.State = state;
        }

        internal override void Validate()
        {
            base.Validate();

            if (this.State == null)
            {
                throw new ArgumentNullException(nameof(this.State));
            }
        }
    }
}
