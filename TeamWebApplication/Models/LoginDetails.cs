using Microsoft.Build.Framework;

namespace TeamWebApplication.Models
{
    public record LoginDetails
    {
        [Required]
        public int? UserId { get; init; } = null;
        [Required]
        public string Password { get; init; }
    }
}
