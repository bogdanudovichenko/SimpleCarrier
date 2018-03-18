using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCarrier.API.ViewModels.Auth
{
    public class RefreshTokenModel
    {
        [Required]
        public String RefreshToken { get; set; }
    }
}