public interface IUserService
{

    List<User> GetAllUsers();

    User? GetUserById(int id);

    void AddUser(User user);

    void DeleteUser(int id);

    void UpdateUser(int id, string username, string? email);

}