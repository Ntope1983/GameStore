public interface IUserService
{

    List<User> GetAllUsers();

    User? GetUserById(int id);

    int AddUser(UserDtoCreate user);

    void DeleteUser(int id);

    void UpdateUser(int id, string username, string? email);

}