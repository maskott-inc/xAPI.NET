using System;
using xAPI.Client.Resources;

namespace xAPI.Client.Requests
{
    public abstract class AStateRequest : ARequest
    {
        public Uri ActivityId { get; set; }

        public Agent Agent { get; set; }

        public Guid? Registration { get; set; }

        internal override void Validate()
        {
            if (this.ActivityId == null)
            {
                throw new ArgumentNullException(nameof(this.ActivityId));
            }
            if (!this.ActivityId.IsAbsoluteUri)
            {
                throw new ArgumentException("IRI should be absolute", nameof(this.ActivityId));
            }
            if (this.Agent == null)
            {
                throw new ArgumentNullException(nameof(this.Agent));
            }
        }
    }
}
