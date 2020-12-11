using System;

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

        public async Task<IEnumerable<T>> GetReservationsByUserAsync<T>(string userId)
        {
            var appointments =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.UserId == userId
                                && x.DateTime.Date > DateTime.UtcNow.Date)
                    .OrderBy(x => x.DateTime)
                    .To<T>().ToListAsync();
            return appointments;
        }

        public async Task<IEnumerable<T>> GetPastByUserAsync<T>(string userId)
        {
            var reservations =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.UserId == userId
                                && x.DateTime.Date < DateTime.UtcNow.Date
                                && x.Confirmed == true)
                    .OrderBy(x => x.DateTime)
                    .To<T>().ToListAsync();
            return reservations;
        }

        public async Task AddAsync(string userId, string salonId, int serviceId, DateTime dateTime)
        {
            await this.reservationsRepository.AddAsync(new Reservation
            {
                Id = Guid.NewGuid().ToString(),
                DateTime = dateTime,
                UserId = userId,
                SalonId = salonId,
                ServiceId = serviceId,
            });
            await this.reservationsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var appointment =
                await this.reservationsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            this.reservationsRepository.Delete(appointment);
            await this.reservationsRepository.SaveChangesAsync();
        }
    }
}
