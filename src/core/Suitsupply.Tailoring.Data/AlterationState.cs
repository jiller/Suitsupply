namespace Suitsupply.Tailoring.Data
{
    /// <summary>
    /// Possible alteration states
    /// </summary>
    public enum AlterationState
    {
        /// <summary>
        /// Created - when a new alteration posted by salesrep
        /// </summary>
        Created = 1,
        /// <summary>
        /// Paid - when an alteration was paid by customer
        /// </summary>
        Paid = 2,
        /// <summary>
        /// Done - when an alteration was done by tailor
        /// </summary>
        Done = 3
    }
}