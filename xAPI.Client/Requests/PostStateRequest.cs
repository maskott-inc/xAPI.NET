using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public static class PostStateRequest
    {
        public static PostStateRequest<T> Create<T>(StateDocument<T> state)
        {
            return new PostStateRequest<T>(state);
        }
    }

    public class PostStateRequest<T> : ASingleStateRequest
    {
        public StateDocument<T> State { get; private set; }

        internal PostStateRequest(StateDocument<T> state)
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
