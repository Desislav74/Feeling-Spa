namespace FeelingSpa.Services.Data.Reservations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FeelingSpa.Data.Common.Repositories;
    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;
    using Microsoft.EntityFrameworkCore;


    public class ReservationsService : IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationsRepository;

        public ReservationsService(IDeletableEntityRepository<Reservation>reservationsRepository)
        {
            this.reservationsRepository = reservationsRepository;
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var reservation =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .To<T>().FirstOrDefaultAsync();
            return reservation;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var reservations =
                await this.reservationsRepository
                    .All()
                    .OrderByDescending(x => x.DateTime)
                    .To<T>().ToListAsync();
            return reservations;
        }
    }
}
