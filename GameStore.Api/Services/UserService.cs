using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly GameStoreContext _context;

    public UserService(GameStoreContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsers()
        => await _context.User.ToListAsync();

    public async Task<User?> GetUserById(int id)
        => await _context.User.FindAsync(id);

    public async Task<int> AddUser(UserDtoCreate user)
    {
        User newUser = new();
        newUser.Username = user.Username;
        newUser.Email = user.Email;
        await _context.User.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return newUser.Id;
    }

    public async Task DeleteUser(int id)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null) return;

        _context.User.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(int id, string username, string? email)
    {
        var user = await _context.User.FindAsync(id);
        if (user == null) return;

        if (!string.IsNullOrWhiteSpace(username))
            user.Username = username;

        if (!string.IsNullOrWhiteSpace(email))
            user.Email = email;
        await _context.SaveChangesAsync();
    }
}