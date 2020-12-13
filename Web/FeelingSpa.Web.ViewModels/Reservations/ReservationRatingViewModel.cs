﻿using System.Linq;
using AutoMapper;

namespace FeelingSpa.Web.ViewModels.Reservations
{
    using System.ComponentModel.DataAnnotations;

    using FeelingSpa.Data.Models;
    using FeelingSpa.Services.Mapping;

    public class ReservationRatingViewModel : IMapFrom<Reservation>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string SalonId { get; set; }

        public string SalonName { get; set; }

        public string SalonCategoryName { get; set; }

        public string SalonCityName { get; set; }

        public string SalonAddress { get; set; }

        public string SalonImageUrl { get; set; }

        public bool? IsSalonRatedByUser { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Please choose a valid number of stars from 1 to 5.")]
        public int RateValue { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reservation, ReservationRatingViewModel>()
                .ForMember(x => x.SalonImageUrl, opt =>
                    opt.MapFrom(x =>
                        "/images/salons/" + x.Salon.Images.FirstOrDefault().Id + "." + x.Salon.Images.FirstOrDefault().Extension));
        }
    }
}
