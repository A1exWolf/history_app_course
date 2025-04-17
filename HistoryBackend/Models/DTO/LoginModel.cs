﻿using System.ComponentModel.DataAnnotations;

namespace HistoryBackend.Models.DTO;

public class LoginModel
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}