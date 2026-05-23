public class UserService : IUserService
{
    private readonly GameStoreContext _context;

    public UserService(GameStoreContext context)
    {
        _context = context;
    }

    public List<User> GetAllUsers()
        => _context.User.ToList();

    public User? GetUserById(int id)
        => _context.User.Find(id);

    public int AddUser(UserDtoCreate user)
    {
        User newUser = new();
        newUser.Username = user.Username;
        newUser.Email = user.Email;
        _context.User.Add(newUser);
        _context.SaveChanges();
        return newUser.Id;
    }

    public void DeleteUser(int id)
    {
        var user = _context.User.Find(id);
        if (user == null) return;

        _context.User.Remove(user);
        _context.SaveChanges();
    }

    public void UpdateUser(int id, string username, string? email)
    {
        var user = _context.User.Find(id);
        if (user == null) return;

        if (!string.IsNullOrWhiteSpace(username))
            user.Username = username;

        if (!string.IsNullOrWhiteSpace(email))
            user.Email = email;
        _context.SaveChanges();
    }
}