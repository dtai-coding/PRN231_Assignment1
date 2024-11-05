namespace PRN231PE_FA23_665511_taipdse172357
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public string Role { get; set; }
        public string AccountId { get; set; }
    }

}
