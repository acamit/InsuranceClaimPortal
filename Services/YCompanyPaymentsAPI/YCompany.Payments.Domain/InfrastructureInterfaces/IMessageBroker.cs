namespace YCompany.Payments.Domain.InfrastructureInterfaces
{
    public interface IMessageBroker
    {
        Task<bool> CheckHealthAsync();
    }
}
