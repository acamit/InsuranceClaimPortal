namespace YCompany.Payments.Domain.InfrastructureInterfaces
{
    public interface IPaymentsStorageService
    {
        Task<bool> CheckHealthAsync();
    }
}
