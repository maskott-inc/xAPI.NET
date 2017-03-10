using System;

namespace xAPI.Client.Requests
{
    public class GetStatementRequest : AGetStatementRequest
    {
        public Guid? StatementId { get; set; }

        public Guid? VoidedStatementId { get; set; }

        internal override void Validate()
        {
            base.Validate();

            if (!this.StatementId.HasValue && !this.StatementId.HasValue)
            {
                throw new ArgumentException($"{nameof(this.StatementId)} or {nameof(this.VoidedStatementId)} is required");
            }

            if (this.StatementId.HasValue && this.StatementId.HasValue)
            {
                throw new ArgumentException($"{nameof(this.StatementId)} and {nameof(this.VoidedStatementId)} cannot both be specified");
            }
        }
    }
}
