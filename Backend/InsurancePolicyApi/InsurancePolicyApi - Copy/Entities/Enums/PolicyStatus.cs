namespace InsurancePolicyApi.Entities.Enums
{
    /// <summary>Policy lifecycle states (§14, POL-BR-004..007).</summary>
    public enum PolicyStatus
    {
        PendingPayment,
        Active,
        Expired,
        Cancelled
    }
}
