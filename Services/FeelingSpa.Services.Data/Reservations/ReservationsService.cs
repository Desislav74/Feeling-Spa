namespace FeelingSpa.Services.Data.Reservations
{
    using System;
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

        public ReservationsService(IDeletableEntityRepository<Reservation> reservationsRepository)
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
            var reservations =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.UserId == userId
                                && x.DateTime.Date > DateTime.UtcNow.Date)
                    .OrderBy(x => x.DateTime)
                    .To<T>().ToListAsync();
            return reservations;
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
            var reservation =
                await this.reservationsRepository
                    .AllAsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            this.reservationsRepository.Delete(reservation);
            await this.reservationsRepository.SaveChangesAsync();
        }

        public async Task RateReservationAsync(string id)
        {
            var reservation =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            reservation.IsSalonRatedByUser = true;
            await this.reservationsRepository.SaveChangesAsync();
        }

        public async Task ConfirmAsync(string id)
        {
            var reservation =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            reservation.Confirmed = true;
            await this.reservationsRepository.SaveChangesAsync();
        }

        public async Task DeclineAsync(string id)
        {
            var reservation =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();
            reservation.Confirmed = false;
            await this.reservationsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllBySalonAsync<T>(string salonId)
        {
            var reservations =
                await this.reservationsRepository
                    .All()
                    .Where(x => x.SalonId == salonId)
                    .OrderByDescending(x => x.DateTime)
                    .To<T>().ToListAsync();
            return reservations;
        }
    }
}
