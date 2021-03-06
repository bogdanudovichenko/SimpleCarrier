﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCarrier.API.ViewModels.Auth
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}