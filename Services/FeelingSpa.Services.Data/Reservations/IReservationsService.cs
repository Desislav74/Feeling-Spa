using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeelingSpa.Services.Data.Reservations
{
    public interface IReservationsService
    {
        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<T>> GetReservationsByUserAsync<T>(string userId);

        Task<IEnumerable<T>> GetPastByUserAsync<T>(string userId);

        Task AddAsync(string userId, string salonId, int serviceId, DateTime dateTime);
    }
}
