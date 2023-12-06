using System.ComponentModel.DataAnnotations;

namespace TeamWebApplicationAPI.Models
{
    public record LoginDetails
    {
        [Required]
        public int? UserId { get; init; }
        [Required]
        public string Password { get; init; }
    }
}
