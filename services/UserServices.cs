
using Microsoft.EntityFrameworkCore;

namespace authentication;

public class UserServices : IUser
{
    private readonly AppDbContext _dbContext;
    public UserServices(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> DeleteUser(User user)
    {
        try
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return $"{user.Fullname} deleted";

        }
        catch (Exception ex)
        {
            return $"{ex.Message}";
        }
    }

    // public async Task<User> GetUser(Guid Id)
    // {
    //     return await _dbContext.Users.Where(user=> user.Id == Id).FirstOrDefaultAsync();
    // }

    public async Task<User> GetUser(Guid Id, string token)
    {
        try
        {
            return await _dbContext.Users.Where(user => user.Id == Id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<User> GetUserByEmail(string Email)
    {
        try
        {
            return await _dbContext.Users.Where(user => user.Email == Email).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<User> GetUserById(Guid Id)
    {
        try
        {
            return await _dbContext.Users.Where(user => user.Id == Id).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<List<User>> GetUsers()
    {
        try
        {
            return await _dbContext.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            // ex.Message;
            throw;
        }

    }

    public Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<string> RegisterUser(User user)
    {
        try
        {
            var isFirstUser = _dbContext.Users.Any();
            if (!isFirstUser)
            {
                user.Role = "Admin";
            }
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return $"{user.Fullname} registered successfully!!";

        }
        catch (Exception ex)
        {
            return $"{ex.Message}";
        }

    }

    public async Task<string> UpdateUser(Guid Id, RegisterUserDto updateUser)
    {
        try
        {
            var toBeUpdatedUser = await _dbContext.Users.Where(user => user.Id == Id).FirstOrDefaultAsync();
            if (toBeUpdatedUser != null)
            {
                toBeUpdatedUser.Fullname = updateUser.Fullname;
                toBeUpdatedUser.Email = updateUser.Email;
                toBeUpdatedUser.PhoneNumber = updateUser.PhoneNumber;
                toBeUpdatedUser.Residence = updateUser.Residence;
                toBeUpdatedUser.Password = updateUser.Password;

                await _dbContext.SaveChangesAsync();
                return $"{toBeUpdatedUser.Fullname} updated successfully!!";

            }

            return "User not found";

        }
        catch (Exception ex)
        {
            // ex.Message;
            throw;
        }

    }
}
