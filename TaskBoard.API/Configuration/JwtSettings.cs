namespace TaskBoard.API.Configuration
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = "YourSuperSecretKeyThatIsAtLeast32CharactersLong!";
        public string Issuer { get; set; } = "TaskBoardAPI";
        public string Audience { get; set; } = "TaskBoardUsers";
        public int ExpirationMinutes { get; set; } = 60;
    }
}
