using System.ComponentModel.DataAnnotations;
using api.Models;

namespace api.Dtos.Event
{
    // DTO for event creations 
    public class CreateEventRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Must be at least 5 characters")]
        [MaxLength(60, ErrorMessage = "Must be less than 60 characters")]
        public string title { get; set; } = string.Empty;
        [Required]
        [MinLength(20, ErrorMessage = "Must be at least 20 characters")]
        [MaxLength(250, ErrorMessage = "Must be less than 250 characters")]
        public string description { get; set; } = string.Empty;
        [Required]
        [MinLength(20, ErrorMessage = "Must be at least 10 characters")]
        [MaxLength(250, ErrorMessage = "Must be less than 100 characters")]
        public string address { get; set; } = string.Empty;
        public double lat { get; set; }
        public double lon { get; set; }
        [Required]
        public EventType type { get; set; }
        [Required]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/((19|20)\d\d)$", 
            ErrorMessage = "Invalid date format. Use DD/MM/YYYY.")]
        public string date {get; set; } = string.Empty;
        [Required]
        [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d$", 
        ErrorMessage = "Invalid time format. Use HH:MM (24-hour format).")]
        public string time { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
        public string imageUrl {get; set;} = string.Empty;
    }
}