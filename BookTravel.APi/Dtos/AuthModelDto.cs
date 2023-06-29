namespace BookTravel.APi.Dtos
{
    public class AuthModelDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public bool IsAuthenticated { get; set; }
        public List<string> Roles { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime ExpireOn { get; set; }
    }
}
