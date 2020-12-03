namespace FeelingSpa.Services.Data.SalonServices
{
    using System.Threading.Tasks;

    public interface ISalonServicesService
    {
        Task<T> GetByIdAsync<T>(string salonId, int serviceId);
    }
}
