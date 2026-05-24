public interface IUserService
{

    Task<List<User>> GetAllUsers();

    Task<User?> GetUserById(int id);

    Task<int> AddUser(UserDtoCreate user);

    Task DeleteUser(int id);

    Task UpdateUser(int id, string username, string? email);

}