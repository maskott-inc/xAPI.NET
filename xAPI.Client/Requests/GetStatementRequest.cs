using System;

namespace xAPI.Client.Requests
{
    /// <summary>
    /// Class used to GET a single statement from the LRS.
    /// </summary>
    public class GetStatementRequest : AGetStatementRequest
    {
        /// <summary>
        /// Id of Statement to fetch.
        /// </summary>
        public Guid? StatementId { get; set; }

        /// <summary>
        /// Id of voided Statement to fetch.
        /// </summary>
        public Guid? VoidedStatementId { get; set; }

        internal override void Validate()
        {
            base.Validate();

            if (!this.StatementId.HasValue && !this.VoidedStatementId.HasValue)
            {
                throw new ArgumentException($"{nameof(this.StatementId)} or {nameof(this.VoidedStatementId)} is required");
            }

            if (this.StatementId.HasValue && this.VoidedStatementId.HasValue)
            {
                throw new ArgumentException($"{nameof(this.StatementId)} and {nameof(this.VoidedStatementId)} cannot both be specified");
            }
        }
    }
}
