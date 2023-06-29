namespace BookTravel.APi.Dtos
{
    public class LoginDto
    {
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMessge.StringLength)]
        public string Password { get; set; } = null!;
    }
}
