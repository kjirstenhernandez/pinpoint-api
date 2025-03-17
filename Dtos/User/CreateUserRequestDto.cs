using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class CreateUserRequestDto
    {
        [Required]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters")]
        [MaxLength(15, ErrorMessage = "Must be less than 15 characters")]
        public string username { get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Must be at least 1 character")]
        [MaxLength(20, ErrorMessage = "Must be less than 20 characters")]
        public string firstName {get; set; } = string.Empty;
        [Required]
        [MinLength(1, ErrorMessage = "Must be at least 1 character")]
        [MaxLength(30, ErrorMessage = "Must be less than 30 characters")]
        public string lastName {get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Zipcode must be 5 digits.")]
        public int zipcode { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Must be a valid email format: johndoe@example.com")]
        public string email { get; set; } = string.Empty;
        [Required]
        [Phone(ErrorMessage = "Must be a valid phone number format")]
        public string phone { get; set; } = string.Empty;
    }
}