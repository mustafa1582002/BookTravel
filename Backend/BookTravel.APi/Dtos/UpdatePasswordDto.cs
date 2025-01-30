namespace BookTravel.APi.Dtos
{
    public class UpdatePasswordDto
    {
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMessge.StringLength)]
        public string NewPassword { get; set; } = null!;
        [Compare("NewPassword", ErrorMessage = ErrorMessge.PasswordConfirm)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
