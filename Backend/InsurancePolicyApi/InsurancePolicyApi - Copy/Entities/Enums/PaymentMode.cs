namespace InsurancePolicyApi.Entities.Enums
{
    /// <summary>Mode of a simulated premium payment (§9.6).</summary>
    public enum PaymentMode
    {
        UPI,
        Card,
        NetBanking,
        Cash
    }
}
