namespace FeelingSpa.Data.Models
{
    using System;

    using FeelingSpa.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int SalonId { get; set; }

        public Salon Salon { get; set; }

        public string Extension { get; set; }
    }
}
