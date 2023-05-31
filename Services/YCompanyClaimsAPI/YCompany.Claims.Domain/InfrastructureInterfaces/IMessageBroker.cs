namespace YCompany.Claims.Domain.InfrastructureInterfaces
{
    public interface IMessageBroker
    {
        Task<bool> CheckHealthAsync();
    }
}
