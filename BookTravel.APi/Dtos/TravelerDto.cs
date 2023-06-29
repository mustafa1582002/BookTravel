namespace BookTravel.APi.Dtos
{
    public class TravelerDto
    {

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [MaxLength(100)]
        public string UserName { get; set; } = null!;
        [MaxLength(100)]
        public string FirstName { get; set; } = null!;
        [MaxLength(100)]
        public string LastName { get; set; } = null!;
        [MaxLength(11)]
        [RegularExpression("^01[0125]{1}[0-9]{8}", ErrorMessage = ErrorMessge.Regex)]
        public string PhoneNumber { get; set; } = null!;
        [StringLength(30, MinimumLength = 8, ErrorMessage = ErrorMessge.StringLength)]
        public string Password { get; set; } = null!;
        [Compare("Password", ErrorMessage = ErrorMessge.PasswordConfirm)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
