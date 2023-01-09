﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_WebApi.Models.DTO
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }

        public string SpecialDetails { get; set; }
    }
}
