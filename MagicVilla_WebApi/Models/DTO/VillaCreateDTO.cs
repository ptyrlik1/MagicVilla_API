using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebApi.Models.DTO
{
    public class VillaCreateDTO
    {

       
        public string Name { get; set; }

        public string Details { get; set; }
        [Required]
        public int Occupancy { get; set; }

        public double Rate { get; set; }

        public int Sqft { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }
    }
}
