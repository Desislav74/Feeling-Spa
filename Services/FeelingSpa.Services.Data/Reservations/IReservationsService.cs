﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeelingSpa.Services.Data.Reservations
{
    public interface IReservationsService
    {
        Task<T> GetByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllAsync<T>();
    }
}
