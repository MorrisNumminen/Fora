namespace Fora.Shared
{
    public class LoginDto
    {
        public bool IsLoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBanned { get; set; }
    }
}
