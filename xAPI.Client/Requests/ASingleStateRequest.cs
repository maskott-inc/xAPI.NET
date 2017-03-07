using System;

namespace xAPI.Client.Requests
{
    public abstract class ASingleStateRequest : AStateRequest
    {
        public string StateId { get; set; }

        internal override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(this.StateId))
            {
                throw new ArgumentNullException(nameof(this.StateId));
            }
        }
    }
}
