namespace authentication;

public interface Ijwt
{
    string GenerateToken(User appUser);
}
