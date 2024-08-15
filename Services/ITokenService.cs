namespace API_BurgerManiaBackend.Services
{
    public interface ITokenService
    {
        string GenerateToken(string username);
    }
}
