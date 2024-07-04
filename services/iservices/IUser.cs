namespace authentication;

public interface IUser
{
    Task<string> RegisterUser (User user);
    Task<List<User>> GetUsers();
    Task<LoginResponseDto> LoginUser (LoginRequestDto loginRequest);
    Task<User> GetUser (Guid Id, string token);
    Task<User> GetUserById(Guid Id);
    Task<User> GetUserByEmail(string Email);
    Task<string> UpdateUser (Guid Id,RegisterUserDto updateUser);
    Task<string> DeleteUser (User user);

}
